using System;
using System.Collections.Generic;

namespace SkyCommCoreMVC.Models
{
    public partial class Continents
    {
        public Continents()
        {
            Countries = new HashSet<Countries>();
        }

        public int ContinentId { get; set; }
        public string Continent { get; set; }

        public virtual ICollection<Countries> Countries { get; set; }
    }
}
