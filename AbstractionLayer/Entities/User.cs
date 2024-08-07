using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AbstractionLayer.Entities
{
    public class User
    {
        [Required]
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Reviews> Reviews { get; set; }
    }
}
