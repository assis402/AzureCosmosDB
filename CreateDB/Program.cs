using Microsoft.Azure.Cosmos;

var cosmosEndpointUri = "https://appaccount.documents.azure.com:443/";
var cosmosDBKey = "riZtEhsY2qeICNuHNPX3FoJJHxcVB9rXnGHx7M4LPAsinGH8iqBstzzW5EYy0rioWh8ILY7okxc0BJdtQFXDrw==";

///await CreateDatabase("appdb");
await CreateContainer("appdb", "Orders", "/category");

async Task CreateDatabase(string databaseName)
{
    var cosmosClient = new CosmosClient(cosmosEndpointUri, cosmosDBKey);

    await cosmosClient.CreateDatabaseAsync(databaseName);
    Console.WriteLine("Database created");
}

async Task CreateContainer(string databaseName, string containerName, string partitionKey)
{
    var cosmosClient = new CosmosClient(cosmosEndpointUri, cosmosDBKey);

    var database = cosmosClient.GetDatabase(databaseName);

    await database.CreateContainerAsync(containerName, partitionKey);
    Console.WriteLine("Container created");
}