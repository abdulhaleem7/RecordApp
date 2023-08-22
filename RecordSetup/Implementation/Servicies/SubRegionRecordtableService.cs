using RecordSetup.Dto;
using RecordSetup.Entities;
using RecordSetup.Interface.Repositories;
using RecordSetup.Interface.Servicies;

namespace RecordSetup.Implementation.Servicies
{
    public class SubRegionRecordtableService : ISubRegionRecordTableService
    {

        private readonly ISubRegionRecordTableRepository _subRegionRecordTableRepository;
        public SubRegionRecordtableService(ISubRegionRecordTableRepository subRegionRecordTableRepository)
        {
            _subRegionRecordTableRepository = subRegionRecordTableRepository;
        }
        public async Task<BaseResponse<SubRegionRecordTableDto>> Delete(Guid id)
        {
            var subRegion = await _subRegionRecordTableRepository.Get(a => a.Id == id);
            if (subRegion == null)
            {
                return new BaseResponse<SubRegionRecordTableDto>
                {
                    Message = "not found or is already deleted",
                    Status = false
                };
            }
            await _subRegionRecordTableRepository.Delete(subRegion);
            return new BaseResponse<SubRegionRecordTableDto>
            {
                Message = "SubRegion deleted",
                Status = true
            };
        }
        public async Task<BaseResponse<SubRegionRecordTableDto>> Get(Guid id)
        {
            var region = await _subRegionRecordTableRepository.GetSubregionRecordTable(id);
            if (region == null)
            {
                return new BaseResponse<SubRegionRecordTableDto>
                {
                    Message = "not found",
                    Status = false
                };
            }
            var regionDto = new SubRegionRecordTableDto
            {
                Id = id,
                Name = region.Name,
                Description = region.Description,
                SubRegionRecordName = region.SubRegionRecord.Name
            };
            return new BaseResponse<SubRegionRecordTableDto>
            {
                Data = regionDto,
                Message = "Successful",
                Status = true
            };
        }

        public async Task<BaseResponse<IEnumerable<SubRegionRecordTableDto>>> GetAll()
        {
            var getall = await _subRegionRecordTableRepository.GetAllSubregionRecordTable();
            if (getall == null)
            {
                return new BaseResponse<IEnumerable<SubRegionRecordTableDto>>
                {
                    Message = "No record Found",
                    Status = false
                };
            }
            var result = getall.Select(x => new SubRegionRecordTableDto
            {
                Name = x.Name,
                Description = x.Description,
                SubRegionRecordName = x.SubRegionRecord.Name
            });
            return new BaseResponse<IEnumerable<SubRegionRecordTableDto>> { Data = result };
        }

        public async Task<BaseResponse<SubRegionRecordTableDto>> Register(SubRegionRecordTableRequestModel regionDto)
        {
            var getname = await _subRegionRecordTableRepository.Get(l => l.Name == regionDto.Name);
            if (getname != null)
            {
                return new BaseResponse<SubRegionRecordTableDto>
                {
                    Message = "already exist",
                    Status = false
                };
            }
            var region = new SubRegionRecordTable
            {
                Name = regionDto.Name,
                Description = regionDto.Description,
                SubRegionRecordId = regionDto.SubRegionRecordId,
            };
            var subRegionRecordTableSaved = await _subRegionRecordTableRepository.Register(region);

            return new BaseResponse<SubRegionRecordTableDto>
            {
                Message = "created succesfully",
                Status = true,
                Data = new SubRegionRecordTableDto
                {
                    Name = subRegionRecordTableSaved.Name,
                    Description = subRegionRecordTableSaved.Description,
                    Id  = subRegionRecordTableSaved.Id,
                    SubRegionRecordId = subRegionRecordTableSaved.SubRegionRecordId,
                    SubRegionRecordName = subRegionRecordTableSaved.SubRegionRecord?.Name
                }
            };
        }

        public async Task<BaseResponse<SubRegionRecordTableDto>> Update(Guid id, SubRegionRecordTableRequestModel requestModel)
        {
            var region = await _subRegionRecordTableRepository.GetSubregionRecordTable(id);
            if (region == null)
            {
                return new BaseResponse<SubRegionRecordTableDto>
                {
                    Message = "not found",
                    Status = false
                };
            }
            region.Name = requestModel.Name ?? region.Name;
            region.Description = requestModel.Description ?? region.Description;
            await _subRegionRecordTableRepository.Update(region);
            return new BaseResponse<SubRegionRecordTableDto>
            {
                Message = "updated succesfully",
                Status = true
            };
        }
    }
}
