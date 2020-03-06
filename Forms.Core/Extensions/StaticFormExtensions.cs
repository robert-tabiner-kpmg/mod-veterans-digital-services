using System.Collections.Generic;
using Forms.Core.Models.Static;

namespace Forms.Core.Extensions
{
    public static class StaticFormExtensions
    {
        public static ITaskItem FindTaskItem(this Task task, string itemId)
        {
            foreach (var item in task.TaskItems)
            {
                if (itemId == item.Id) return item;
                
                if (item is SubTask nextSubTask)
                {
                    var subTaskResult =  CheckSubTask(nextSubTask, itemId);

                    if (subTaskResult != null) return subTaskResult;
                }
            }

            throw new KeyNotFoundException(itemId);
        }
        
        private static ITaskItem CheckSubTask(SubTask subTask, string itemId)
        {
            foreach (var subTaskItem in subTask.TaskItems)
            {
                if (itemId == subTaskItem.Id) return subTaskItem;

                if (subTaskItem is SubTask nextSubTask)
                {
                    var subTaskResult = CheckSubTask(nextSubTask, itemId);

                    if (subTaskResult != null) return subTaskResult;
                }
            }

            return null;
        }
    }
}