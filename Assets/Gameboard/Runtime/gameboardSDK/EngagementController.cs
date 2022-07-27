using UnityEngine;
using Newtonsoft.Json;
using Gameboard.Objects.Engagement;
using System.Collections.Generic;

namespace Gameboard
{
    [RequireComponent(typeof(Gameboard))]
    public class EngagementController : MonoBehaviour
    {
        private Gameboard gameboard => Gameboard.Instance;

        private void Awake()
        {

        }

        private void Start()
        {
            
        }

        private void SendEngagementMetric<T>(T metric) where T : EngagementMetric
        {
            string extras = JsonConvert.SerializeObject(metric);
            GameboardLogging.Verbose($"sending engagement metric extras:\n{extras}");
            gameboard.Gameboardlytics_SendEvent(metric.eventType, extras);
        }

        /// <summary>
        ///  Players have entered the lobby system and have have started a game.
        ///  This should be called each time a new game (game session) is started, not just at the launch of the game APK
        /// </summary>
        public void SendGameSessionStarted(List<string> userIds)
        {
            GameSessionStartMetric sessionStartMetric = new GameSessionStartMetric(userIds);
            SendEngagementMetric(sessionStartMetric);
        }

        /// <summary>
        /// Users that have entered the lobby and are active in a game session (not just having user presence on the board)
        /// </summary>
        /// <param name="userIds"></param>
        public void SendUserIdsInSession(List<string> userIds)
        {
            UserListMetric userListMetric = new UserListMetric(userIds);
            SendEngagementMetric(userListMetric);
        }

        /// <summary>
        /// Send a ranking of a game. Could be in progress or finished based on the type passed.
        /// </summary>
        /// <remarks>
        /// The ranking report includes a list of RankingEntry,
        /// the RankingEntry contains the rank and score the team achieved as well a list of team members
        /// </remarks>
        public void SendRankingReport(RankingReportMetric rankingReport)
        {
            SendEngagementMetric(rankingReport);
        }
    }
}
