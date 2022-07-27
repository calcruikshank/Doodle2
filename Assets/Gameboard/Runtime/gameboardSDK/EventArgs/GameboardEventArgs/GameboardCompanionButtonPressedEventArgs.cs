namespace Gameboard.EventArgs
{
    [System.Serializable]
    public class GameboardCompanionButtonPressedEventArgs : GameboardIncomingEventArg
    {
        public GameboardCompanionButtonPressedEventArgs() { }

        public string buttonId;
        public string callbackMethod;
    }
}