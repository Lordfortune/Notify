using System.ComponentModel.DataAnnotations.Schema;
using Notify.Common.Enums;

namespace Notify.Dal.Models
{
	[Table("Notificators")]
	public abstract class NotificatorDal
	{
		[Column("Id")]
		public int Id { get; set; }
		[Column("Name")]
		public string Name { get; set; }
		[Column("Slug")]
		public string Slug { get; set; }
		[Column("TypeId")]
		public NotificationTypeEnum TypeId { get; set; }
	}
}
