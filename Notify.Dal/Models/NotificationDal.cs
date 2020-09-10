using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Notify.Common.Enums;

namespace Notify.Dal.Models
{
	[Table("Notifications")]
	public abstract class NotificationDal
	{
		[Column("Id"), Key]
		public int Id { get; set; }
		[Column("NotificatorContactId")]
		public int NotificatorContactId { get; set; }

		[Column("Subject")]
		public string Subject { get; set; }
		[Column("Message")]
		public string Message { get; set; }
		[Column("TypeId")]
		public NotificationTypeEnum TypeId { get; set; }
		[Column("StatusId")]
		public NotificationStatusEnum StatusId { get; set; }


		[Column("CreatedAt")]
		public DateTime CreatedAt { get; set; }
		[Column("UpdatedAt")]
		public DateTime UpdatedAt { get; set; }

		public NotificationTypeDal Type { get; set; }
		public NotificationStatusDal Status { get; set; }
		public NotificationRequestDal Request { get; set; }
		public NotificatorContactDal NotificatorContact { get; set; }
	}
}
