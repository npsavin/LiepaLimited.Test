using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace LiepaLimited.Test.Application.Command
{
    public interface ICommand<out T> : IRequest<T> { }

    public interface ICommand : IRequest, ICommand<Unit> { }
}
