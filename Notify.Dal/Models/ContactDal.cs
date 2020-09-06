using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Notify.Common.Enums;

namespace Notify.Dal.Models
{
	[Table("Contacts")]
	public abstract class ContactDal
	{
		[Column("Id"), Key]
		public int Id { get; set; }
		[Column("Name")]
		public string Name { get; set; }
		[Column("PersonId")]
		public int? PersonId { get; set; }
		[Column("TypeId")]
		public NotificationTypeEnum TypeId { get; set; }

		[Column("IsActive")]
		public bool IsActive { get; set; }

		[Column("CreatedAt")]
		public DateTime CreatedAt { get; set; }
		[Column("UpdatedAt")]
		public DateTime UpdatedAt { get; set; }

		public PersonDal Person { get; set; }
		public NotificationTypeDal Type { get; set; }
		public List<NotificatorContactDal> NotificatorContacts { get; set; }
	}
}
