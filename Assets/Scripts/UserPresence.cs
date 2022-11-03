using UnityEngine;
using Gameboard.EventArgs;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Gameboard
{
    public class UserPresence : MonoBehaviour
    {
        public static UserPresence instance;
        public UserPresenceController userPresenceController;
        public GameObject gameboardObject;
        [SerializeField] public List<Transform> playerSpawnLocations;
        public Dictionary<string, Transform> playersInScene = new Dictionary<string, Transform>();
        List<Transform> orderedList = new List<Transform>();

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(this);
            }
            instance = this;
            DontDestroyOnLoad(this);
            userPresenceController.OnUserPresence += OnUserPresence;
            //  Gameboard.singleton.companionController.SetDrawersHidden();


        }
        private void Start()
        {
            GetUsers();
        }
        public async void GetUsers()
        {

#if UNITY_EDITOR
            return;
#endif


            CompanionUserPresenceEventArgs companionUserPresence = await userPresenceController.GetCompanionUserPresence();
            List<GameboardUserPresenceEventArgs> userPresence = companionUserPresence.playerPresenceList;

            if (userPresence.Count > 0)
            {
                foreach (GameboardUserPresenceEventArgs thisPresence in userPresence)
                {
                    OnUserPresence(thisPresence);
                }
            }


        }

        List<GameObject> instObjs = new List<GameObject>();
        private void OnUserPresence(GameboardUserPresenceEventArgs userPresence)
        {
            for (int i = 0; i < instObjs.Count; i++)
            {
                Debug.Log("destroying iobject at  +" + i);
                Destroy(instObjs[i].gameObject);
            }
            
            string userID = userPresence.userId; 
           
            if (playersInScene.ContainsKey(userID))
            {
                playersInScene[userID].gameObject.SetActive(false);
                playersInScene.Remove(userID);
            }

            if (userPresence.changeValue != DataTypes.UserPresenceChangeTypes.REMOVE)
            {
                if (userPresence.boardUserPosition.x == .75f && userPresence.boardUserPosition.y == 1)
                {
                    if (!playersInScene.ContainsKey(userID))
                    {
                        playersInScene.Add(userID, playerSpawnLocations[0]);
                        Debug.LogError(userPresence.boardUserPosition + "bottom right");
                        //bottom right
                    }
                }
                if (userPresence.boardUserPosition.x == .25 && userPresence.boardUserPosition.y == 1)
                {
                    if (!playersInScene.ContainsKey(userID))
                        playersInScene.Add(userID, playerSpawnLocations[1]);

                    Debug.LogError(userPresence.boardUserPosition + " bottom left ");
                    //bottom left
                }
                if (userPresence.boardUserPosition.x == 0 && userPresence.boardUserPosition.y == .75f)
                {
                    if (!playersInScene.ContainsKey(userID))
                        playersInScene.Add(userID, playerSpawnLocations[2]);

                    //Left bottom
                }
                if (userPresence.boardUserPosition.x == 0 && userPresence.boardUserPosition.y == .25)
                {
                    if (!playersInScene.ContainsKey(userID))
                        playersInScene.Add(userID, playerSpawnLocations[3]);

                    Debug.LogError(userPresence.boardUserPosition + " Left top ");
                    //Left top
                }
                if (userPresence.boardUserPosition.x == .25 && userPresence.boardUserPosition.y == 0)
                {
                    if (!playersInScene.ContainsKey(userID))
                        playersInScene.Add(userID, playerSpawnLocations[4]);

                    Debug.LogError(userPresence.boardUserPosition + " p left");
                    //top left
                }
                if (userPresence.boardUserPosition.x == .75f && userPresence.boardUserPosition.y == 0)
                {
                    if (!playersInScene.ContainsKey(userID))
                        playersInScene.Add(userID, playerSpawnLocations[5]);

                    Debug.LogError(userPresence.boardUserPosition + "op right ");
                    //top right
                }
                if (userPresence.boardUserPosition.x == 1 && userPresence.boardUserPosition.y == .25)
                {
                    if (!playersInScene.ContainsKey(userID))
                        playersInScene.Add(userID, playerSpawnLocations[6]);

                    Debug.LogError(userPresence.boardUserPosition + " right top ");
                    //right top
                }
                if (userPresence.boardUserPosition.x == 1 && userPresence.boardUserPosition.y == .75f)
                {
                    if (!playersInScene.ContainsKey(userID))
                        playersInScene.Add(userID, playerSpawnLocations[7]);

                    Debug.LogError(userPresence.boardUserPosition + "/right bottom");
                    //right bottom
                }

            }

            foreach (KeyValuePair<string, Transform> kvp in playersInScene)
            {
                GameObject instobj = Instantiate(kvp.Value.gameObject, FindObjectOfType<Canvas>().transform);
                instobj.transform.position = kvp.Value.position;
                instobj.transform.rotation = kvp.Value.rotation;
                instobj.SetActive(true);
                instObjs.Add(instobj);
            }

        }



    }
}


