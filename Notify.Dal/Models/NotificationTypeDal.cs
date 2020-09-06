using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Notify.Common.Enums;

namespace Notify.Dal.Models
{
	[Table("NotificationTypes")]
	public class NotificationTypeDal
	{
		[Column("Id"), Key]
		public NotificationTypeEnum Id { get; set; }
		[Column("Name")]
		public string Name { get; set; }
		[Column("Slug")]
		public string Slug { get; set; }

		public List<ContactDal> Contacts { get; set; }
		public List<NotificatorDal> Notificators { get; set; }
		public List<NotificationDal> Notifications { get; set; }
	}
}
