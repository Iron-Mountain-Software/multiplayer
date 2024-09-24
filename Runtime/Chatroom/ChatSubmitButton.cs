using UnityEngine;
using UnityEngine.UI;

namespace Multiplayer.Chatroom
{
    public class ChatSubmitButton : MonoBehaviour
    {
        [SerializeField] private ChatInput inputField;
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
            if (inputField) inputField.Submit();
        }
    }
}