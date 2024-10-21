using IronMountain.Multiplayer.Clients;
using UnityEngine;
using UnityEngine.UI;

namespace IronMountain.Multiplayer.Chatroom
{
    public class ChatInput : MonoBehaviour
    {
        [SerializeField] private ChatManager manager;
        [SerializeField] private InputField inputField;
        [SerializeField] private Button submitButton;
        [SerializeField] private bool refocusAfterSubmit = true;
        
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
            if (!inputField) return;
            if (inputField.IsActive() && Input.GetKeyDown(KeyCode.Return)) Submit();
        }

        private void Activate()
        {
            if (!inputField) return;
            inputField.ActivateInputField();
            inputField.Select();
        }

        public void Submit()
        {
            if (!manager || !inputField || string.IsNullOrWhiteSpace(inputField.text)) return;
            string author = ClientsManager.LocalClient ? ClientsManager.LocalClient.DisplayName : string.Empty;
            manager.AddMessageServerRpc(author, inputField.text);
            inputField.text = string.Empty;
            if (refocusAfterSubmit) Activate();
        }
    }
}