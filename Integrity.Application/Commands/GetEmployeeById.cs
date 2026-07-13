using Integrity.Application.Interfaces;

namespace Integrity.Application.Commands;

public class GetEmployeeById
{
    private readonly IEmployeeService _employeeService;

    public GetEmployeeById(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    public async Task<Employee?> ExecuteAsync(int id)
    {
        return await _employeeService.GetEmployeeById(id);
    }
}