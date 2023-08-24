namespace RecordSetup.Dto
{
    public class RegionDto
    {
        public Guid Id { get; set; }
        public string DisplayId { get; set; }  
        public string? Name { get; set; }
    }

    public class RegionRequestModel
    {
        public string? Name { get; set; }
    }
}
