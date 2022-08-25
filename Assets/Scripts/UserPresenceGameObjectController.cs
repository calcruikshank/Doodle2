using Gameboard;
using Gameboard.EventArgs;
using Gameboard.Examples;
using Gameboard.Objects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Gameboard
{

    public class UserPresenceGameObjectController : MonoBehaviour
    {
        public List<PlayerPresenceDrawer> playerList = new List<PlayerPresenceDrawer>();
        private List<string> onScreenLog = new List<string>();
        [SerializeField] GameObject playerPresenceSceneObject;

        public static UserPresenceGameObjectController singleton;

        public List<CompanionTextureAsset> cardIdList;
        public List<string> cardImageList = new List<string>();
        private float cachedTime; private string resolveOnUpdate;

        UserPresenceController userPresenceController;
        CardController cardController;
        AssetController assetController;
        Canvas mainCanvas;

        bool setupComplete = false;
        private void Awake()
        {
            if (singleton != null)
            {
                Destroy(this);
            }
            singleton = this;
            GameObject gameboardObject = GameObject.FindWithTag("Gameboard");
            userPresenceController = gameboardObject.GetComponent<UserPresenceController>();
            cardController = gameboardObject.GetComponent<CardController>();
            assetController = gameboardObject.GetComponent<AssetController>();
            mainCanvas = FindObjectOfType<Canvas>();
        }
        void Start()
        {
            userPresenceController.OnUserPresence += OnUserPresence;
            GetUP();
        }


        async void GetUP()
        {
            if (userPresenceController != null)
            {
                CompanionUserPresenceEventArgs compasa = await userPresenceController.GetCompanionUserPresence();
                foreach (GameboardUserPresenceEventArgs user in compasa.playerPresenceList)
                {
                    OnUserPresence(user);
                }
            }
            
        }



        void OnUserPresence(GameboardUserPresenceEventArgs userPresence)
        {
            Debug.LogError("On  user presence " + userPresence.userId);
            PlayerPresenceDrawer myObject = playerList.Find(s => s.userId == userPresence.userId);
            lock (playerList)
            {
                if (myObject == null)
                {
                    // Add it here, and when adding also populate myObject
                    // If the user doesn't exist in our player list, add them now.
                    if (playerList.Find(s => s.userId == userPresence.userId) == null)
                    {
                        /*UserPresencePlayer testPlayer = new UserPresencePlayer()
                        {
                            gameboardId = userPresence.userId
                        };*/

                        GameObject scenePrefab = Instantiate(playerPresenceSceneObject, mainCanvas.transform);

                        myObject = scenePrefab.GetComponent<PlayerPresenceDrawer>();
                        myObject.InjectDependencies(userPresence);



                        if (!string.IsNullOrEmpty(userPresence.userId))
                        {
                            myObject.InjectUserId(userPresence.userId);
                        }


                        playerList.Add(myObject);
                        Debug.Log("--- === New player added: " + userPresence.userId);
                    }

                }
                if (myObject != null)
                {
                    myObject.UpdateUserPresence(userPresence);
                }

            }
            

        }
       

        void AddToLog(string logMessage)
        {
           // onScreenLog.Add(logMessage);
            Debug.Log(logMessage);
        }

        void OnGUI()
        {
            foreach (string thisString in onScreenLog)
            {
                GUILayout.Label(thisString);
            }
        }
        public Texture2D DeCompress(Texture2D source)
        {
            RenderTexture renderTex = RenderTexture.GetTemporary(
                        source.width,
                        source.height,
                        0,
                        RenderTextureFormat.Default,
                        RenderTextureReadWrite.Linear);

            Graphics.Blit(source, renderTex);
            RenderTexture previous = RenderTexture.active;
            RenderTexture.active = renderTex;
            Texture2D readableText = new Texture2D(source.width, source.height);
            readableText.ReadPixels(new Rect(0, 0, renderTex.width, renderTex.height), 0, 0);
            readableText.Apply();
            RenderTexture.active = previous;
            RenderTexture.ReleaseTemporary(renderTex);
            return readableText;
        }


    }

}
