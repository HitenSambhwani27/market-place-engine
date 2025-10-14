using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Behaviours
{
    public class LoggingBehaviour<TRequest, TResponse>(ILogger<LoggingBehaviour<TRequest,TResponse>> logger) : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull, IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            logger.LogInformation("[Start] Handle Request = {Request} - Response = {Response}", typeof(TRequest).Name, typeof(TRequest).Name, request);

                var timer = new Stopwatch();
            timer.Start();

            var response = await next();
            timer.Stop();

            var timeTaken = timer.Elapsed;

            if(timeTaken.Seconds > 3)
            {
                logger.LogWarning("[Performance] the request {Request} took {timeTaken} seconds", typeof(TRequest).Name, typeof(TRequest).Name, request);
            }
            return response;
        }
    }
}
