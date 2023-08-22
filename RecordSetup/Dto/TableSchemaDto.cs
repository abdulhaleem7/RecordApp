using RecordSetup.Entities;

namespace RecordSetup.Dto
{
    public class TableSchemaDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string SubRegionRecordTableName { get; set; }
        public Guid SubRegionRecordTableId { get; set; }
    }
    
    public class TableSchemaRequestModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string SubRegionRecordTableName { get; set; }
        public Guid SubRegionRecordTableId { get; set; }
    }
}
