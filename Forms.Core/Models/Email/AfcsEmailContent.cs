namespace Forms.Core.Models.Email
{
    public class AfcsEmailContent
    {
        [EmailContent("reference_number")]
        public string ReferenceNumber { get; set; }
        
        [EmailContent("personal_details")]
        public string PersonalDetails { get; set; }
        
        [EmailContent("service_details")]
        public string ServiceDetails { get; set; }
        
        [EmailContent("claim_details")]
        public string ClaimDetails { get; set; }
        
        [EmailContent("other_compensation")]
        public string OtherCompensation { get; set; }
        
        [EmailContent("other_benefits")]
        public string OtherBenefits { get; set; }
        
        [EmailContent("payment_details")]
        public string PaymentDetails { get; set; }
        
        [EmailContent("nominate_representative")]
        public string NominateRepresentative { get; set; }

        [EmailContent("consent_declaration")]
        public string ConsentDeclaration { get; set; }
        
        [EmailContent("welfare_agent_details")]
        public string WelfareAgentDetails { get; set; }
    }
}