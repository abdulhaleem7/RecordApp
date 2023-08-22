using RecordSetup.Dto;

namespace RecordSetup.Interface.Servicies
{
    public interface IRegionService
    {
        Task<BaseResponse<RegionDto>> Register(RegionRequestModel requestModel);
        Task<BaseResponse<RegionDto>> Delete(Guid id);
        Task<BaseResponse<RegionDto>> Get(Guid id);
        Task<BaseResponse<IEnumerable<RegionDto>>> GetAll();
        Task<BaseResponse<RegionDto>> Update(Guid id, RegionRequestModel requestModel);

    }
}
