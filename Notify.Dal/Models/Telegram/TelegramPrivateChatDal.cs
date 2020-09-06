using System.ComponentModel.DataAnnotations.Schema;

namespace Notify.Dal.Models.Telegram
{
	public class TelegramPrivateChatDal : TelegramChatDal
	{
		[Column("ContactPrivateId")]
		public int ContactPrivateId { get; set; }
		[Column("Username")]
		public string Username { get; set; }
		[Column("FirstName")]
		public string FirstName { get; set; }
		[Column("LastName")]
		public string LastName { get; set; }
	}
}
