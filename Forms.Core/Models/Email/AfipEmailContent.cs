namespace Forms.Core.Models.Email
{
    public class AfipEmailContent
    {
        [EmailContent("form_content")]
        public string FormContent { get; set; }
        
        [EmailContent("reference_number")]
        public string ReferenceNumber { get; set; }
        
        [EmailContent("consent_correspond_email")]
        public string ConsentCorrespondEmail { get; set; }
        
        [EmailContent("consent_DWP_benefits")]
        public string ConsentDwpBenefits { get; set; }
    }
}