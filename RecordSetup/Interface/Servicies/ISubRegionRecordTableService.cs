using RecordSetup.Dto;
using RecordSetup.Entities;

namespace RecordSetup.Interface.Servicies
{
    public interface ISubRegionRecordTableService
    {
        Task<BaseResponse<SubRegionRecordTableDto>> Register(SubRegionRecordTableRequestModel requestModel);
        Task<BaseResponse<SubRegionRecordTableDto>> Delete(Guid id);
        Task<BaseResponse<SubRegionRecordTableDto>> Get(Guid id);
        Task<BaseResponse<IEnumerable<SubRegionRecordTableDto>>> GetAll();
        Task<BaseResponse<SubRegionRecordTableDto>> Update(Guid id, SubRegionRecordTableRequestModel requestModel);
    }
}
