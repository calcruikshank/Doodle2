namespace Gameboard.EventArgs
{
    [System.Serializable]
    public class GameboardMatZoneDropOccuredEventArgs : GameboardIncomingEventArg
    {
        public GameboardMatZoneDropOccuredEventArgs() { }

        public string userId;
        public string matZoneId;
        public string droppedObjectId;
    }
}