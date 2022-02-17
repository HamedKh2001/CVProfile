using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
	public class Contact: BaseEntity
	{
		public int UserID { get; set; }
		public string Subject { get; set; }
		public string ContactText { get; set; }
		public string FileName { get; set; }
		public string Email { get; set; }
		public bool IsApprove { get; set; } = false;
		public CommentLevel Lvl { get; set; } = CommentLevel.Private;


		public virtual User User { get; set; }
		public enum CommentLevel
		{
			Public,
			Private,
			VIP
		};
	}
}
