﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
	public class CalculateRateDTO
	{
		public int Id { get; set; }
		public int Star1 { get; set; }
		public int Star2 { get; set; }
		public int Star3 { get; set; }
		public int Star4 { get; set; }
		public int Star5 { get; set; }
		public float Average { get; set; }
		public float CountReview { get; set; }
	}
}
