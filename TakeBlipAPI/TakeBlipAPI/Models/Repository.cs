using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TakeBlipAPI.Models
{
    public class Repository
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public DateTime Created_at { get; set; }
        public string Avatar_url { get; set; }
    }
}
