using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Notify.Dal.Models
{
	[Table("Clients")]
	public class ClientDal
	{
		[Column("Id")]
		public int Id { get; set; }
		[Column("Name")]
		public string Name { get; set; }
		[Column("Slug")]
		public string Slug { get; set; }
		[Column("Token")]
		public string Token { get; set; }

		[Column("IsActive")]
		public bool IsActive { get; set; }

		[Column("CreatedAt")]
		public DateTime CreatedAt { get; set; }
		[Column("UpdatedAt")]
		public DateTime UpdatedAt { get; set; }

		public List<ClientNotificatorDal> ClientNotificators { get; set; }
	}
}
