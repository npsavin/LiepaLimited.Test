using MediatR;

namespace LiepaLimited.Test.Application.Queries
{
    public interface IQuery<out T> : IRequest<T> { }
}
