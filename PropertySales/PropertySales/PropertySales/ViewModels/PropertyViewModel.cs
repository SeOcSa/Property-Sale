using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PropertySales.ViewModels
{
    public class PropertyViewModel
    {
        public String Type { get; set; }
        public int Rooms { get; set; }
        public bool Kitchen { get; set; }
        public int Bathroom { get; set; }
        public int Surface { get; set; }
        public bool IsAvailable { get; set; }
        public int Price { get; set; }
        public string photoUrl { get; set; }
        public string photoUrl2 { get; set; }
        public string photoUrl3 { get; set; }
        public string photoUrl4 { get; set; }
    }
}
