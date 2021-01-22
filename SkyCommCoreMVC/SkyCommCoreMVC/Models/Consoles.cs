using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace SkyCommCoreMVC.Models
{
    public class Consoles
    {
        public string ConsoleId { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize<Consoles>(this);
        }
    }
}
