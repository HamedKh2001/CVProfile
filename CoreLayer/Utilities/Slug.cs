using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Utilities
{
	public static class Slug
	{
		public static string ToSlug(this string Slug)
		{
			return (Slug.Trim().ToLower()
				.Replace(" ", "-")
				.Replace("!", "")
				.Replace("×", "")
				.Replace("@", "")
				.Replace("#", "")
				.Replace("$", "")
				.Replace("%", "")
				.Replace("^", "")
				.Replace("&", "")
				.Replace("*", "")
				.Replace(")", "")
				.Replace("(", "")
				.Replace("=", "")
				.Replace("+", "")
				.Replace(":", "")
				.Replace(";", "")
				.Replace("/", "")
				.Replace(">", "")
				.Replace("<", "")
				.Replace("{", "")
				.Replace("}", "")
				);
		}
	}
}
