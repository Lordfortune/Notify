using System.ComponentModel.DataAnnotations.Schema;

namespace Notify.Dal.Models.Telegram
{
	public class TelegramGroupChatDal : TelegramChatDal
	{
		[Column("Title")]
		public string Title { get; set; }
	}
}
