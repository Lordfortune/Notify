using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Notify.Dal.Models
{
	[Table("Persons")]
	public class PersonDal
	{
		[Column("Id"), Key]
		public int Id { get; set; }
		[Column("FirstName")]
		public string FirstName { get; set; }
		[Column("LastName")]
		public string LastName { get; set; }
		[Column("MiddleName")]
		public string MiddleName { get; set; }

		[Column("IsActive")]
		public bool IsActive { get; set; }

		[Column("CreatedAt")]
		public DateTime CreatedAt { get; set; }
		[Column("UpdatedAt")]
		public DateTime UpdatedAt { get; set; }
	}
}
