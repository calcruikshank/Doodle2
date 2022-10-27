using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameboard;

namespace Gameboard.Examples
{
    public class HandBladeController : MonoBehaviour
    {
        bool setupFinished = false;
        private List<TrackedBoardObject> trackingBoardObjects = new List<TrackedBoardObject>();

        [SerializeField] Transform handBlade;

        void Update()
        {
            if (!setupFinished)
            {
                if (Gameboard.singleton != null)
                {
                    setupFinished = true;
                    Gameboard.singleton.boardTouchController.boardTouchHandler.BoardObjectsUpdated += BoardObjectsUpdated;
                    Gameboard.singleton.boardTouchController.boardTouchHandler.BoardObjectSessionsDeleted += BoardObjectsDeleted;
                }
            }
        }

        UserPresenceSceneObject lastUser;
        void BoardObjectsUpdated(object origin, List<TrackedBoardObject> newBoardObjectList)
        {
            lock (trackingBoardObjects)
            {
                foreach (TrackedBoardObject newBoardObject in newBoardObjectList)
                {
                    if (newBoardObject.TrackedObjectType == DataTypes.TrackedBoardObjectTypes.Pointer && newBoardObject.PointerUpdated && newBoardObject.PointerType == DataTypes.PointerTypes.Blade)
                    {
                        if (trackingBoardObjects.Find(s => s.sessionId == newBoardObject.sessionId) == null)
                        {
                            SetHandbladePosition(newBoardObject.sceneWorldPosition, newBoardObject.tuio.ptr.angle * Mathf.Rad2Deg, newBoardObject.sessionId);



                            Collider2D hit = Physics2D.OverlapPoint(handBlade.position);
                            if (hit != null)
                            {
                                if (hit.GetComponent<UserPresenceSceneObject>())
                                {
                                    lastUser = hit.GetComponent<UserPresenceSceneObject>();
                                    lastUser.ShowPrompt();
                                }
                            }


                        }
                    }
                }
            }
        }

        void BoardObjectsDeleted(object origin, List<uint> deletedSessionIdList)
        {
            lock (trackingBoardObjects)
            {
                foreach (uint thisInt in deletedSessionIdList)
                {
                    if (lastUser != null)
                    {
                        GameManager.singleton.HidePrompt();
                        handBlade.position = Vector3.zero;
                        lastUser = null;
                    }
                }
            }
        }

        void SetHandbladePosition(Vector3 positionSent, float yAngleSent, uint index)
        {
            //im so hungry
            handBlade.position = new Vector3(positionSent.x, positionSent.y, handBlade.position.z);
        }
    }
}
