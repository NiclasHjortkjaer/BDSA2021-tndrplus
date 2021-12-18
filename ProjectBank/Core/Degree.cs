namespace ProjectBank.Core
{
    public enum Degree{
        Bachelor,
        Master,
        PHD,
        Unspecified //so we can search for projects with a non-specified degree. aka. find all matching just keyword and ignore degree.
    }
    
}