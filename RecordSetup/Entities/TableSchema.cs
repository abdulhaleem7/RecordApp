namespace RecordSetup.Entities
{
    public class TableSchema
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public SubRegionRecordTable SubRegionRecordTable { get; set; }
        public Guid SubRegionRecordTableId { get; set; }
    }
}
