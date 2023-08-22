using RecordSetup.Dto;
using RecordSetup.Entities;

namespace RecordSetup.Interface.Servicies
{
    public interface ISubRegionRecordService
    {
        Task<BaseResponse<SubRegionRecordDto>> Register(SubRegionRecordRequestModel subRegionRecord);
        Task<BaseResponse<SubRegionRecordDto>> Delete(Guid id);
        Task<BaseResponse<SubRegionRecordDto>> Get(Guid id);
        Task<BaseResponse<IEnumerable<SubRegionRecordDto>>> GetAll();
        Task<BaseResponse<SubRegionRecordDto>> Update(Guid id, SubRegionRecordRequestModel subRegionRecord);
    }
}
