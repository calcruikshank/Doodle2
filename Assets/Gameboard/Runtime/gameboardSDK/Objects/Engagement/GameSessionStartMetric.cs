using System.Collections.Generic;

namespace Gameboard.Objects.Engagement
{
    /// <summary>
    ///  Players have entered the lobby system and have have started a game.
    ///  This should be called each time a new game (game session) is started, not just at the launch of the game APK
    /// </summary>
    [System.Serializable]
    public class GameSessionStartMetric : EngagementMetric
    {
        public override string eventType => "GAME_SESSION_STARTED";

        public GameSessionStartMetric(List<string> userIds)
        {
            this.userIds = userIds;
        }
    }
}
