namespace RecordSetup.Entities
{
    public class Region
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public List<SubRegionRecord> SubRegions { get; set; } = new List<SubRegionRecord>();
    }
}
