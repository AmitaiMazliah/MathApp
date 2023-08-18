using System.Collections.Generic;
using Sirenix.OdinInspector;
using MathApp.Audio;
using UnityEngine;

namespace MathApp.UI
{
    [CreateAssetMenu(fileName = "NewUIActionSoundMapping", menuName = "Audio/UI Action Sound Mapping")]
    public class UIActionSoundMapping : SerializedScriptableObject
    {
        public Dictionary<UIAction, AudioCueSO> mapping = new();
    }
}
