using FoodieGoals.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodieGoals.Data.DTOs
{
    public class DTOFactory
    {

        public PersonDTO Create(Person person)
        {
            return new PersonDTO()
            {
                ID = person.ID,
                FirstName = person.FirstName,
                MiddleName = person.MiddleName,
                LastName = person.LastName,
                Email = person.Email,
                Address = person.Address,
                CreatedOn = person.CreatedOn,
                LastEdited = person.LastEdited,
                PersonLists = person.PersonLists.Select(pl => Create(pl)).ToList()
            };
        }


        public PersonListDTO Create(PersonList personList)
        {
            return new PersonListDTO()
            {
                ID = personList.ID,
                Title = personList.Title,
                Comments = personList.Comments,
                CreatedOn = personList.CreatedOn,
                LastEdited = personList.LastEdited
            };
        }

    }
}
