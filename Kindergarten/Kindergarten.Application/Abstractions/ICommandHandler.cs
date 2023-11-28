using MediatR;

namespace Kindergarten.Application.Abstractions
{
    public interface ICommandHandler<in TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
       where TRequest : ICommand<TResponse>
    {
    }
}
