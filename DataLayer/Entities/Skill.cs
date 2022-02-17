using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
	public class Skill: BaseEntity
	{
		[Display(Name ="نام :")]
		[Required(ErrorMessage = "{0} را وارد کنید")]
		public string Name { get; set; }
		[Display(Name = "درصد :")]
		[Required(ErrorMessage = "{0} را وارد کنید")]
		[Range(0,100,ErrorMessage ="مقدار وارد شده بین 0 و 100 باشد")]
		public byte PercentOfDominance { get; set; }
	}
}
