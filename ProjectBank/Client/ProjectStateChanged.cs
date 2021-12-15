namespace ProjectBank.Client;
//this approach is highly inspired by Quanqo from StackOverflow, and Chris Sainty. links below:
//https://chrissainty.com/3-ways-to-communicate-between-components-in-blazor/
//https://stackoverflow.com/questions/60912726/exchange-data-between-page-and-navmenu-in-blazor
public class ProjectStateChanged : IDisposable
{
    public ProjectStateChanged()
    {
        projects = new List<string>();
    }

    public List<string> projects { get; private set; }

    public event Action OnChange;
    
    public void AddSavedProjects(string title)
    {
        projects.Add(title);
        NotifyStateChanged();
    }
    

    public void RemoveSavedProjects(string title)
    {
        projects.Remove(title); //if contains
        NotifyStateChanged();
    }
    
    public void setList(ISet<string> titles)
    {
        foreach (string s in titles)
        {
            if (!projects.Contains(s))
            {
                projects.Add(s);   
            }
        }
    }

    //The NavMenu "listens" to the OnChange being invoked, the it will re-render the page based on the new data in this state container class.
    private void NotifyStateChanged() => OnChange?.Invoke();

    public void Dispose()
    {
        this.Dispose();
    }
}