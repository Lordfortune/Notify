using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Notify.Common.Enums;

namespace Notify.Dal.Models.Telegram
{
	[Table("TelegramChatTypes")]
	public class TelegramChatTypeDal
	{
		[Column("Id"), Key]
		public TelegramChatTypeEnum Id { get; set; }
		[Column("Name")]
		public string Name { get; set; }
		[Column("Slug")]
		public string Slug { get; set; }

		public List<TelegramChatDal> Chats { get; set; }
	}
}
