namespace Forms.Core.Models.Email
{
    public class UserEmailContent
    {
        [EmailContent("reference_number")]
        public string ReferenceNumber { get; set; }
    }
}