using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Company.Function;

public class CalculatorProTrigger
{
    private readonly ILogger<CalculatorProTrigger> _logger;

    public CalculatorProTrigger(ILogger<CalculatorProTrigger> logger)
    {
        _logger = logger;
    }

    [Function("CalculatorProTrigger")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "calculator")] HttpRequest req)
    {
        var request = await req.ReadFromJsonAsync<CalculatorRequest>();
        if (request == null)
        {
            return new BadRequestObjectResult("Invalid request payload.");
        }
        if (request.Operation == "add")
        {
            var result = request.A + request.B;
            return new OkObjectResult(new CalculationResult { Operation = "add", Result = result });
        }
        else if (request.Operation == "subtract")
        {
            var result = request.A - request.B;
            return new OkObjectResult(new CalculationResult { Operation = "subtract", Result = result });
        }
        else if (request.Operation == "multiply")
        {
            var result = request.A * request.B;
            return new OkObjectResult(new CalculationResult { Operation = "multiply", Result = result });
        }
        else if (request.Operation == "divide")
        {
            if (request.B == 0)
            {
                return new BadRequestObjectResult("Division by zero is not allowed.");
            }
            var result = request.A / request.B;
            return new OkObjectResult(new CalculationResult { Operation = "divide", Result = result });
        }
        else
        {
            return new BadRequestObjectResult("Invalid operation. Supported operations are add, subtract, multiply, divide.");
        }

    }
}