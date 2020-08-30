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

		public TelegramNotificatorDal Notificator { get; set; }
	}
}