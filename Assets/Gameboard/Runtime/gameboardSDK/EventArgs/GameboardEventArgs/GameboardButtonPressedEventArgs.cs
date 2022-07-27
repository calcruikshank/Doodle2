namespace Gameboard.EventArgs
{
    [System.Serializable]
    public class GameboardButtonPressedEventArgs : GameboardIncomingEventArg
    {
        public GameboardButtonPressedEventArgs() { }

        public string buttonId;
    }
}