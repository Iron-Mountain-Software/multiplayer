using System;
using Unity.Collections;
using Unity.Netcode;

namespace IronMountain.Multiplayer.Players
{
    public class Player : NetworkBehaviour
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
            _displayName.Value = LocalPlayerPrefs.LocalPlayerNameSet 
                ? LocalPlayerPrefs.LocalPlayerName 
                : name;
        }
        
        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            name = "Player " + OwnerClientId;
            _displayName.OnValueChanged += InvokeOnDisplayNameChanged;
            if (IsLocalPlayer) LocalPlayerPrefs.OnLocalPlayerNameChanged += RefreshName;
            RefreshName();
            PlayersManager.Register(this);
        }
        public override void OnNetworkDespawn()
        {
            base.OnNetworkDespawn();
            PlayersManager.Unregister(this);
            _displayName.OnValueChanged -= InvokeOnDisplayNameChanged;
            if (IsLocalPlayer) LocalPlayerPrefs.OnLocalPlayerNameChanged -= RefreshName;
        }
    }
}