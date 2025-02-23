﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreweryAPIClassLibrary.Models
{
    public class Beer
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public int BrewerId { get; set; }

        public static implicit operator Beer(List<Beer> v)
        {
            throw new NotImplementedException();
        }
    }
}
