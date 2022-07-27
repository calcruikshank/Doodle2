namespace Gameboard.EventArgs
{
    [System.Serializable]
    public class GameboardCardPlayedEventArgs : GameboardIncomingEventArg
    {
        public GameboardCardPlayedEventArgs() { }

        public string cardId;
        public string userId;
    }
}