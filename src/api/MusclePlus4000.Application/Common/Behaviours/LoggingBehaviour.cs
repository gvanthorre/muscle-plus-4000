using MediatR;
using Microsoft.Extensions.Logging;

namespace MusclePlus4000.Application.Common.Behaviours;

/// <summary>
/// MediatR pipeline behaviour that logs the start and completion
/// (or failure) of every request for observability.
/// </summary>
public sealed class LoggingBehaviour<TRequest, TResponse>(
    ILogger<LoggingBehaviour<TRequest, TResponse>> logger
) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;

        logger.LogInformation("Handling {RequestName}", requestName);

        try
        {
            var response = await next();
            logger.LogInformation("Handled {RequestName} successfully", requestName);
            return response;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Request {RequestName} failed", requestName);
            throw;
        }
    }
}

