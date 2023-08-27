using RecordSetup.Entities;

namespace RecordSetup.Dto
{
    public class SubRegionRecordTableDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DisplayId { get; set; }
        public string? Description { get; set; }
        public string SubRegionRecordName { get; set; }
        public Guid SubRegionRecordId { get; set; }
        public string RegionName { get; set; }
    }

    public class SubRegionRecordTableRequestModel
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string SubRegionRecordName { get; set; }
        public Guid SubRegionRecordId { get; set; }
    }
}
