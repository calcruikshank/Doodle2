using Gameboard.EventArgs;
using Gameboard.Helpers;
using Gameboard.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


namespace Gameboard.Examples
{

    public class PlayerPresenceDrawer : UserPresenceSceneObject
    {
        UserPresenceController userPresenceController;
        CardController cardController;
        AssetController assetController;
        private void Awake()
        {
            GameboardLogging.Verbose("UserPresenceExample Awake");
            GameObject gameboardObject = GameObject.FindWithTag("Gameboard");
            GameboardLogging.Verbose("UserPresenceExample Awake Success");
            userPresenceController = gameboardObject.GetComponent<UserPresenceController>();
            cardController = gameboardObject.GetComponent<CardController>();
            assetController = gameboardObject.GetComponent<AssetController>();
        }

        public void UpdatePlayerPositionOnStart(Vector2 vectorSent)
        {
            var  playerSceneDrawerPosition = GameboardHelperMethods.GameboardScreenPointToScenePoint(Camera.main, vectorSent);
            Debug.LogError(playerSceneDrawerPosition + " Player presence scene drawer update");
            ScenePositionUpdated(playerSceneDrawerPosition);
            ScenePositionUpdated(new Vector3(playerSceneDrawerPosition.x, 0.2f, playerSceneDrawerPosition.z));

        }
        protected override void ScenePositionUpdated(Vector3 inNewPosition)
        {
            this.transform.position = inNewPosition;
            Debug.Log("UpdatedPosition = " + inNewPosition);
        }
        protected override void LocalEulerAnglesUpdated(Vector3 inNewEulers)
        {
            this.transform.localEulerAngles = inNewEulers;
        }
        protected override void PlayerAdded()
        {
            Debug.Log("Adding Player");
        }
        protected override void PlayerRemoved()
        {
            Debug.Log("Player Removed");
            Destroy(this.gameObject);
        }
        protected override void PlayerNameChanged()
        {
            Debug.Log("Player name changed");
        }
        protected override void PlayerColorChanged()
        {
            Debug.Log("Player Color Changed ");
        }

        public Vector3 GetRotation()
        {
            return this.transform.eulerAngles;
        }

        

    }


}
