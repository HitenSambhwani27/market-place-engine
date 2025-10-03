using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.CQRS
{
    public interface ICommand : IRequest
    {
    }
    public interface ICommand<out TResponse>: IRequest<TResponse>
    {

    }
}
