using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Company.Function;

public class Calculator
{
    private readonly ILogger<Calculator> _logger;

    public Calculator(ILogger<Calculator> logger)
    {
        _logger = logger;
    }

    [Function("Calculator")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "calculator/{a:int}/{b:int}/{operation:string}")] HttpRequest req, int a, int b, string operation)
    {
        int result = 0;
        if (operation == "add")
        {
            result = a + b;
        }
        else if (operation == "subtract")
        {
            result = a - b;
        }
        else if (operation == "multiply")
        {
            result = a * b;
        }
        else if (operation == "divide")
        {
            if (b == 0)
            {
                return new BadRequestObjectResult("Division by zero is not allowed.");
            }
            result = a / b;
        }
        else
        {
            return new BadRequestObjectResult("Invalid operation. Supported operations are add, subtract, multiply, divide.");
        }

        return new OkObjectResult($"The result of {operation}ing {a} and {b} is: {result}");
    }
}