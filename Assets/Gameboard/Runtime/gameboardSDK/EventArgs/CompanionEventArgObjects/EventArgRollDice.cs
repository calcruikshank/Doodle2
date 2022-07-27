using Gameboard.Objects.Dice;
using System;

namespace Gameboard.EventArgs
{
    public class EventArgRollDice
    {
        /// <summary>
        /// user who initiated the roll
        /// </summary>
        public string ownerId;

        /// <summary>
        /// id associated with the roll
        /// </summary>
        public string id;

        [Obsolete("Use DieGroup for additional future features.")]
        public int[] diceSizesToRoll;

        public DieGroup[] dice;

        /// <summary>
        /// Overall modifier added to the roll
        /// </summary>
        public int addedModifier;
        
        [Obsolete("Use DieGroup for additional future features.")]
        public string diceTintHexColor;

        public string diceNotation;
        //public string[] orderedDiceTextureUIDs; // Phase 2
        //public string[] customDiceUIDs; // Phase 4
    }
}