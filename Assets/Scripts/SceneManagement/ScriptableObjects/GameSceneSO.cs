using MathApp.Audio;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace MathApp.SceneManagement
{
    /// <summary>
    /// This class is a base class which contains what is common to all game scenes (Locations or Menus)
    /// </summary>
    public class GameSceneSO : DescriptionBaseSO
    {
        [Header("Information")]
        public AssetReference sceneReference;

        [Header("Sounds")]
        public AudioCueSO musicTrack;
    }
}
