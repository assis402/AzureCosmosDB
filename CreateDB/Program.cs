using Microsoft.Azure.Cosmos;

var cosmosEndpointUri = "https://appaccount.documents.azure.com:443/";
var cosmosDBKey = "riZtEhsY2qeICNuHNPX3FoJJHxcVB9rXnGHx7M4LPAsinGH8iqBstzzW5EYy0rioWh8ILY7okxc0BJdtQFXDrw==";

await CreateDatabase("appdb");

async Task CreateDatabase(string databaseName)
{
    var cosmosClient = new CosmosClient(cosmosEndpointUri, cosmosDBKey);

    await cosmosClient.CreateDatabaseAsync(databaseName);
    Console.WriteLine("Database created");
}
