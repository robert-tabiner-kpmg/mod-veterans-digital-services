using System.Collections.Generic;
using System.Linq;

namespace Forms.Core.Models.InFlight
{
    public class Answer
{
    public Dictionary<string, string> Values { get; set; } = new Dictionary<string, string>();
    public AnswerType AnswerType { get; set; }
    public string AnswerAsString
    {
        get
        {
            if (Values.Any())
            {
                switch (AnswerType)
                {
                    case AnswerType.Date:
                        return Values.All(x => x.Value == null) ? null : $"{Values["day"]}/{Values["month"]}/{Values["year"]}";
                    case AnswerType.ConditionalTextInput:
                        return Values.All(x => x.Value == null)
                            ? null
                            : $"{Values["radio"]} {(string.IsNullOrEmpty(Values["text"]) ? "" : $" - {Values["text"]}")}";
                    case AnswerType.Checkbox:
                        return Values.All(x => x.Value == null) ? null : string.Join(", ", Values.Values);
                    default:
                        return Values["default"];
                }
            }

            return null;
        }
    }
}
}