using System;
using UnityEngine;

namespace IronMountain.Multiplayer.Clients
{
    public class ClientDisplay : MonoBehaviour
    {
        public event Action OnClientChanged;

        private Client _client;

        public Client Client
        {
            get => _client;
            set
            {
                if (_client == value) return;
                _client = value;
                OnClientChanged?.Invoke();
            }
        }
    }
}