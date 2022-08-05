using AddItem;
using CreateDB;
using Microsoft.Azure.Cosmos;

var cosmosEndpointUri = "https://appaccount.documents.azure.com:443/";
var cosmosDBKey = "riZtEhsY2qeICNuHNPX3FoJJHxcVB9rXnGHx7M4LPAsinGH8iqBstzzW5EYy0rioWh8ILY7okxc0BJdtQFXDrw==";
var databaseName = "appdb";
var containerName = "Customers";
//var containerName = "Orders";

//await AddItem("01", "Laptop", 100);
//await AddItem("02", "Mobiles", 200);
//await AddItem("03", "Desktop", 75);
//await AddItem("04", "Laptop", 25);

await AddArrayItem("C1", "CustomerA", "New York", new List<Order>()
{
    new Order()
    {
        orderId = "01",
        category = "Laptop",
        quanitty = 100
    },
    new Order()
    {
        orderId = "03",
        category = "Desktop",
        quanitty = 75
    }
});

async Task AddItem(string orderId, string category, int quantity)
{
    var cosmosClient = new CosmosClient(cosmosEndpointUri, cosmosDBKey);

    var database = cosmosClient.GetDatabase(databaseName);
    var container = database.GetContainer(containerName);

    var order = new Order()
    {
        //id = Guid.NewGuid().ToString(),
        orderId = orderId,
        category = category,
        quanitty = quantity
    };

    var response = await container.CreateItemAsync(order, new PartitionKey(category));

    Console.WriteLine("Added item with Order Id {0}", orderId);
    Console.WriteLine("Request Units {0}", response.RequestCharge);
}

async Task AddArrayItem(string customerId, string customerName, string customerCity, List<Order> orders)
{
    var cosmosClient = new CosmosClient(cosmosEndpointUri, cosmosDBKey);

    var database = cosmosClient.GetDatabase(databaseName);
    var container = database.GetContainer(containerName);

    var customer = new Customer()
    {
        customerId = customerId,
        customerName = customerName,
        customerCity = customerCity,
        orders = orders
    };

    await container.CreateItemAsync(customer, new PartitionKey(customerCity));
    Console.WriteLine("Added Customer with id {0}", customerId);
}
