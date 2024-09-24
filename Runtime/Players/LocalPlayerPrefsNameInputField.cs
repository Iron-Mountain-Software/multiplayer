using UnityEngine;
using UnityEngine.UI;

namespace IronMountain.Multiplayer.Players
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
            inputField.text = LocalPlayerPrefs.LocalPlayerNameSet 
                ? LocalPlayerPrefs.LocalPlayerName
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
            LocalPlayerPrefs.LocalPlayerName = value;
        }
    }
}