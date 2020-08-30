using System.ComponentModel.DataAnnotations.Schema;

namespace Notify.Dal.Models
{
	public class TelegramNotificatorDal : NotificatorDal
	{
		[Column("TelegramSettingsId")]
		public int SettingsId { get; set; }

		public TelegramNotificatorSettingsDal Settings { get; set; }
	}
}