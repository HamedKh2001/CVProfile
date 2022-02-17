using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
	public class Person :BaseEntity
	{
		public string FullName { get; set; }
		public string Phonenumber { get; set; }
		
		public Roles Role { get; set; }
		public string ProfilePhoto { get; set; }
		public string PassWord { get; set; }

		public enum Roles
		{
			Owner,
			User
		}
	}
}
