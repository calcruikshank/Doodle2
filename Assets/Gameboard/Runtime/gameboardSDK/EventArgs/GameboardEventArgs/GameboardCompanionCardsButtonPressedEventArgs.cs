using UnityEngine;

namespace Gameboard.EventArgs
{
    [System.Serializable]
    public class GameboardCompanionCardsButtonPressedEventArgs : GameboardCompanionButtonPressedEventArgs
    {
        public GameboardCompanionCardsButtonPressedEventArgs() { }

        public string selectedCardId;

        public override string ToString()
        {
            return JsonUtility.ToJson(this);
        }
    }
}