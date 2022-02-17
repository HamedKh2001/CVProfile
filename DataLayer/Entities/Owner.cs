using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
	public class Owner: Person
	{
		public string City { get; set; }
		public string Languages { get; set; }
		public string About { get; set; }
		public string Email { get; set; }
		public DateTime BirthDate { get; set; }
		public bool IsActive { get; set; } = true;
	}
}
