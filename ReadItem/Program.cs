using Microsoft.Azure.Cosmos;
using ReadItem;

var cosmosEndpointUri = "https://appaccount.documents.azure.com:443/";
var cosmosDBKey = "riZtEhsY2qeICNuHNPX3FoJJHxcVB9rXnGHx7M4LPAsinGH8iqBstzzW5EYy0rioWh8ILY7okxc0BJdtQFXDrw==";
var databaseName = "appdb";
var containerName = "Orders";

await ReadItem();

async Task ReadItem()
{
    var cosmosClient = new CosmosClient(cosmosEndpointUri, cosmosDBKey);

    var database = cosmosClient.GetDatabase(databaseName);
    var container = database.GetContainer(containerName);

    var query = "SELECT o.orderId, o.category, o.quanitty FROM Orders o";

    var queryDefination = new QueryDefinition(query);

    var feedIterator = container.GetItemQueryIterator<Order>(queryDefination);

    while (feedIterator.HasMoreResults)
    {
        var feedResponse = await feedIterator.ReadNextAsync();
        foreach(var order in feedResponse)
        {
            Console.WriteLine("Order Id {0}", order.orderId);
            Console.WriteLine("Category {0}", order.category);
            Console.WriteLine("Quantity {0}", order.quanitty);
        }
    }
}
