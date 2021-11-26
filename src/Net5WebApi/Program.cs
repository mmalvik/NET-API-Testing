using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Net5WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Host
                .CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webHost => webHost.UseStartup<Startup>())
                .Build()
                .Run();
        }
    }
}