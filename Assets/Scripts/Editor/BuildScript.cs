using System;
using UnityEditor;
using UnityEditor.AddressableAssets.Settings;

public static class BuildScript
{
    const string initScene = "Assets/Scenes/Initialization.unity";

    [MenuItem("Build/Web")]
    public static void BuildWindowsServer()
    {
        PlayerSettings.SetScriptingBackend(BuildTargetGroup.Standalone, ScriptingImplementation.IL2CPP);
        var buildPlayerOptions = new BuildPlayerOptions
        {
            scenes = new[] { initScene },
            locationPathName = "Builds/Web/MathApp.exe",
            target = BuildTarget.WebGL,
            options = BuildOptions.CompressWithLz4HC,
            subtarget = (int)StandaloneBuildSubtarget.Server
        };

        AddressableAssetSettings.CleanPlayerContent();
        AddressableAssetSettings.BuildPlayerContent();

        Console.WriteLine("Building...");
        BuildPipeline.BuildPlayer(buildPlayerOptions);
        Console.WriteLine("Built.");
    }
}