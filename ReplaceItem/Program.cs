using Microsoft.Azure.Cosmos;
using ReplaceItem;

var cosmosEndpointUri = "https://appaccount.documents.azure.com:443/";
var cosmosDBKey = "riZtEhsY2qeICNuHNPX3FoJJHxcVB9rXnGHx7M4LPAsinGH8iqBstzzW5EYy0rioWh8ILY7okxc0BJdtQFXDrw==";
var databaseName = "appdb";
var containerName = "Orders";

await ReplaceItem();

async Task ReplaceItem()
{
    var cosmosClient = new CosmosClient(cosmosEndpointUri, cosmosDBKey);

    var database = cosmosClient.GetDatabase(databaseName);
    var container = database.GetContainer(containerName);

    var orderId = "01";
    var query = $"SELECT o.id, o.category FROM Orders o WHERE o.orderId = '{orderId}'";

    var id = "";
    var category = "";

    var queryDefination = new QueryDefinition(query);

    var feedIterator = container.GetItemQueryIterator<Order>(queryDefination);

    while (feedIterator.HasMoreResults)
    {
        var feedResponse = await feedIterator.ReadNextAsync();
        foreach (var order in feedResponse)
        {
            id = order.id;
            category = order.category;
        }
    }

    var response = await container.ReadItemAsync<Order>(id, new PartitionKey(category));

    var item = response.Resource;
    item.quanitty = 150;

    await container.ReplaceItemAsync<Order>(item, id, new PartitionKey(category));

    Console.WriteLine("Item is updated");
}
