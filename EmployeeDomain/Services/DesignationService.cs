using EmployeeDomain.Dto;
using EmployeeDomain.Interfaces;
using EmployeeDomain.ViewModels;
using EmployeeInfrastructure.Models;
using Microsoft.EntityFrameworkCore;


namespace EmployeeDomain.Services
{
    public class DesignationService : IDesignationService
    {
        private readonly EmployeeContext _employeeContext;

        public DesignationService(EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;

        }
        public async Task<bool> AddDesignation(AddDesignationDto designation)
        {
            bool isSucess = false;
            if (!string.IsNullOrEmpty(designation.DesignationName))
            {
                DesignationMaster designationMaster = new DesignationMaster()
                {
                    Designation = designation.DesignationName,
                    Active = true,
                    DeleteFlag = false
                };

                await _employeeContext.DesignationMasters.AddAsync(designationMaster);
                int id = await _employeeContext.SaveChangesAsync();

                isSucess = id == 1;
            }
            return isSucess;
        }

        public async Task<GetDesignationDto> GetDesignationById(int DesignationId)
        {
            GetDesignationDto designation = new GetDesignationDto();
            if (DesignationId != 0)
            {
                Designation data = await _employeeContext.DesignationMasters.AsNoTracking()
                                                         .Where(w => w.DesignationId == DesignationId && w.Active == true && w.DeleteFlag == false)
                                                         .Select(s => new Designation
                                                         {
                                                             DesignationId = s.DesignationId,
                                                             DesignationName = s.Designation
                                                         }).FirstOrDefaultAsync() ?? new Designation();

                if (data.DesignationId != 0)
                {
                    designation = new GetDesignationDto
                    {
                        DesignationId = data.DesignationId,
                        DesignationName = data.DesignationName
                    };

                }
            }
            return designation;
        }

        public async Task<List<GetDesignationDto>> GetAllDesignationData()
        {
            List<GetDesignationDto> result = new List<GetDesignationDto>();

            List<Designation> designationDatas = await _employeeContext.DesignationMasters.AsNoTracking()
                                                                       .Where(w => w.Active == true && w.DeleteFlag == false)
                                                                       .Select(s => new Designation
                                                                       {
                                                                           DesignationId = s.DesignationId,
                                                                           DesignationName = s.Designation
                                                                       }).ToListAsync();

            if (designationDatas.Any())
            {
                result = designationDatas.Select(s => new GetDesignationDto
                {
                    DesignationId = s.DesignationId,
                    DesignationName = s.DesignationName,
                }).ToList();
            }

            return result;
        }
    }
}
