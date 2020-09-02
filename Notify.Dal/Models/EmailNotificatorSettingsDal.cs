using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Notify.Dal.Models
{
	[Table("EmailNotificatorSettings")]
	public class EmailNotificatorSettingsDal
	{
		[Column("Id")]
		public int Id { get; set; }
		[Column("Port")]
		public int Port { get; set; }
		[Column("Host")]
		public string Host { get; set; }
		[Column("UserName")]
		public string UserName { get; set; }
		[Column("Password")]
		public string Password { get; set; }
		[Column("SenderName")]
		public string SenderName { get; set; }
		[Column("SenderAddress")]
		public string SenderAddress { get; set; }
		[Column("DefaultSubject")]
		public string DefaultSubject { get; set; }

		[Column("IsActive")]
		public bool IsActive { get; set; }

		[Column("CreatedAt")]
		public DateTime CreatedAt { get; set; }
		[Column("UpdatedAt")]
		public DateTime UpdatedAt { get; set; }

		public EmailNotificatorDal Notificator { get; set; }
	}
}
