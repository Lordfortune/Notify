using System.ComponentModel.DataAnnotations.Schema;

namespace Notify.Dal.Models.Email
{
	public class EmailContactDal : ContactDal
	{
		[Column("Email")]
		public string Email { get; set; }
	}
}
