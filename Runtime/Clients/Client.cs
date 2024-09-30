using System;
using Unity.Collections;
using Unity.Netcode;

namespace IronMountain.Multiplayer.Clients
{
    public class Client : NetworkBehaviour
    {
        public event Action OnDisplayNameChanged;
        private void InvokeOnDisplayNameChanged(FixedString64Bytes oldName, FixedString64Bytes nemName) =>
            OnDisplayNameChanged?.Invoke();
        
        private readonly NetworkVariable<FixedString64Bytes> _displayName = new(
            new FixedString64Bytes(), 
            NetworkVariableReadPermission.Everyone,
            NetworkVariableWritePermission.Owner);
        
        public string DisplayName => _displayName.Value.ToString();

        private void RefreshName()
        {
            if (!IsLocalPlayer) return;
            _displayName.Value = LocalClientPrefs.DisplayNameSet 
                ? LocalClientPrefs.DisplayName 
                : name;
        }
        
        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            name = "Client " + OwnerClientId;
            _displayName.OnValueChanged += InvokeOnDisplayNameChanged;
            if (IsLocalPlayer) LocalClientPrefs.OnLocalClientNameChanged += RefreshName;
            RefreshName();
            ClientsManager.Register(this);
        }
        
        public override void OnNetworkDespawn()
        {
            base.OnNetworkDespawn();
            ClientsManager.Unregister(this);
            _displayName.OnValueChanged -= InvokeOnDisplayNameChanged;
            if (IsLocalPlayer) LocalClientPrefs.OnLocalClientNameChanged -= RefreshName;
        }
    }
}