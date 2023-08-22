namespace RecordSetup.Entities
{
    public class SubRegionRecordTable
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public SubRegionRecord SubRegionRecord { get; set; }
        public Guid SubRegionRecordId { get; set; }
        public List<TableSchema> TableSchemas { get; set; }  = new List<TableSchema>();
        

    }
}
