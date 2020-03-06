namespace Forms.Web.Models.ViewModels
{
    public class BackButtonProps
    {
        public BackButtonProps(string currentNodeId, bool showBackButton)
        {
            CurrentNodeId = currentNodeId;
            ShowBackButton = showBackButton;
        }
        
        public string CurrentNodeId { get; }
        public bool ShowBackButton { get; }
    }
}