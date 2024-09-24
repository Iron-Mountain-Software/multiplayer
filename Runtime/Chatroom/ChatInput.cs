using IronMountain.Multiplayer.Players;
using UnityEngine;
using UnityEngine.UI;

namespace IronMountain.Multiplayer.Chatroom
{
    public class ChatInput : MonoBehaviour
    {
        [SerializeField] private ChatManager manager;
        [SerializeField] private InputField inputField;
        [SerializeField] private Button submitButton;

        private void OnValidate()
        {
            if (!manager) manager = GetComponentInParent<ChatManager>();
            if (!inputField) inputField = GetComponentInChildren<InputField>();
            if (!submitButton) submitButton = GetComponentInChildren<Button>();
        }
    
        private void Awake() => OnValidate();

        private void OnEnable()
        {
            if (submitButton) submitButton.onClick.AddListener(Submit);
        }

        private void OnDisable()
        {
            if (submitButton) submitButton.onClick.RemoveListener(Submit);
        }

        private void Update()
        {
            if (!inputField || !inputField.IsActive()) return;
            if (Input.GetKeyDown(KeyCode.Return)) Submit();
        }

        public void Submit()
        {
            if (!manager || !inputField || string.IsNullOrWhiteSpace(inputField.text)) return;
            string author = PlayersManager.LocalPlayer ? PlayersManager.LocalPlayer.DisplayName : string.Empty;
            manager.AddMessageServerRpc(author, inputField.text);
            inputField.text = string.Empty;
            inputField.Select();
        }
    }
}