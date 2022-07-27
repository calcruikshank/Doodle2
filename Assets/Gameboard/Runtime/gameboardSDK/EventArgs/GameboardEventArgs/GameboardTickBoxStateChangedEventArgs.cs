namespace Gameboard.EventArgs
{
    [System.Serializable]
    public class GameboardTickBoxStateChangedEventArgs : GameboardIncomingEventArg
    {
        public GameboardTickBoxStateChangedEventArgs() { }

        public string userId;
        public string tickboxId;
        public DataTypes.TickboxStates newState;
    }
}
