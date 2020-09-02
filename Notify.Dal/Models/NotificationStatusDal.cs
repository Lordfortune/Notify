using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Notify.Common.Enums;

namespace Notify.Dal.Models
{
	[Table("NotificationStatuses")]
	public class NotificationStatusDal
	{
		[Column("Id"), Key]
		public NotificationStatusEnum Id { get; set; }
		[Column("Name")]
		public string Name { get; set; }
		[Column("Slug")]
		public string Slug { get; set; }
		[Column("Description")]
		public string Description { get; set; }
	}
}
