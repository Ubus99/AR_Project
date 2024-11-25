using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace StateMachine
{
    public class EasterEggState : AbstractMSM
    {

        public EasterEggState(GameManager manager) : base(manager)
        {
        }

        public override void Enter()
        {
#if UNITY_EDITOR
            Debug.Log("Enter EasterEggState");
#endif
            Manager.spawnManager.SpawnEgg();
        }

        public override void Execute()
        {
#if UNITY_EDITOR
            Debug.Log("Execute EasterEggState");
#endif

            if (!Manager.tapStartThisFrame)
                return;

            var hits = new List<ARRaycastHit>();
            if (Manager.raycastManager.Raycast(
                    Manager.mTapStartPosition,
                    hits,
                    TrackableType.PlaneWithinPolygon
                ))
            {
                hits[0].trackable.gameObject.SetActive(false);
            }
        }

        public override void Exit()
        {
#if UNITY_EDITOR
            Debug.Log("Exit EasterEggState");
#endif

            Manager.spawnManager.DestroyEgg();
        }

        public override Type GetNextState()
        {
            if (Manager.spawnManager.eggInstance)
                return typeof(EasterEggState);
            return typeof(StartState);
        }
    }
}
