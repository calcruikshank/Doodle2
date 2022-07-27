using Gameboard.Objects.Dice;
using UnityEngine;

namespace Gameboard.EventArgs
{
    [System.Serializable]
    public class CompanionDiceRollEventArgs : CompanionMessageResponseArgs
    {
        public CompanionDiceRollEventArgs() { }

        public string id;
        public DieGroup[] dice;
        public int[] diceSizesRolledList = new int[0];
        public int addedModifier = 0;
        public string diceNotation;

        public override string ToString()
        {
            return JsonUtility.ToJson(this);
        }
    }
}