using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P8.Server.Controllers
{
    public class Cloth /// Model
    {
        public int Price {get ; set ;}
        public string Name{get ;set ;}
        public int Id {get ; set ;}
        public string Color {get ;set ;}
        public override string ToString()
        {
            return $"{this.Name}, {this.Price}, {this.Id}, {this.Color}";
            
        }
        public override int GetHashCode()
        {
            return this.Id ;
        }
        public override bool Equals(object obj)
        {
            var otherCloth = obj as Cloth;
            if (obj is null) return false;
            return this.Id == otherCloth.Id;
        }
    }
}