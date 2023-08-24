using RecordSetup.Entities;

namespace RecordSetup.Dto
{
    public class SubRegionRecordDto
    {
        public Guid Id { get; set; }
        public string DisplayId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string RegionName { get; set; }
        public Guid RegionId { get; set; }
    }

    public class SubRegionRecordRequestModel
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public Guid RegionId { get; set; }
    }


        
}
