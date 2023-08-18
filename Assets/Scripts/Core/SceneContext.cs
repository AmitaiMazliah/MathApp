using System;
using MathApp.UI;
using UnityEngine;

namespace MathApp
{
    [Serializable]
    public class SceneContext
    {
        // Player

        [HideInInspector] public string peerUserID;

        // General

        // public SceneAudio Audio;
        public SceneUI UI;
        public ObjectCache ObjectCache;
        public BoolEventChannelSO changeCursorStateEvent;
        // public SceneCamera Camera;
        // public NetworkGame NetworkGame;

        // [HideInInspector] public GlobalSettings Settings;
        // public RuntimeSettings RuntimeSettings;

        // Gameplay

        // public Announcer        Announcer;

        [HideInInspector] public bool isVisible;
        [HideInInspector] public bool hasInput;

        // [HideInInspector] public NetworkRunner runner;
        // [HideInInspector] public GameplayMode gameplayMode;
        // [HideInInspector] public PlayerRef localPlayerRef;
        // [HideInInspector] public PlayerRef observedPlayerRef;
        // [HideInInspector] public Agent observedAgent;
        // [HideInInspector] public MatchDetails matchDetails;
    }
}
