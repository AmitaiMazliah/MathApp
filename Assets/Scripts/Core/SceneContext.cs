using System;
using MathApp;
using UnityEngine;

[Serializable]
public class SceneContext
{
    // public SceneAudio Audio;
    public SceneUI UI;
    public ObjectCache ObjectCache;

    // [HideInInspector] public GlobalSettings Settings;
    public RuntimeSettings RuntimeSettings;

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