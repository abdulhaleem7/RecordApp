using RecordSetup.Dto;
using RecordSetup.Entities;
using RecordSetup.Interface.Repositories;
using RecordSetup.Interface.Servicies;

namespace RecordSetup.Implementation.Servicies
{
    public class TableSchemaService : ITableSchemaService
    {
        private readonly ITableSchemaRepository _tableSchemaRepository;
        private readonly ISubRegionRecordTableRepository _subRegionRecordTableRepository;
        public TableSchemaService(ITableSchemaRepository tableSchemaRepository, ISubRegionRecordTableRepository subRegionRecordTableRepository)
        {
            _tableSchemaRepository = tableSchemaRepository;
            _subRegionRecordTableRepository = subRegionRecordTableRepository;
        }
        public async Task<BaseResponse<TableSchemaDto>> Delete(Guid id)
        {
            var subRegion = await _tableSchemaRepository.Get(a => a.Id == id);
            if (subRegion == null)
            {
                return new BaseResponse<TableSchemaDto>
                {
                    Message = "not found or is already deleted",
                    Status = false
                };
            }
            await _tableSchemaRepository.Delete(subRegion);
            return new BaseResponse<TableSchemaDto>
            {
                Message = "SubRegion deleted",
                Status = true
            };
        }
        public async Task<BaseResponse<TableSchemaDto>> Get(Guid id)
        {
            var region = await _tableSchemaRepository.GetTableSchemaAsync(id);
            if (region == null)
            {
                return new BaseResponse<TableSchemaDto>
                {
                    Message = "not found",
                    Status = false
                };
            }
            var regionDto = new TableSchemaDto
            {
                Id = id,
                Name = region.Name,
                Description = region.Description,
                SubRegionRecordTableName = region.SubRegionRecordTable.Name
            };
            return new BaseResponse<TableSchemaDto>
            {
                Data = regionDto,
                Message = "Successful",
                Status = true
            };
        }

        public async Task<BaseResponse<IEnumerable<TableSchemaDto>>> GetAll()
        {
            var getall = await _tableSchemaRepository.GetAllTableSchemaAsync();
            if (getall == null)
            {
                return new BaseResponse<IEnumerable<TableSchemaDto>>
                {
                    Message = "No record Found",
                    Status = false
                };
            }
            var result = getall.Select(x => new TableSchemaDto
            {
                Name = x.Name,
                Description = x.Description,
                SubRegionRecordTableName = x.SubRegionRecordTable.Name
            });
            return new BaseResponse<IEnumerable<TableSchemaDto>> { Data = result };
        }

        public async Task<BaseResponse<TableSchemaDto>> Register(TableSchemaRequestModel requestModel)
        {
            var getname = await _tableSchemaRepository.Get(l => l.Name == requestModel.Name);
            if (getname != null)
            {
                return new BaseResponse<TableSchemaDto>
                {
                    Message = "already exist",
                    Status = false
                };
            }
            var region = new TableSchema
            {
                Name = requestModel.Name,
                Description = requestModel.Description,
                SubRegionRecordTableId = requestModel.SubRegionRecordTableId,
            };
            var savedResponse =  await _tableSchemaRepository.Register(region);

            return new BaseResponse<TableSchemaDto>
            {
                Message = "created succesfully",
                Status = true,
                Data = new TableSchemaDto
                {
                    Name = savedResponse.Name,
                    Description = savedResponse.Description,
                    SubRegionRecordTableId= savedResponse.SubRegionRecordTableId,
                    Id = savedResponse.Id,
                    SubRegionRecordTableName = savedResponse.SubRegionRecordTable.Name
                }
            };
        }

        public async Task<BaseResponse<TableSchemaDto>> Update(Guid id, TableSchemaRequestModel regionDto)
        {
            var region = await _tableSchemaRepository.GetTableSchemaAsync(id);
            if (region == null)
            {
                return new BaseResponse<TableSchemaDto>
                {
                    Message = "not found",
                    Status = false
                };
            }
            region.Name = regionDto.Name ?? region.Name;
            region.Description = regionDto.Description ?? region.Description;
            await _tableSchemaRepository.Update(region);
            return new BaseResponse<TableSchemaDto>
            {
                Message = "updated succesfully",
                Status = true
            };
        }
    }
}
