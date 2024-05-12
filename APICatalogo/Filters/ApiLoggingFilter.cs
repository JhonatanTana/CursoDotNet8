﻿using Microsoft.AspNetCore.Mvc.Filters;

namespace APICatalogo.Filters; 
public class ApiLoggingFilter: IActionFilter {

    private readonly ILogger<ApiLoggingFilter> _logger;

    public ApiLoggingFilter(ILogger<ApiLoggingFilter> logger) {

        _logger = logger;
    }

    public void OnActionExecuted(ActionExecutedContext context) {

        // Executa apos a Action
        _logger.LogInformation("### Executando -> OnActionExecuting");
        _logger.LogInformation("###################################");
        _logger.LogInformation($"{DateTime.Now.ToLongTimeString()}");
        _logger.LogInformation($"StatusCode : {context.HttpContext.Response.StatusCode}");
        _logger.LogInformation("###################################");

    }

    public void OnActionExecuting(ActionExecutingContext context) {

        // Executa antes da Action
        _logger.LogInformation("### Executando -> OnActionExecuting");
        _logger.LogInformation("###################################");
        _logger.LogInformation($"{DateTime.Now.ToLongTimeString()}");
        _logger.LogInformation($"ModelState : {context.ModelState.IsValid}");
        _logger.LogInformation("###################################");

    }
}
