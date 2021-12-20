using ProjectBank.Client.Pages;
using ProjectBank.Core.Enum;
using ProjectBank.Core.DTO;
using System.Net.Http;
using System.Net.Http.Json;

namespace ProjectBank.Client.Scripts;

public class KeywordFinder : IKeywordFinder
{
    private const int DefaultRatio = 10;
    private HttpClient _http;
    private Degree _degree;
    public IDictionary<string, int> Ratios { get; } = new Dictionary<string, int>();
    public int Ratio_Total { get; set; }
    
    //The int is a counter for how many times a keyword has been seen
    private Dictionary<string, int> _keywords = new Dictionary<string, int>();

    public async Task Setup(HttpClient Http, Degree degree)
    {
        _http = Http;
        _degree = degree;
        var keywords = await _http.GetFromJsonAsync<string[]>("/api/Keyword/getStrings");
        if (keywords != null){
            foreach (var keyword in keywords)
            {
                _keywords.Add(keyword, 0);
                AddKeywordToRatios(keyword);
            }
        }
    }     

    //Test den her metode
    //Skal den være async?
    public string FindWeightedRandomKeyword()
    {
        Random random = new Random();
        int x = random.Next(0, Ratio_Total);
        
        for (int i = 0; x > 0 && i < Ratios.Count; i++)
        {
            var element = Ratios.ElementAt(i);
            var key = element.Key;
            var value = element.Value;

            if ((x -= value) <= 0)
            {
                return key;
            }
        }

        return null;
    }

    public Status UpdateRatioAsync(string keywordName, bool userLikedProject)
    {
        if (!Ratios.ContainsKey(keywordName))
        {
            return Status.NotFound;
        }

        if(userLikedProject)
        {
            var ratio = Ratios[keywordName];
            Ratios[keywordName] *= 2;
            Ratio_Total += ratio;
        } else 
        {
            var ratio = Ratios[keywordName]/2;
            Ratios[keywordName] /= 2;
            Ratio_Total -= ratio;
        }
        
        return Status.Updated;
    }

    private void AddKeywordToRatios(string keyword)
    {
        if(!Ratios.ContainsKey(keyword)){
            Ratios.Add(keyword, DefaultRatio);
            Ratio_Total += DefaultRatio;
        }
    }

    public async Task<ProjectDetailsDto?> ReadProjectGivenKeywordAsync(string keyword)
    {

        if (!_keywords.ContainsKey(keyword))
        {
            return null;
        }

        var timesSeen = _keywords.GetValueOrDefault(keyword);

        _keywords[keyword]++;
        
        return await _http.GetFromJsonAsync<ProjectDetailsDto>($"api/keyword/{keyword}/{timesSeen}/{_degree}");
        
    }
}