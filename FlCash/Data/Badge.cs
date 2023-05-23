namespace FlCash.Data
{
    public class Badge
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int NumberOfPoints { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public string InitialLogo { get; set; }
        public bool Status { get; set; }
    }
}
