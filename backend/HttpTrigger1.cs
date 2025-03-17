using Azure.Data.Tables;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Company.Function
{
    public class HttpTrigger1
    {
        private readonly ILogger<HttpTrigger1> _logger;
        private readonly string _connectionString = Environment.GetEnvironmentVariable("TableStorageConnectionString") ??
            throw new InvalidOperationException("TableStorageConnectionString environment variable not set. Check local.settings.json");

        public HttpTrigger1(ILogger<HttpTrigger1> logger)
        {
            _logger = logger;
        }

        [Function("HttpTrigger1")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("Processing request...");

            var queryParams = System.Web.HttpUtility.ParseQueryString(req.Url.Query);
            string? name = queryParams["name"];

            if (string.IsNullOrEmpty(name))
            {
                var badResponse = req.CreateResponse(HttpStatusCode.BadRequest);
                await badResponse.WriteStringAsync("Please provide a name.");
                return badResponse;
            }

            var tableClient = new TableClient(_connectionString, "NamesTable");

            await tableClient.CreateIfNotExistsAsync();

            var entity = new TableEntity("User", Guid.NewGuid().ToString())
            {
                { "Name", name },
                { "Timestamp", DateTime.UtcNow.ToString("o") }
            };

            await tableClient.AddEntityAsync(entity);

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Access-Control-Allow-Origin", "*");
            response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, OPTIONS");

            await response.WriteStringAsync($"Saved {name} to Table Storage!");
            return response;
        }
    }
}