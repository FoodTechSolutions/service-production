using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BackgroundServices.Models
{
    public class CreateProductionModel
    {
        public string Order { get; set; }
        public string Customer { get; set; }
        public IEnumerable<Item> Items { get; set; }


        public class Item
        {
            public string Name { get; set; }
            public IEnumerable<Ingredient> Ingredients { get; set; }
        }
    }
}
