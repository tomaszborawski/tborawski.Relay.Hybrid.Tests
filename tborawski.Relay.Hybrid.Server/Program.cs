using tborawski.Relay.Hybrid.Model;

namespace tborawski.Relay.Hybrid.Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConfigBootstrap.Init();
            var config = ConfigBootstrap.Read<HybidRelayConfiguration>("HybidRelayConfiguration");
            var service = new ServiceHost<Test, ITest>();
            service.LogExceptions += Service_LogExceptions;
            var cts = new CancellationTokenSource();
            Task.Run(() => service.OpenAsync(config, cts.Token));
            Console.WriteLine("Server is running");
            Console.ReadLine();

            //Close the service
            cts.Cancel();
            Console.ReadLine();
        }

        private static void Service_LogExceptions(object? sender, Public.ExceptionEventArgs e)
        {
            Console.WriteLine(e.Exception.Message);
        }
    }
}
