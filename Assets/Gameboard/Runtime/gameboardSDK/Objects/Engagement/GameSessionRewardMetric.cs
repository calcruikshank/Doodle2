using System;

// TODO: nothing for achievements has been implemented yet and will be added in the future:
//          Achievements can be Gameboard Achievements, Profile Assets, or Gameboard Experience Points (GXP).
//          Currently this class is not used anywhere.
//          The intent with this class is just a jumping off point for discussions, everything here will probably change.

namespace Gameboard.Objects.Engagement
{
    /// <summary>
    /// This call is made when a player(s) unlock a specific in-game reward (game ID, reward ID, player ID, date/time, score to unlock).  
    /// Achievements can be Gameboard Achievements, Profile Assets, or Gameboard Experience Points (GXP).
    /// </summary>
    [System.Serializable]
    class GameSessionRewardMetric : EngagementMetric
    {
        public override string eventType => "GAME_SESSION_REWARD";

        /// <summary>
        /// Id of the reward
        /// </summary>
        public string rewardId;

        /// <summary>
        /// DateTime the reward was given
        /// </summary>
        public DateTime timeRewarded;

        /// <summary>
        /// Score required before the reward is given
        /// </summary>
        public float scoreRequirement; // TODO: maybe this should just be a string explaining the requirements?        

        public GameSessionRewardMetric(string userId, string rewardId)
        {
            timeRewarded = DateTime.UtcNow;
            this.userIds.Add(userId);
            this.rewardId = rewardId;
        }
    }
}
