using System.ComponentModel.DataAnnotations.Schema;
using Notify.Enums;

namespace Notify.Dal.Models
{
	[Table("NotificatorTypes")]
	public class NotificatorTypeDal
	{
		[Column("Id")]
		public NotificationTypeEnum Id { get; set; }
		[Column("Name")]
		public string Name { get; set; }
		[Column("Slug")]
		public string Slug { get; set; }
	}
}