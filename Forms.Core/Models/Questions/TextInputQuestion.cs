namespace Forms.Core.Models.Questions
{
    public class TextInputQuestion : BaseQuestion
    {
        public string Type { get; set; } = "Text";
        public string Autocomplete { get; set; }
        public int Width { get; set; }
        public bool SpellCheck { get; set; }
        public string InputMode { get; set; }
    }
}