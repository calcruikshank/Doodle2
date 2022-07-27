using Gameboard.Objects.Dice;
using UnityEngine;

namespace Gameboard.EventArgs
{
    [System.Serializable]
    public class GameboardDiceRolledEventArgs : GameboardIncomingEventArg
    {
        public GameboardDiceRolledEventArgs() { }

        public string id;
        public DieGroup[] dice;
        public int[] diceSizesRolledList;
        public int addedModifier;
        public string diceNotation;

        public override string ToString()
        {
            return JsonUtility.ToJson(this);
        }
    }
}