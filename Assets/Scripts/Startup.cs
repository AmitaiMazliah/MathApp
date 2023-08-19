using MathApp.Events;
using MathApp.SceneManagement;
using UnityEngine;

public class Startup : MonoBehaviour
{
    [SerializeField] GameSceneSO lobbyScene;

    [SerializeField] LoadEventChannelSO loadSceneEvent;
    
    private void Start()
    {
        loadSceneEvent.RaiseEvent(lobbyScene, true);
    }
}
