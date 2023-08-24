using RecordSetup.Dto;
using RecordSetup.Entities;
using RecordSetup.Interface.Repositories;
using RecordSetup.Interface.Servicies;
using System.Reflection.Emit;

namespace RecordSetup.Implementation.Servicies
{
    public class RegionService : IRegionService
    {
        private readonly IRegionRepository _regionRepository;
        public RegionService(IRegionRepository regionRepository) 
        { 
            _regionRepository = regionRepository;
        }
        public async Task<BaseResponse<RegionDto>> Delete(Guid id)
        {
            var region = await _regionRepository.Get(a => a.Id == id);
            if (region == null)
            {
                return new BaseResponse<RegionDto>
                {
                    Message = "not found or is already deleted",
                    Status = false
                };
            }
            await _regionRepository.Delete(region);
            return new BaseResponse<RegionDto>
            {
                Message = "Region deleted",
                Status = true
            };
        }

        public async Task<BaseResponse<RegionDto>> Get(Guid id)
        {
            var region = await _regionRepository.Get(s => s.Id == id);
            if (region == null)
            {
                return new BaseResponse<RegionDto>
                {
                    Message = "not found",
                    Status = false
                };
            }
            var regionDto = new RegionDto
            {
                Id = id,
                Name = region.Name,
                 DisplayId = region.Id.ToString()[..9]
            };
            return new BaseResponse<RegionDto>
            {
                Data = regionDto,
                Message = "Successful",
                Status = true
            };
        }

        public async Task<BaseResponse<IEnumerable<RegionDto>>> GetAll()
        {
            var getall = await _regionRepository.GetAll();
            if(getall == null)
            {
                return new BaseResponse<IEnumerable<RegionDto>>
                {
                    Message = "No record Found",
                    Status = false
                };
            }
            var result = getall.Select(x => new RegionDto
            {
                Name = x.Name,
                Id = x.Id,
                DisplayId = x.Id.ToString()[..11]
            });
            return new BaseResponse<IEnumerable<RegionDto>> 
            { 
                Data = result,
                Status = true,
                Message = "Regions successfully retrieved"
            };
        }

        public async Task<BaseResponse<RegionDto>> Register(RegionRequestModel requestModel)
        {
            var getname = await _regionRepository.Get(l => l.Name == requestModel.Name);
            if (getname != null)
            {
                return new BaseResponse<RegionDto>
                {
                    Message = "already exist",
                    Status = false
                };
            }
            var region = new Region
            {
                Name = requestModel.Name
            };
            var regionSaved = await _regionRepository.Register(region);

            return new BaseResponse<RegionDto>
            {
                Message = "created succesfully",
                Status = true,
                Data = new RegionDto
                {
                    Name = regionSaved.Name,
                    Id = regionSaved.Id
                }
            };
        }

        public async Task<BaseResponse<RegionDto>> Update(Guid id, RegionRequestModel regionDto)
        {
            var region = await _regionRepository.Get(s => s.Id == id);
            if (region == null)
            {
                return new BaseResponse<RegionDto>
                {
                    Message = "not found",
                    Status = false
                };
            }

           
            region.Name = regionDto.Name;
            await _regionRepository.Update(region);
            return new BaseResponse<RegionDto>
            {
                Message = "updated succesfully",
                Status = true
            };
        }
    }
}
