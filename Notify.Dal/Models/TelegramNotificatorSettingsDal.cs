using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Notify.Dal.Models
{
	[Table("TelegramNotificatorSettings")]
	public class TelegramNotificatorSettingsDal
	{
		[Column("Id")]
		public int Id { get; set; }
		[Column("Token")]
		public string Token { get; set; }

		[Column("IsActive")]
		public bool IsActive { get; set; }

		[Column("CreatedAt")]
		public DateTime CreatedAt { get; set; }
		[Column("UpdatedAt")]
		public DateTime UpdatedAt { get; set; }

		public TelegramNotificatorDal Notificator { get; set; }
	}
}
