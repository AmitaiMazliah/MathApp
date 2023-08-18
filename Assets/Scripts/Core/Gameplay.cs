using System.Collections;
using System.Collections.Generic;
using Tempname.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityScene = UnityEngine.SceneManagement.Scene;

namespace MathApp
{
    public class Gameplay : Scene
    {
        // [SerializeField] GameplayUI gameplayUIPrefab;

        // Scene INTERFACE

        protected override void OnInitialize()
        {
            base.OnInitialize();

            // List<IContextBehaviour> contextBehaviours = new();

            var countLoaded = SceneManager.sceneCount;
            for (var i = 0; i < countLoaded; i++)
            {
                var scene = SceneManager.GetSceneAt(i);
                // contextBehaviours.AddRange(scene.GetComponents<IContextBehaviour>(true));
            }

            // var contextBehaviours = Context.runner.SimulationUnityScene.GetComponents<IContextBehaviour>(true);

            // foreach (var behaviour in contextBehaviours)
            // {
            //     behaviour.Context = Context;
            // }
        }

        protected override void OnActivate()
        {
            base.OnActivate();

            // var uiService = Instantiate(gameplayUIPrefab);
            //
            // Context.UI = uiService;
            //
            // AddService(uiService);
            //
            // uiService.Activate();
        }

        protected override void OnTick()
        {
            // if (Context.runner != null)
            // {
            //     Context.runner.IsVisible = Context.isVisible;
            // }

            base.OnTick();
        }
    }
}
