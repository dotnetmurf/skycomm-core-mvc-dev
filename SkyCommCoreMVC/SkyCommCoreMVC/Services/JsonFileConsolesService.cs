using Microsoft.AspNetCore.Hosting;
using SkyCommCoreMVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace SkyCommCoreMVC.Services
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
