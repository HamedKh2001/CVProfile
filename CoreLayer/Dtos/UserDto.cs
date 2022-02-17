using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataLayer.Entities.Person;

namespace CoreLayer.Dtos
{
	public class UserDto
	{
		public int Id { get; set; }
		public bool IsActive { get; set; }
		public DataLayer.Entities.Person.Roles Role { get; set; }
		public string FullName { get; set; }
	}
}
