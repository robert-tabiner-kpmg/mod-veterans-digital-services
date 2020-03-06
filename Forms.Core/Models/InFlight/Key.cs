using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Forms.Core.Models.InFlight.Decision;
using Forms.Core.Models.InFlight.Physical;
using Forms.Core.Models.Static;
using Microsoft.VisualBasic;

namespace Forms.Core.Models.InFlight
{
    public class Key
    {
        public string Value { get; }

        public Key(string value)
        {
            Value = value;
        }

        public static Key EmptyKey()
            => new Key(string.Empty);
        
        public static Key ForDecisionFormRouter()
            => new Key(GetBytes(new [] { nameof(FormRouterFormNode) }));
        
        public static Key ForTaskList()
            => new Key(GetBytes(new [] { nameof(TaskListFormNode) }));

        public static Key ForDecisionTaskRouter(string taskId)
            => new Key(GetBytes(new[] {taskId, nameof(TaskRouterFormNode) }));
        
        public static Key ForDecisionTaskItemRouter(string taskId, int repeatIndex)
            => new Key(GetBytes(new[] {taskId, repeatIndex.ToString(), nameof(TaskItemRouterFormNode) }));

        public static Key ForTaskSummary(string taskId, int repeatIndex)
            => new Key(GetBytes(new [] { taskId, repeatIndex.ToString(), nameof(TaskSummaryFormNode) }));

        public static Key ForPreTaskPage(string taskId)
            => new Key(GetBytes(new [] { taskId, nameof(PreTaskFormNode) }));

        public static Key ForPostTaskPage(string taskId)
            => new Key(GetBytes(new [] { taskId, nameof(PostTaskFormNode) }));
        
        // This is used for both SubTaskRouters and TaskQuestionPages as they both sit at the same level in the structure
        public static Key ForTaskItemPage(string taskId, string itemId, IEnumerable<int> repeatIndices)
            => new Key(GetBytes(new [] { taskId, itemId, string.Join(',', repeatIndices), nameof(ITaskItem) }));
        
        public static Key ForSubTaskItemRouter(string taskId, string subTaskId, IEnumerable<int> repeatIndices)
            => new Key(GetBytes(new [] { taskId, subTaskId, string.Join(',', repeatIndices), nameof(SubTaskItemRouterFormNode) }));
        
        public static Key ForPostSubTaskPage(string taskId, string subTaskId, IEnumerable<int> repeatIndices)
            => new Key(GetBytes(new [] { taskId, subTaskId, string.Join(',', repeatIndices), nameof(PostSubTaskFormNode) }));
        
        public static Key ForPreSubTaskPage(string taskId, string subTaskId, IEnumerable<int> repeatIndices)
            => new Key(GetBytes(new [] { taskId, subTaskId, string.Join(',', repeatIndices), nameof(PreSubTaskFormNode) }));

        private static string GetBytes(string[] args)
        {
            return string.Join('-', args);
        }

        public override bool Equals(object obj)
        {
            if (obj is Key key)
                return Value.SequenceEqual(key.Value);

            return false;
        }

        public override int GetHashCode()
        {
            return Value != null ? Value.GetHashCode() : 0;
        }
    }
}