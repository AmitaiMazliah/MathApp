using UnityEngine;
using UnityEngine.Rendering;

namespace Tempname.Utils
{
    public class CursorController : MonoBehaviour
    {
        [Header("Events")]
        [SerializeField] private BoolEventChannelSO changeCursorStateEvent;

        private void OnEnable()
        {
            changeCursorStateEvent.OnEventRaised += ToggleCursorLock;
        }

        private void OnDisable()
        {
            changeCursorStateEvent.OnEventRaised -= ToggleCursorLock;
        }

        private void ToggleCursorLock(bool shouldLock)
        {
            if (shouldLock) LockCursor();
            else UnlockCursor();
        }

        void LockCursor()
        {
            Debug.Log("Cursor locked");
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        void UnlockCursor()
        {
            Debug.Log("Cursor unlocked");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
