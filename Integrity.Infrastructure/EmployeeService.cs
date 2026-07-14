namespace Integrity.Infrastructure;

using Application.Interfaces;
using Domain.Entities;

public class EmployeeService : IEmployeeService
{
    public async Task<Employee> GetEmployeeById(int id)
    {
        throw new NotImplementedException();
    }
    
    public async Task<Employee> GetPositionByEmployeeId(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Employee> GetEmployeeByClockNumber(int clockNumber)
    {
        throw new NotImplementedException();
    }

    public async Task<Employee> GetDepartmentByEmployeeId(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Employee> GetSupervisorByEmployeeId(int id)
    {
        throw new NotImplementedException();
    }
}