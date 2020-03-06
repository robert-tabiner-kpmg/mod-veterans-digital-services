using System.Collections.Generic;
using System.Text;
using Forms.Core.Models.Display;

namespace Forms.Core.Extensions
{
    public static class TaskGroupingExtensions
    {
        public static List<TaskGrouping> FlattenTaskGroupings(this TaskGrouping taskGrouping)
        {
            var result = new List<TaskGrouping>
            {
                taskGrouping
            };
            
            var newList = new List<BaseDisplayItem>();
            for (var i = 0; i < taskGrouping.Items.Count; i++)
            {
                var item = taskGrouping.Items[i];

                if (item is TaskDisplayItem)
                {
                    newList.Add(item);
                    continue;
                }
                if (item is TaskGrouping group)
                {
                    result.AddRange(FlattenTaskGroupings(group));
                }
            }

            taskGrouping.Items = newList;
            
            return result;
        }
        
        public static string BuildFormString(IList<TaskGrouping> taskGroupings)
        {
            var builder = new StringBuilder();

            foreach (var grouping in taskGroupings)
            {
                var newGroupings = grouping.FlattenTaskGroupings();
                foreach (var flattenedGrouping in newGroupings)
                {
                    builder.AppendLine(BuildTaskString(flattenedGrouping));
                }
            }

            return builder.ToString();
        }
        
        public static string BuildTaskString(TaskGrouping grouping)
        {
            var builder = new StringBuilder();
            builder.AppendLine("---");
            builder.AppendLine(grouping.Name);
            string lastItemHeader = null;
            foreach (var item in grouping.Items)
            {
                if (item is TaskGrouping group)
                {
                    builder.AppendLine(BuildTaskString(group));
                    lastItemHeader = null;
                }

                if (item is TaskDisplayItem displayItem)
                {
                    if (lastItemHeader != displayItem.Header)
                    {
                        lastItemHeader = displayItem.Header;
                        builder.AppendLine(displayItem.Header);
                        builder.AppendLine(" ");
                    }

                    if (!string.IsNullOrEmpty(displayItem.QuestionText))
                    {
                        builder.AppendLine("^ " + displayItem.QuestionText);
                    }
                    
                    builder.AppendLine("^ " + (displayItem.Question?.Answer?.AnswerAsString ?? "None provided"));
                    builder.AppendLine(" ");
                }
            }
            
            return builder.ToString();
        }
    }
}