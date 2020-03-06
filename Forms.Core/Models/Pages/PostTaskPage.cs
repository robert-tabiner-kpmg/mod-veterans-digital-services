namespace Forms.Core.Models.Pages
{
    public abstract class PostTaskPage : Page
    {
        public string Header { get; set; }
        public string Body { get; set; }
        public string RepeatLinkText { get; set; } = "Add another";
        public string SummaryTableHeader { get; set; } = "";
        public string SummaryTableText { get; set; } = "Item";
    }
}