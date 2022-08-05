using Microsoft.Azure.Cosmos;

var cosmosEndpointUri = "https://appaccount.documents.azure.com:443/";
var cosmosDBKey = "riZtEhsY2qeICNuHNPX3FoJJHxcVB9rXnGHx7M4LPAsinGH8iqBstzzW5EYy0rioWh8ILY7okxc0BJdtQFXDrw==";
var databaseName = "appdb";
var containerName = "Orders";

await CallStoredProcedure();

async Task CallStoredProcedure()
{
    var cosmosClient = new CosmosClient(cosmosEndpointUri, cosmosDBKey);

    var container = cosmosClient.GetContainer(databaseName, containerName);
    var result = await container.Scripts.ExecuteStoredProcedureAsync<string>("Display", new PartitionKey(""), null);
    
    Console.WriteLine(result);
}

