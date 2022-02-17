using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Dtos
{
    public class OwnerDto
    {
		public string FullName { get; set; }
		public string Phonenumber { get; set; }
		public string ProfilePhoto { get; set; }
		public string City { get; set; }
		public string Languages { get; set; }
		public string About { get; set; }
		public string Email { get; set; }
		public DateTime BirthDate { get; set; }
		public bool IsActive { get; set; } = true;
	}
}
