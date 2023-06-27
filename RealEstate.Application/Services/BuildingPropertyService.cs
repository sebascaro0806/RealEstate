using AutoMapper;
using RealEstate.Application.DTOs.BuildingProperty;
using RealEstate.Domain.Interfaces;
using RealEstate.Domain.Models;

namespace RealEstate.Application.Interfaces
{
    public class BuildingPropertyService : IBuildingPropertyService
    {
        private readonly IBuildingPropertyRepository _buildingPropertyRepository;
        private readonly IMapper _mapper;

        public BuildingPropertyService(IBuildingPropertyRepository buildingPropertyRepository, IMapper mapper)
        {
            _buildingPropertyRepository = buildingPropertyRepository;
            _mapper = mapper;
        }

        public async Task<BuildingPropertyDTO> CreateBuildingProperty(CreateBuildingPropertyDTO buildingPropertyDTO)
        {
            BuildingProperty buildingProperty = _mapper.Map<BuildingProperty>(buildingPropertyDTO);
            await _buildingPropertyRepository.CreateBuildingProperty(buildingProperty);
            return _mapper.Map<BuildingPropertyDTO>(buildingProperty);
        }

        public async Task<IEnumerable<BuildingPropertyDTO>> GetBuildingProperties()
        {

            List<BuildingProperty> owners = (await _buildingPropertyRepository.GetBuildingProperties()).ToList();
            return _mapper.Map<List<BuildingPropertyDTO>>(owners);
        }
    }
}
