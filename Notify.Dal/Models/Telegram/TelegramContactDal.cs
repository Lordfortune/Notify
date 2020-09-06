using System.ComponentModel.DataAnnotations.Schema;

namespace Notify.Dal.Models.Telegram
{
	public class TelegramContactDal : ContactDal
	{
		[Column("ChatId")]
		public int ChatId { get; set; }
		[Column("TelegramId")]
		public long TelegramId { get; set; }
		
		public TelegramChatDal Chat { get; set; }
	}
}
