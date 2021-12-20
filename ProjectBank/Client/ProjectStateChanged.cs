namespace ProjectBank.Client;
//
//this approach is highly inspired by Quanqo from StackOverflow, and Chris Sainty from his own website.
//Creates a state container to manage communication between components in an observer pattern.
//NavMenu subscribes to the OnChange event of this class, it "observes" for changes.
//When the swipe page is updated, the updated state is given to this class, which invokes the onChange event.
//the NavMenu then checks the updated data of this class and re-renders accordingly, in a dynamic sense.
//
//The class is added as a singleton service, to be available through components that inject it,
//whe the host has been build.
//
//links below:
//https://chrissainty.com/3-ways-to-communicate-between-components-in-blazor/
//https://stackoverflow.com/questions/60912726/exchange-data-between-page-and-navmenu-in-blazor
public class ProjectStateChanged
{
    public ProjectStateChanged()
    {
        projects = new List<string>();
    }
    public List<string> projects { get; private set; }
    public event Action OnChange;
    public void AddSavedProjects(string title)
    {
        if (!projects.Contains(title))
        {
            projects.Add(title);
            NotifyStateChanged();
        }
    }
    public void RemoveSavedProjects(string title)
    {
        if (projects.Contains(title))
        {
            projects.Remove(title);
             NotifyStateChanged();
        }
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
    private void NotifyStateChanged() => OnChange?.Invoke();
}