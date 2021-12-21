namespace ProjectBank.Core.Enum;
    public enum Degree{
        Bachelor,
        Master,
        Phd,
        Unspecified //so we can search for projects with a non-specified degree. aka. find all matching just keyword and ignore degree.
    }