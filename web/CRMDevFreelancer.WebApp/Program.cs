namespace CRMDevFreelancer.WebApp
{
    public class Program
    {
        public static void Main(string[] args) => 
            HostBuilder(args)
                .Build()
                .Run();

        public static IHostBuilder HostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
