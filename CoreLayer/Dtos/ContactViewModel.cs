using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreLayer.Dtos
{
	public class ContactDto
	{
		public string Subject { get; set; }
		public string ContactText { get; set; }
		public IFormFile File { get; set; }
		public string Email { get; set; }
	}
}
