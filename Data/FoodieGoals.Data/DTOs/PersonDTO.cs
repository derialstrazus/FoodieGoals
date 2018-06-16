using FoodieGoals.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodieGoals.Data.DTOs
{
    public class PersonDTO
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastEdited { get; set; }
        public IEnumerable<PersonListDTO> PersonLists { get; set; }
    }
}
