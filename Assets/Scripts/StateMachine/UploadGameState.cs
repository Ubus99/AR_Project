using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StateMachine
{
    public class UploadGameState : AbstractMSM
    {
        Scene _scene;
        ImageSwitcher _ui;

        bool _initialized;

        public UploadGameState(GameManager manager) : base(manager)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _scene = SceneManager.LoadScene(
                Manager.game1Scene.name,
                new LoadSceneParameters(LoadSceneMode.Additive)
            );
        }

        public override void Execute()
        {
            base.Execute();
            if (_scene.isLoaded && !_initialized) //initializing here is not good form, but we need a one frame delay
            {
                _ui = GameObject.Find("ImageController").GetComponent<ImageSwitcher>();
                GameObject.Find("DebugEventSystem")?.SetActive(false);
                _initialized = true;
            }
        }

        public override void Exit()
        {
            base.Exit();
            SceneManager.UnloadSceneAsync(_scene);
        }

        public override Type GetNextState()
        {
            if (_ui && _ui.finished)
                return typeof(EasterEggState<ChargeGameState>);
            return typeof(UploadGameState);
        }
    }
}
