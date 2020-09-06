using System.ComponentModel.DataAnnotations.Schema;
using Notify.Common.Enums;

namespace Notify.Dal.Models.Telegram
{
	public abstract class TelegramChatDal
	{
		[Column("Id")]
		public int Id { get; set; }
		[Column("TelegramId")]
		public long TelegramId { get; set; }
		[Column("ChatTypeId")]
		public TelegramChatTypeEnum ChatTypeId { get; set; }

		public TelegramContactDal Contact { get; set; }
		public TelegramChatTypeDal ChatType { get; set; }
	}
}
