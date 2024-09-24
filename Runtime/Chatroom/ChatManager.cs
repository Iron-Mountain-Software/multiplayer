using System;
using Unity.Netcode;

namespace IronMountain.Multiplayer.Chatroom
{
    public class ChatManager : NetworkBehaviour
    {
        public event Action<string> OnAlertAdded;
        public event Action<string, string> OnMessageAdded;
        
        [ServerRpc(RequireOwnership = false)]
        public void AddAlertServerRpc(string alert)
        {
            AddAlertClientRpc(alert);
        }
        
        [ClientRpc]
        private void AddAlertClientRpc(string alert)
        {
            OnAlertAdded?.Invoke(alert);
        }
        
        [ServerRpc(RequireOwnership = false)]
        public void AddMessageServerRpc(string author, string message)
        {
            AddMessageClientRpc(author, message);
        }
        
        [ClientRpc]
        private void AddMessageClientRpc(string author, string message)
        {
            OnMessageAdded?.Invoke(author, message);
        }
    }
}