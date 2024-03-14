using AutoMapper;
using Todos.Dtos;
using Todos.Entities;

namespace Todos.MapperProfiles;

public class TodoProfile : Profile
{
    public TodoProfile()
    {
        CreateMap<TodoCreateDto, Todo>();
        CreateMap<TodoUpdateDto, Todo>();
        CreateMap<Todo, TodoResponseDto>();
    }
}