using System;
using UnityEditor;
using UnityEditor.AddressableAssets.Settings;

public static class BuildScript
{
    const string initScene = "Assets/Scenes/Initialization.unity";
    
    [MenuItem("Build/Windows")]
    public static void BuildWindows()
    {
        PlayerSettings.SetScriptingBackend(BuildTargetGroup.Standalone, ScriptingImplementation.IL2CPP);
        var buildPlayerOptions = new BuildPlayerOptions
        {
            scenes = new[] { initScene },
            locationPathName = "Builds/Windows/MathApp.exe",
            target = BuildTarget.StandaloneWindows64,
            options = BuildOptions.CompressWithLz4HC,
            subtarget = (int)StandaloneBuildSubtarget.Player
        };

        AddressableAssetSettings.CleanPlayerContent();
        AddressableAssetSettings.BuildPlayerContent();

        Console.WriteLine("Building...");
        BuildPipeline.BuildPlayer(buildPlayerOptions);
        Console.WriteLine("Built.");
    }

    [MenuItem("Build/Web")]
    public static void BuildWeb()
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