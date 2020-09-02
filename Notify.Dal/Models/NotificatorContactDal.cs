using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Notify.Dal.Models
{
	[Table("NotificatorContacts")]
	public class NotificatorContactDal
	{
		[Column("Id"), Key]
		public int Id { get; set; }
		[Column("ContactId")]
		public int ContactId { get; set; }
		[Column("NotificatorId")]
		public int NotificatorId { get; set; }

		[Column("IsActive")]
		public bool IsActive { get; set; }

		[Column("CreatedAt")]
		public DateTime CreatedAt { get; set; }
		[Column("UpdatedAt")]
		public DateTime UpdatedAt { get; set; }

		public ContactDal Contact { get; set; }
		public NotificatorDal Notificator { get; set; }
	}
}
