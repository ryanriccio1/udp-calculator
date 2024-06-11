using System.Data;
using UdpLibrary;

var server = new UdpListener();
Console.WriteLine("Server Started on Port 50025");

// async server listener
Task.Factory.StartNew(async () =>
{
    while (true)
    {
        var received = await server.ReceiveAsync();
        Console.WriteLine(received.Sender + ": " + received.Message);

        // We may want to do something on CLEAR later
        if (received.Message != "CLEAR")
        {
            string result;
            try
            {
                // try and parse the input
                result = Convert.ToString(new DataTable().Compute(received.Message, null));
            }
            catch
            {
                result = "Error";
            }

            // reply
            Console.WriteLine(received.Sender + ": (result) " + result);
            await server.ReplyAsync(result, received.Sender);
        }
    }
});

// synchronous input loop
string read;
do
{
    read = Console.ReadLine();
} while (read != "quit");