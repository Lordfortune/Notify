using System.Runtime.Serialization;

namespace Notify.Common.Dto
{
	[DataContract]
	public class SendMessageDto
	{
		[DataMember(Name = "contactId")]
		public int ContactId { get; set; }
		[DataMember(Name = "subject")]
		public string Subject { get; set; }
		[DataMember(Name = "message")]
		public string Message { get; set; }
		[DataMember(Name = "notificatorId")]
		public int NotificatorId { get; set; }
		[DataMember(Name = "notificatorSlug")]
		public string NotificatorSlug { get; set; }
	}
}
