namespace Forms.Web.Models.FormComponents
{
    public class RadioInput
    {
        public string Id { get; set; }
        public string Label { get; set; }
        public string Hint { get; set; }
        public string Error { get; set; }
        public bool ShowInline { get; set; }
        public bool Small { get; set; }
        public string[] Values { get; set; }
    }
}