using Shared.DTO;
using Shared.Models;

namespace Application.LogicInterfaces;

public interface ITodoLogic
{
    Task<Todo> CreateAsync(TodoCreationDto dto);
    
    Task<IEnumerable<Todo>> GetAsync(SearchTodoParametersDto searchParameters);
    Task<Todo> GetByIdAsync(int id);

    Task UpdateAsync(TodoUpdateDto dto);
    
    Task DeleteAsync(int id);
}