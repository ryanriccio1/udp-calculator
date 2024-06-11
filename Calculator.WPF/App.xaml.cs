using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using Calculator.WPF.ViewModels;
using UdpLibrary;

namespace Calculator.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {

            // Taken from UdpCalculateServer/Program.cs
            // Runs as a console app when WPF app is started
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

            // bind to our view model
            MainWindow = new MainWindow()
            {
                DataContext = new CalculatorViewModel()
            };
            MainWindow.Show();

            base.OnStartup(e);
        }

    }
}
