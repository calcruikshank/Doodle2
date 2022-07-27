using System.Collections.Generic;

namespace Gameboard.Objects.Ranking
{
    public class RankingEntry
    {
        /// <summary>
        /// A list of the userId's contained in this team
        /// </summary>
        public List<string> teamMembers;

        /// <summary>
        /// The current rank/position that the team achieved in the game
        /// </summary>
        /// <remarks>
        /// 1 being best/first by default
        /// </remarks>
        public int rank;

        /// <summary>
        /// The score the team received in the game.
        /// </summary>
        /// <remarks>
        /// This could be used as many different things, including time, 
        /// and the specific type being used would need to be specified when the ranking is initially created.
        /// 
        /// The meaning and type of this score is defined when the ranking is created.
        /// 
        /// Higher is better by default.
        /// </remarks>
        public double score;

        public RankingEntry(List<string> teamMembers, int rank, double score)
        {
            this.teamMembers = teamMembers;
            this.rank = rank;
            this.score = score;
        }
    }
}
