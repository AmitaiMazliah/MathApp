using MathApp.Events;
using MathApp.SceneManagement;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class Startup : MonoBehaviour
{
    [SerializeField] private GameSceneSO lobbyScene;
    [SerializeField] private RuntimeSettings runtimeSettings;
    [SerializeField] private LoadEventChannelSO loadSceneEvent;
    
    private void Start()
    {
        LocalizationSettings.InitializationOperation.WaitForCompletion();
        runtimeSettings.Initialize();
        loadSceneEvent.RaiseEvent(lobbyScene, true);
    }
}
