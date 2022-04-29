using MediatR;

namespace LiepaLimited.Test.Application.Command
{
    public interface ICommand<out T> : IRequest<T> { }

    public interface ICommand : IRequest, ICommand<Unit> { }
}
