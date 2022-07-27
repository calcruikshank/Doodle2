namespace Gameboard.EventArgs
{
    [System.Serializable]
    public class GameboardDropdownChangedEventArgs : GameboardIncomingEventArg
    {
        public GameboardDropdownChangedEventArgs() { }

        public string userId;
        public string dropDownId;
        public int newIndex;
    }
}