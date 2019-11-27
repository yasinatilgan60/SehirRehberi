using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SehirRehberi_API.Models
{
    public class City
    {
        public City()
        {
            Photos = new List<Photo>(); // Liste tip olduğu için buradan başlatmak gerekmektedir.
        }
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        // Tablolar arasındaki ilişkiyi sağlamak için;
        public List<Photo> Photos { get; set; }
        public User User { get; set; }
    }
}
