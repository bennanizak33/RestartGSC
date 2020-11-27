using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace McDonalds.DAL
{
	public class ServerEvent
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
		public int ServerEventId { get; set; }

		public Event Event { get; set; }

		public DateTime Date { get; set; }

        public DateTime UpTimes { get; set; }

        public string Detail { get; set; }

        public virtual Restaurant Restaurant { get; set; }
	}
}