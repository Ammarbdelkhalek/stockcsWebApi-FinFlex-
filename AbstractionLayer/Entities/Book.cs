using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractionLayer.Entities
{
    public class Book
    {
        public int Id { get; set; }//0
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public decimal Price { get; set; }
        public string Genre { get; set; }
        public DateTime PublicationDate { get; set; }
        public ICollection<Reviews> Reviews { get; set; } = new List<Reviews>();
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    } 
}
