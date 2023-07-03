using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace Company.Function
{
    public static class CosmosTrigger1
    {
        [FunctionName("CosmosTrigger1")]
        public static async Task Run([CosmosDBTrigger(
            databaseName: "%ChangeFeedDatabaseName%",
            containerName: "%ChangeFeedContainerName%",
            MaxItemsPerInvocation = 1,
            StartFromBeginning = true,
            Connection = "CosmosDBConnectionString")]IReadOnlyList<Document> input,
            ILogger log,
            CancellationToken cancellationToken)
        {
            try
            {
                if (input != null && input.Count > 0)
                {
                    log.LogInformation("Documents modified " + input.Count);
                    log.LogInformation("First document Id " + input[0].Id);
                    // Simulate doing some work by waiting for 30 seconds
                    await Task
                        .Delay(TimeSpan.FromSeconds(30), cancellationToken)
                        .ConfigureAwait(continueOnCapturedContext: false);
                    log.LogInformation("Done");
                }
            }
            catch (TaskCanceledException)
            {
                log.LogWarning("Task was cancelled!");
                throw;
            }
        }
    }
}
