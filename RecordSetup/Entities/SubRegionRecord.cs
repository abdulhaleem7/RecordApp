namespace RecordSetup.Entities
{
    public class SubRegionRecord
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public Region Region { get; set; }
        public Guid RegionId { get; set; }
        public List<SubRegionRecordTable> SubRegionRecordTables { get; set; } = new List<SubRegionRecordTable>();
    }
}
