using MathApp.Events;
using MathApp.SceneManagement;
using UnityEngine;

public class Startup : MonoBehaviour
{
    [SerializeField] private GameSceneSO lobbyScene;
    [SerializeField] private RuntimeSettings runtimeSettings;
    [SerializeField] private LoadEventChannelSO loadSceneEvent;
    
    private void Start()
    {
        runtimeSettings.Initialize();
        loadSceneEvent.RaiseEvent(lobbyScene, true);
    }
}
