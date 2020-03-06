namespace Forms.Core.Options
{
    public class EmailTemplateOptions
    {
        public string AFIPTemplateId { get; set; }
        public string AFIPUserTemplateId { get; set; }
        public string AFCSTemplateId { get; set; }
        
        public string AFCSUserTemplateId { get; set; }
        
        public string AFIPEmailRecipient { get; set; }
        public string AFCSEmailRecipient { get; set; }
        public string DevelopmentUserEmailRecipient { get; set; }
    }
}