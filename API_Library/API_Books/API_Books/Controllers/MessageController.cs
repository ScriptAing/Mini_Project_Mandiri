using Microsoft.AspNetCore.Mvc;

namespace API_Books.Controllers
{
    public class MessageController : Controller
    {
        public string GetMessage(string code)
        {
            var builder = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("message.json");

            var config = builder.Build();
            return config.GetSection(code).Value.ToString();
        }
    }
}
