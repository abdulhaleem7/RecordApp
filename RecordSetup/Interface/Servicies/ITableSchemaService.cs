using RecordSetup.Dto;

namespace RecordSetup.Interface.Servicies
{
    public interface ITableSchemaService
    {
        Task<BaseResponse<TableSchemaDto>> Register(TableSchemaRequestModel requestModel);
        Task<BaseResponse<TableSchemaDto>> Delete(Guid id);
        Task<BaseResponse<TableSchemaDto>> Get(Guid id);
        Task<BaseResponse<IEnumerable<TableSchemaDto>>> GetAll();
        Task<BaseResponse<TableSchemaDto>> Update(Guid id, TableSchemaRequestModel requestModel);
    }
}
