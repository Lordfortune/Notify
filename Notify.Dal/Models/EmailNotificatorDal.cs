using System.ComponentModel.DataAnnotations.Schema;

namespace Notify.Dal.Models
{
	public class EmailNotificatorDal : NotificatorDal
	{
		[Column("EmailSettingsId")]
		public int SettingsId { get; set; }

		public EmailNotificatorSettingsDal Settings { get; set; }
	}
}