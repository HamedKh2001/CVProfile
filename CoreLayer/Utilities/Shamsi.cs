using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Utilities
{
	public static class Shamsi
	{
		public static string ToShamsi(this DateTime dateTime)
		{
			PersianCalendar PC = new PersianCalendar();
			return PC.GetYear(dateTime).ToString() + "/" + PC.GetMonth(dateTime).ToString("00") + "/" + PC.GetDayOfMonth(dateTime).ToString("00");
		}
		public static string ToShamsiDay(this DateTime dateTime)
		{
			PersianCalendar PC = new PersianCalendar();
			var day= PC.GetDayOfWeek(dateTime).ToString();
			switch (day)
			{
				case ("Saturday"):return "شنبه";				
				case ("Sunday"): return "یکشنبه";
				case ("Monday"): return "دوشنبه";
				case ("Tuseday"): return "سه شنبه";
				case ("wednesday"): return "چهارشنبه";
				case ("Thursday"): return "پنجشنبه";
				case ("Friday"): return "جمعه";
				default: return "";
			}
		}
	}
}
