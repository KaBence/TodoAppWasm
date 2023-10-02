using Application.DaoInterfaces;
using Shared.DTO;
using Shared.Models;

namespace FileData.DAOs;

public class TodoFileDao : ITodoDao
{
    private readonly FileContext context;

    public TodoFileDao(FileContext context)
    {
        this.context = context;
    }
    

    public Task<Todo> CreateAsync(Todo todo)
    {
        int id = 1;
        if (context.Todos.Any())
        {
            id = context.Todos.Max(t => t.Id);
            id++;
        }

        todo.Id = id;
        
        context.Todos.Add(todo);
        context.SaveChanges();

        return Task.FromResult(todo);
    }

    public Task<IEnumerable<Todo>> GetAsync(SearchTodoParametersDto searchParameters)
    {
        IEnumerable<Todo> todos = context.Todos.AsEnumerable();

        if (!string.IsNullOrEmpty(searchParameters.Username))
        {
            todos = context.Todos.Where(todo => todo.Owner.UserName.Equals(searchParameters.Username));
        }

        if (searchParameters.UserId!=null)
        {
            todos = todos.Where(todo => todo.Id == searchParameters.UserId);
        }

        if (searchParameters.CompletedStatus!=null)
        {
            todos = todos.Where(todo => todo.IsCompleted == searchParameters.CompletedStatus);
        }

        if (!string.IsNullOrEmpty(searchParameters.TitleContains))
        {
            todos = todos.Where(todo => todo.Title.Contains(searchParameters.TitleContains));
        }

        return Task.FromResult(todos);
    }

    public Task<Todo?> GetByIdAsync(int id)
    {
        Todo? todo = context.Todos.FirstOrDefault(todo => todo.Id == id);;
        return Task.FromResult(todo);
    }

    public Task UpdateAsync(Todo todo)
    {
        Todo? existing = context.Todos.FirstOrDefault(x => x.Id == todo.Id);
        if (existing==null)
        {
            throw new Exception($"There is no todo with this Id {todo.Id}");
        }

        context.Todos.Remove(existing);
        context.Todos.Add(todo);
        context.SaveChanges();
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        Todo? existing = context.Todos.FirstOrDefault(x => x.Id == id);
        if (existing==null)
        {
            throw new Exception($"There is no todo with this Id:  {id}");
        }

        context.Todos.Remove(existing);
        context.SaveChanges();
        return Task.CompletedTask;
    }
}