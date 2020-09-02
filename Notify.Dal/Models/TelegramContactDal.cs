using System.ComponentModel.DataAnnotations.Schema;

namespace Notify.Dal.Models
{
	public class TelegramContactDal : ContactDal
	{
		[Column("Username")]
		public string Username { get; set; }
		[Column("FirstName")]
		public string FirstName { get; set; }
		[Column("LastName")]
		public string LastName { get; set; }
	}
}
