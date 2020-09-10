using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Notify.Dal.Models
{
	[Table("NotificationRequests")]
	public class NotificationRequestDal
	{
		[Column("Id"), Key]
		public int Id { get; set; }
		[Column("ContactId")]
		public int ContactId { get; set; }
		[Column("Notificator")]
		public string Notificator { get; set; }
		[Column("ClientToken"), MaxLength(200)]
		public string ClientToken { get; set; }
		[Column("Method"), MaxLength(100)]
		public string Method { get; set; }

		[Column("Subject"), MaxLength(250)]
		public string Subject { get; set; }
		[Column("Message")]
		public string Message { get; set; }

		[Column("NotificationId")]
		public int? NotificationId { get; set; }

		[Column("IsSuccess")]
		public bool? IsSuccess { get; set; }
		[Column("Comment")]
		public string Comment { get; set; }

		[Column("CreatedAt"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public DateTime CreatedAt { get; set; }
		[Column("UpdatedAt"), DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public DateTime UpdatedAt { get; set; }

		public NotificationDal Notification { get; set; }
	}
}
