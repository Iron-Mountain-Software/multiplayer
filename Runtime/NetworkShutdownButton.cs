using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

namespace IronMountain.Multiplayer
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Button))]
    public class NetworkShutdownButton : MonoBehaviour
    {
        [SerializeField] private Button button;

        private void OnValidate()
        {
            if (!button) button = GetComponent<Button>();
        }

        private void Awake() => OnValidate();

        private void OnEnable()
        {
            if (button) button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            if (button) button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            if (!NetworkManager.Singleton || NetworkManager.Singleton.ShutdownInProgress) return;
            NetworkManager.Singleton.Shutdown();
        }
    }
}
