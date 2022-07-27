
using Gameboard.Objects.Ranking;
using System.Collections.Generic;

namespace Gameboard.Objects.Engagement
{
    /// <summary>
    /// Once a winner is declared for the entire game session, 
    /// report the scores and ranks of each team/player
    /// </summary>
    [System.Serializable]
    public class RankingReportMetric: EngagementMetric
    {
        public override string eventType => "RANKING_REPORT";

        /// <summary>
        /// A current game report containing teams, scores, and the rank they placed in the game.
        /// </summary>
        public RankingReport rankingReport;

        public RankingReportMetric(RankingReport rankingReport)
        {
            this.rankingReport = rankingReport;
            this.userIds = new List<string>();
            foreach(RankingEntry entry in rankingReport.rankingEntries){
                foreach(string userId in entry.teamMembers){
                    this.userIds.Add(userId);
                }
            }
        }
    }
}
