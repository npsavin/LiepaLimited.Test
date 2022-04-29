using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace LiepaLimited.Test.Application.Queries
{
    public interface IQuery<out T> : IRequest<T> { }
}
