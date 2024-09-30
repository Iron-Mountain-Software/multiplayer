using UnityEngine;
using UnityEngine.UI;

namespace IronMountain.Multiplayer.Clients
{
    public class LocalPlayerPrefsNameInputField : MonoBehaviour
    {
        [SerializeField] private InputField inputField;

        private void OnValidate()
        {
            if (!inputField) inputField = GetComponent<InputField>();
        }

        private void Awake() => OnValidate();

        private void OnEnable()
        {
            if (!inputField) return;
            inputField.text = LocalClientPrefs.DisplayNameSet 
                ? LocalClientPrefs.DisplayName
                : string.Empty;
            inputField.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnDisable()
        {
            if (!inputField) return;
            inputField.onValueChanged.RemoveListener(OnValueChanged);
        }

        private void OnValueChanged(string value)
        {
            LocalClientPrefs.DisplayName = value;
        }
    }
}