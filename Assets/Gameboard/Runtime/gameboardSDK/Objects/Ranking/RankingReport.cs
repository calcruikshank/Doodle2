using System;
using System.Collections.Generic;

namespace Gameboard.Objects.Ranking
{
    public enum RankingReportType
    {
        GameFinished = 0,
        GameStarted = 1,
        GameCancelled = 3,
        RoundEnd = 2,
    }

    public class RankingReport
    {
        /// <summary>
        /// The ID of the rankings to update
        /// </summary>
        /// <remarks>
        /// default ranking is 0
        /// </remarks>
        public int rankingId;

        /// <summary>
        /// The ID of the ranking report, this can be specified if the game desires to have multiple instances report the rankings.
        /// </summary>
        /// <remarks>
        /// A new guid will be assigned if none is specified during instantiation.
        /// </remarks>
        public Guid reportId;

        /// <summary>
        /// Determines if the data reported should be public or private
        /// </summary>
        /// <remarks>
        /// defaults to public (false/0)
        /// </remarks>
        public bool isPrivate;

        /// <summary>
        /// Contains a list of all of the entries to report
        /// </summary>
        public List<RankingEntry> rankingEntries;

        /// <summary>
        /// Report Type
        /// </summary>
        public RankingReportType type;

        /// <summary>
        /// Optional current round being reported
        /// </summary>
        public int? currentRound;
        
        public RankingReport(List<RankingEntry> rankingEntries, RankingReportType type, int? currentRound = null, Guid? reportId = null, int rankingId = 0, bool isPrivate = false)
        {
            this.reportId = reportId ?? Guid.NewGuid();
            this.type = type;
            this.rankingId = rankingId;
            this.isPrivate = isPrivate;
            this.rankingEntries = rankingEntries;
            this.currentRound = currentRound;
        }
    }
}
