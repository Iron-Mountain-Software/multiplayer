using UnityEngine;
using UnityEngine.UI;

namespace IronMountain.Multiplayer.Clients
{
    public class ClientDisplayNameText : MonoBehaviour
    {
        [SerializeField] private Text text;
        [SerializeField] private ClientDisplay clientDisplay;

        private Client _client;
        
        private Client Client
        {
            get => _client;
            set
            {
                if (_client == value) return;
                if (_client) _client.OnDisplayNameChanged -= RefreshText;
                _client = value;
                if (_client) _client.OnDisplayNameChanged += RefreshText;
                RefreshText();
            }
        }

        private void OnValidate()
        {
            if (!text) text = GetComponent<Text>();
            if (!clientDisplay) clientDisplay = GetComponentInParent<ClientDisplay>();
        }

        private void Awake() => OnValidate();

        private void OnEnable()
        {
            if (clientDisplay) clientDisplay.OnClientChanged += RefreshClient;
            RefreshClient();
        }

        private void OnDisable()
        {
            if (clientDisplay) clientDisplay.OnClientChanged -= RefreshClient;
        }

        private void RefreshClient()
        {
            Client = clientDisplay ? clientDisplay.Client : null;
            RefreshText();
        }

        private void RefreshText()
        {
            if (!text) return;
            text.text = Client ? Client.DisplayName : string.Empty;
        }
    }
}