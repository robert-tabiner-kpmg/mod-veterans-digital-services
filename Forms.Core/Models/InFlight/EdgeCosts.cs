namespace Forms.Core.Models.InFlight
{
    public static class EdgeCosts
    {
        // We apply a heavy cost to this relationship as it forces the path-finding algorithms to traverse across all the sub-tasks instead
        // of shortcutting across this relationship and skipping the subtask
        public const int SubTaskToPostSubTaskCost = int.MaxValue;
        public const int TaskToPostTaskCost = int.MaxValue;
    }
}