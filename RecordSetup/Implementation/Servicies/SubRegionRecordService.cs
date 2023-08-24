using RecordSetup.Dto;
using RecordSetup.Entities;
using RecordSetup.Implementation.Repositories;
using RecordSetup.Interface.Repositories;
using RecordSetup.Interface.Servicies;

namespace RecordSetup.Implementation.Servicies
{
    public class SubRegionRecordService : ISubRegionRecordService
    {
        private readonly ISubRegionRecordRepository _subRegionRecordRepository;
        public SubRegionRecordService(ISubRegionRecordRepository subRegionRecordRepository)
        {
           _subRegionRecordRepository = subRegionRecordRepository;
        }
        public async Task<BaseResponse<SubRegionRecordDto>> Delete(Guid id)
        {
            var subRegion = await _subRegionRecordRepository.Get(a => a.Id == id);
            if (subRegion == null)
            {
                return new BaseResponse<SubRegionRecordDto>
                {
                    Message = "not found or is already deleted",
                    Status = false
                };
            }
            await _subRegionRecordRepository.Delete(subRegion);
            return new BaseResponse<SubRegionRecordDto>
            {
                Message = "SubRegion deleted",
                Status = true
            };
        }
        public async Task<BaseResponse<SubRegionRecordDto>> Get(Guid id)
        {
            var region = await _subRegionRecordRepository.GetSubRegion(id);
            if (region == null)
            {
                return new BaseResponse<SubRegionRecordDto>
                {
                    Message = "not found",
                    Status = false
                };
            }
            var regionDto = new SubRegionRecordDto
            {
                Id = id,
                Name = region.Name,
                Description = region.Description,
                RegionName = region.Region.Name,
                DisplayId = region.Id.ToString()[..11],
                RegionId = region.RegionId,
            };
            return new BaseResponse<SubRegionRecordDto>
            {
                Data = regionDto,
                Message = "Successful Retrieval",
                Status = true
            };
        }

        public async Task<BaseResponse<IEnumerable<SubRegionRecordDto>>> GetAll()
        {
            var getall = await _subRegionRecordRepository.GetAllSubRegion();
            if (getall == null)
            {
                return new BaseResponse<IEnumerable<SubRegionRecordDto>>
                {
                    Message = "No record Found",
                    Status = false
                };
            }
            var result = getall.Select(x => new SubRegionRecordDto
            {
                Name = x.Name,
                Description= x.Description,
                RegionName = x.Region.Name,
                DisplayId = x.Id.ToString()[..11],
                Id = x.Id,
                RegionId = x.RegionId,
            });
            return new BaseResponse<IEnumerable<SubRegionRecordDto>> { Data = result, Status = true, Message = "Sub-Region Records Successfully Retrieved" };
        }

        public async Task<BaseResponse<SubRegionRecordDto>> Register(SubRegionRecordRequestModel regionDto)
        {
            var getname = await _subRegionRecordRepository.Get(l => l.Name == regionDto.Name);
            if (getname != null)
            {
                return new BaseResponse<SubRegionRecordDto>
                {
                    Message = "already exist",
                    Status = false
                };
            }
            var region = new SubRegionRecord
            {
                Name = regionDto.Name,
                Description = regionDto.Description,
                RegionId = regionDto.RegionId,
            };
            var responseSaved =  await _subRegionRecordRepository.Register(region);

            return new BaseResponse<SubRegionRecordDto>
            {
                Message = "created succesfully",
                Status = true,
                Data = new SubRegionRecordDto
                {
                    Name = responseSaved.Name,  
                    Description = responseSaved.Description,
                    RegionId = responseSaved.RegionId,
                    Id = responseSaved.Id,
                    DisplayId = responseSaved.Id.ToString()[..11],

                }
            };
        }

        public async Task<BaseResponse<SubRegionRecordDto>> Update(Guid id, SubRegionRecordRequestModel regionDto)
        {
            var region = await _subRegionRecordRepository.GetSubRegion(id);
            if (region == null)
            {
                return new BaseResponse<SubRegionRecordDto>
                {
                    Message = "not found",
                    Status = false
                };
            }
            region.Name = regionDto.Name ?? region.Name;
            region.Description = regionDto.Description ?? region.Description;
            await _subRegionRecordRepository.Update(region);
            return new BaseResponse<SubRegionRecordDto>
            {
                Message = "updated succesfully",
                Status = true
            };
        }
    }
}
