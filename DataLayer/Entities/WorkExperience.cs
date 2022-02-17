using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
	public class WorkExperience:BaseEntity
	{
		public string Subject { get; set; }
		public string Text { get; set; }
		public DateTime FromDate { get; set; }
		public DateTime ToDate { get; set; }
	}
}
