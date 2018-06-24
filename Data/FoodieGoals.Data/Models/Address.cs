using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodieGoals.Data.Models
{
    public class Address
    {
        public int ID { get; set; }

        public string Company { get; set; }
        public string StreetLine1 { get; set; }
        public string StreetLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }

        [NotMapped]
        public string AddressString { get {

                var returnString = StreetLine1;
                
                returnString += string.IsNullOrWhiteSpace(StreetLine2) ? "" : ", " + StreetLine2;
                returnString += string.IsNullOrWhiteSpace(City) ? "" : ", " + City;
                returnString += string.IsNullOrWhiteSpace(State) ? "" : ", " + State;

                return returnString;
            }
        }
    }
}
