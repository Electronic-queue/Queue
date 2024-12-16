using MediatR;
using Queue.Application.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue.Application.Common.Behaviors
{
    public class LoggingBehavior<TRequest,TResponse>
        :IPipelineBehavior<TRequest,TResponse>where TRequest
        :IRequest<TResponse>
    {
        ICurrentUserService _currentUserService;
        public LoggingBehavior (ICurrentUserService currentUserService) =>
            _currentUserService = currentUserService;

        public async Task<TResponse> Handle(TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var requestName=typeof(TRequest).Name;
            var Id = _currentUserService.Id;

            Log.Information("Users Request: {Name} {@Id} {@Request} ",
                requestName, Id, request);

            var response = await next();

            return response;
                
        }

        
    }
}
