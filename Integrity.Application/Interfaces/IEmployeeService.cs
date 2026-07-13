namespace Integrity.Application.Interfaces;
using Domain.Entities;

public interface IEmployeeService
{
    Task<Employee> GetEmployeeById(int id);
    Task<Employee> GetDepartmentByEmployeeId(int id);
    Task<Employee> GetSupervisorByEmployeeId(int id);
    Task<Employee> GetPositionByEmployeeId(int id);
    Task<Employee> GetEmployeeByClockNumber(int clockNumber);
}