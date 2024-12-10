using EmployeeDomain.Dto;

namespace EmployeeDomain.Interfaces
{
    public interface IDesignationService
    {
        Task<bool> AddDesignation(AddDesignationDto designation);

        Task<GetDesignationDto> GetDesignationById(int DesignationId);

        Task<List<GetDesignationDto>> GetAllDesignationData();
    }
}
