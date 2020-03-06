using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Forms.Core.Models.InFlight.Decision;
using Forms.Core.Models.InFlight.Physical;

namespace Forms.Core.Models.InFlight
{
    public class StaticKey
    {
        public string Value { get; }

        public StaticKey(string value)
        {
            Value = value;
        }
        
        public static StaticKey ForTaskList()
            => new StaticKey(GetBytes(new [] { nameof(TaskListFormNode) }));
        
        public static StaticKey ForPreTaskPage(string taskId)
            => new StaticKey(GetBytes(new [] { taskId, nameof(PreTaskFormNode) }));
        
        public static StaticKey ForTaskNode(string taskId)
            => new StaticKey(GetBytes(new [] { taskId, nameof(TaskRouterFormNode) }));

        public static StaticKey ForPostTaskPage(string taskId)
            => new StaticKey(GetBytes(new [] { taskId, nameof(PostTaskFormNode) }));
        
        public static StaticKey ForPreSubTaskPage(string taskId, string subTaskId)
            => new StaticKey(GetBytes(new [] { taskId, subTaskId, nameof(PreSubTaskFormNode) }));

        public static StaticKey ForPostSubTaskPage(string taskId, string subTaskId)
            => new StaticKey(GetBytes(new [] { taskId, subTaskId, nameof(PostSubTaskFormNode) }));

        public static StaticKey ForTaskSummary(string taskId)
            => new StaticKey(GetBytes(new [] { taskId, nameof(TaskSummaryFormNode) }));
        
        public static StaticKey ForTaskQuestionPage(string taskId, string pageId)
            => new StaticKey(GetBytes(new [] { taskId, pageId, nameof(TaskQuestionPageFormNode) }));
        
        public static StaticKey ForSubTask(string taskId, string subTaskId)
            => new StaticKey(GetBytes(new [] { taskId, subTaskId, nameof(SubTaskRouterFormNode) }));

        private static string GetBytes(string[] args)
        {
            using var hash = SHA1.Create();
            var hashedValue =  BitConverter.ToString(hash.ComputeHash(Encoding.UTF8.GetBytes(string.Join('-', args))));
            return hashedValue.Replace("-", string.Empty);
        }
        
        public override bool Equals(object obj)
        {
            if (obj is StaticKey key)
                return Value.SequenceEqual(key.Value);

            return false;
        }

        public override int GetHashCode()
        {
            return Value != null ? Value.GetHashCode() : 0;
        }
    }
}