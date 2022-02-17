using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
	public class User : Person
	{
		public string ActivateCode { get; set; }
		public bool IsActive { get; set; }

		public virtual ICollection<Contact> Comments { get; set; }
	}
}
