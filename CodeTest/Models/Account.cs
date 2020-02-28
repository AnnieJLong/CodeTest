using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTest.Models
{
    public class Account
    {
        public int Id { get; set; }        
        public string IBAN { get; set; }
        public double Balance { get; set; }
        public int Customer_Id { get; set; }
    }
}
