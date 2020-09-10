using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Notify.Dal.Models
{
	[Table("ClientNotificators")]
	public class ClientNotificatorDal
	{
		[Column("Id"), Key]
		public int Id { get; set; }
		[Column("ClientId")]
		public int ClientId { get; set; }
		[Column("NotificatorId")]
		public int NotificatorId { get; set; }

		[Column("IsActive")]
		public bool IsActive { get; set; }

		[Column("CreatedAt")]
		public DateTime CreatedAt { get; set; }
		[Column("UpdatedAt")]
		public DateTime UpdatedAt { get; set; }

		public ClientDal Client { get; set; }
		public NotificatorDal Notificator { get; set; }
	}
}
