using System.Runtime.Serialization;

namespace Notify.Dto
{
	[DataContract]
	public class NotificationRequestDto
	{
		[DataMember(Name = "Token")]
		public string ClientToken { get; set; }
		[DataMember(Name = "ContactId")]
		public int ContactId { get; set; }
		[DataMember(Name = "Notificator")]
		public string Notificator { get; set; }

		[DataMember(Name = "Subject")]
		public string Subject { get; set; }
		[DataMember(Name = "Message")]
		public string Message { get; set; }
	}
}
