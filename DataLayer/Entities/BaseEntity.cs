﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
	public class BaseEntity
	{
		public int Id { get; set; }
		public DateTime CreationDate { get; set; } = DateTime.Now;
	}
}
