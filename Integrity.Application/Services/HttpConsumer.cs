using Integrity.Application.Models;
using Integrity.Application.Models.Types;

namespace Integrity.Application.Services;

public class HttpConsumer (HttpClient httpClient): HttpClient
{
    public async Task<OperationResult> LivelinessCheck(OperationContext context)
    {
        try
        {
            var response = await httpClient.GetAsync("api/health/live");

            if (response.IsSuccessStatusCode)
            {
                return OperationResult.Success(context);
            }
        }
        catch (Exception ex)
        {
            return OperationResult.Failure(
                context,
                new Error(
                    "HttpRequest",
                    context.Operation,
                    ErrorType.Configuration,
                    ex.Message
                ));
        }
        return OperationResult.Failure(
            context,
            new Error(
                "HttpRequest",
                context.Operation,
                ErrorType.NonExecution,
                "No execution within bounds."
            ));
    }
    
    public async Task<OperationResult> FromGetAsync(string url, OperationContext context)
    {
        try
        {
            var response = await httpClient.GetAsync(url);
            
            if (response.IsSuccessStatusCode)
            {
                return OperationResult.Success(context);
            }
        } catch (Exception ex)
        {
            switch (ex.HResult)
            {
                case 500:
                    return OperationResult.Failure(
                        context,
                        new Error(
                            "HttpRequest",
                            context.Operation,
                            ErrorType.Infrastructure,
                            ex.Message
                        ));
                case 401:
                    return OperationResult.Failure(
                        context,
                        new Error(
                            "HttpRequest",
                            context.Operation,
                            ErrorType.Configuration,
                            ex.Message
                        ));
            }
        }
        return OperationResult.Failure(
            context,
            new Error(
                "HttpRequest",
                context.Operation,
                ErrorType.Infrastructure,
                "Unknown error"
            ));
    }
}