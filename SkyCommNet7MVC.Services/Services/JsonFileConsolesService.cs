using SkyCommNet7MVC.Domain.Models;
using System.Text.Json;

namespace SkyCommNet7MVC.Services.Services
{
    public class JsonFileConsolesService
    {
        public JsonFileConsolesService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }

        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "consoles", "consoles.json"); }
        }

        public IEnumerable<Consoles> GetConsoles()
        {
            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                return JsonSerializer.Deserialize<Consoles[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
        }

        public Consoles GetConsoleByID(string consoleID)
        {
            var consolelist = GetConsoles();

            var selectedconsole = consolelist.First(x => x.ConsoleId == consoleID);
            return selectedconsole;
        }
    }
}
