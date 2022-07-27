using Gameboard.EventArgs;
using System.Collections.Generic;
using UnityEngine;

public class UserPresenceTest : MonoBehaviour
{
    private List<UserPresencePlayer> playerList = new List<UserPresencePlayer>();
    private List<string> onScreenLog = new List<string>();

    void Start()
    {
        GameObject presenceObserverObj = GameObject.FindWithTag("UserPresenceObserver");
        UserPresenceObserver userPresenceObserver = presenceObserverObj.GetComponent<UserPresenceObserver>();
        userPresenceObserver.OnUserPresence += OnUserPresence;
    }

    void OnUserPresence(GameboardUserPresenceEventArgs userPresence)
    {
        // If the user doesn't exist in our player list, add them now.
        if (playerList.Find(s => s.gameboardId == userPresence.userId) == null)
        {
            UserPresencePlayer testPlayer = new UserPresencePlayer()
            {
                gameboardId = userPresence.userId
            };

            playerList.Add(testPlayer);

            AddToLog("--- === New player added: " + testPlayer.gameboardId);
        }

        switch (userPresence.changeValue)
        {
            case Gameboard.DataTypes.UserPresenceChangeTypes.UNKNOWN:
            break;

            case Gameboard.DataTypes.UserPresenceChangeTypes.ADD:
                AddToLog($"--- Presence Update of ADD for user " + userPresence.userId);
            break;

            case Gameboard.DataTypes.UserPresenceChangeTypes.REMOVE:
                AddToLog($"--- Presence Update of REMOVE for user " + userPresence.userId);
            break;

            case Gameboard.DataTypes.UserPresenceChangeTypes.CHANGE:
                string changeString = $"--- Presence Update of CHANGE for user {userPresence.userId}";
                if(!string.IsNullOrEmpty(userPresence.tokenColor))
                {
                    changeString += $" - New Token Color of {userPresence.tokenColor}";
                }

                if(!string.IsNullOrEmpty(userPresence.userName))
                {
                    changeString += $"- New name of {userPresence.userName}";
                }

                AddToLog(changeString);
            break;

            case Gameboard.DataTypes.UserPresenceChangeTypes.CHANGE_POSITION:
                AddToLog($"--- Presence Update of CHANGE_POSITION for user {userPresence.userName} with Screen Position of {userPresence.boardUserPosition.screenPosition}");
            break;
        }
    }

    void AddToLog(string logMessage)
    {
        onScreenLog.Add(logMessage);
        Debug.Log(logMessage);
    }

    void OnGUI()
    {
        foreach (string thisString in onScreenLog)
        {
            GUILayout.Label(thisString);
        }
    }

    private class UserPresencePlayer
    {
        public string gameboardId;
    }
}
