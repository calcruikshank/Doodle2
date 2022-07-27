using System.Collections.Generic;

namespace Gameboard.Objects.Engagement
{
    [System.Serializable]
    public abstract class EngagementMetric
    {
        public abstract string eventType { get; }

        /// <summary>
        /// A List of all of the user ids participating in the game session.
        /// </summary>
        public List<string> userIds { get; set; }
    }
}
