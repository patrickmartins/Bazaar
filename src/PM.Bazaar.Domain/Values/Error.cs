namespace PM.Bazaar.Domain.Values
{
    public class Error
    {
        public string Description { get; set; }
        public string Source { get; set; }

        public Error() { }

        public Error(string description, string source)
        {
            Description = description;
            Source = source;
        }
    }
}
