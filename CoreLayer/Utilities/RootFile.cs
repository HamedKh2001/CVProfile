using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Utilities
{
	public static class RootFile
	{
		public static string InsertUserPhoto { get; set; } = "/wwwroot/User_Photos/";
		public static string ReadUserPhoto { get; set; } = "/User_Photos/";

		public static string ReadOrderFile { get; set; } = "/OrderFiles/";
		public static string InsertOrderFile { get; set; } = "/wwwroot/OrderFiles/";
		
		public static string ReadOwnerFile { get; set; } = "/OwnerFile/";
		public static string InsertOwnerFile { get; set; } = "/wwwroot/OwnerFile/";
	}
}
