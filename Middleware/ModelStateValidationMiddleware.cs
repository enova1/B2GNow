// ModelStateValidationMiddleware.cs

namespace MVC_CORE.Middleware;

/// <summary>
/// custom middleware to validate model state before calling the controller action method.
/// </summary>
public class ModelStateValidationMiddleware
{
    private readonly RequestDelegate _next;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="next"></param>
    public ModelStateValidationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.StartsWithSegments("/v1")) // Adjust the path as needed
        {
            var endpoint = context.GetEndpoint();
            if (endpoint?.Metadata.GetMetadata<Controller>() != null)
            {
                if (!context.Items.ContainsKey("__MANUAL_VALIDATION"))
                {
                    var actionDescriptor = endpoint.Metadata.GetMetadata<ControllerActionDescriptor>();
                    var controllerType = actionDescriptor!.ControllerTypeInfo.AsType();

                    if (context.RequestServices.GetService(controllerType) is Controller controllerInstance)
                    {
                        var actionName = actionDescriptor.ActionName;
                        var actionMethodInfo = controllerType.GetMethod(actionName);

                        if (actionMethodInfo != null)
                        {
                            var parameters = actionMethodInfo.GetParameters();

                            if (parameters.Length > 0 && parameters[0].ParameterType == typeof(object))
                            {
                                var modelParameterName = parameters[0].Name;
                                var model = context.Request.Form[modelParameterName];

                                controllerInstance.TryValidateModel(model);
                            }
                        }
                    }

                    if (context.Items["__MODEL_STATE"] is ModelStateDictionary { IsValid: false } modelState)
                    {
                        var errors = modelState.Values.SelectMany(v => v.Errors);
                        context.Response.StatusCode = 400;
                        await context.Response.WriteAsJsonAsync(new { response = errors });
                        return;
                    }
                }
            }
        }

        await _next(context);
    }
}

public static class ModelStateValidationMiddlewareExtensions
{
    public static IApplicationBuilder UseModelStateValidation(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ModelStateValidationMiddleware>();
    }
}