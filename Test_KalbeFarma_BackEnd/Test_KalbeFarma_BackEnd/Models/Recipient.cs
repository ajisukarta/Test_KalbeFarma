using System;
using System.Collections.Generic;

namespace Test_KalbeFarma_BackEnd.Models
{
    public partial class Recipient
    {
        public Recipient()
        {
            Orders = new HashSet<Orders>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string States { get; set; }
        public string Country { get; set; }
        public string Image { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
    }
}
