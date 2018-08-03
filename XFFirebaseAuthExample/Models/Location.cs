using System;
using System.Collections.Generic;
using System.Text;

namespace TouristEye.Models
{
    public class Location
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
