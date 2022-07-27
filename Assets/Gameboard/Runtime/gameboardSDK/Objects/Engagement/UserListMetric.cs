using System.Collections.Generic;

namespace Gameboard.Objects.Engagement
{
    /// <summary>
    /// Users that have entered the lobby and are active in a game session (not just having user presence on the board)
    /// </summary>
    [System.Serializable]
    public class UserListMetric : EngagementMetric
    {
        /// <summary>
        /// A List of all of the user ids participating in the game session.
        /// </summary>
        public List<string> userIds;
        public override string eventType => "PLAYER_IDS_IN_SESSION";

        public UserListMetric(List<string> userIds)
        {
            this.userIds = userIds;
        }
    }
}
