using System.Text.Json;

namespace SkyCommNet7MVC.Domain.Models
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
