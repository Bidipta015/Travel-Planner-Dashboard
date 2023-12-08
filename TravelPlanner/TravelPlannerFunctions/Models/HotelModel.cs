using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelPlannerFunctions.Models
    {
    internal class HotelModel
        {        
            public int Id { get; set; }
            public string Place { get; set; }
            public decimal Price { get; set; }
            public int Bedrooms { get; set; }
            public double Ratings { get; set; }
        }
    }
