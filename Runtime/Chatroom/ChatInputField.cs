using Players;
using UnityEngine;
using UnityEngine.UI;

namespace Multiplayer.Chatroom
{
    public class ChatInputField : MonoBehaviour
    {
        [SerializeField] private ChatManager manager;
        [SerializeField] private InputField inputField;

        private void OnValidate()
        {
            if (!manager) manager = GetComponentInParent<ChatManager>();
            if (!inputField) inputField = GetComponent<InputField>();
        }
    
        private void Awake() => OnValidate();

        private void Update()
        {
            if (!inputField || !inputField.IsActive()) return;
            if (Input.GetKeyDown(KeyCode.Return)) Submit();
        }

        public void Submit()
        {
            if (!manager || string.IsNullOrWhiteSpace(inputField.text)) return;
            manager.AddMessageServerRpc(PlayersManager.LocalPlayer.DisplayName, inputField.text);
            inputField.text = string.Empty;
        }
    }
}