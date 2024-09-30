using System.Collections.Generic;
using UnityEngine;

namespace IronMountain.Multiplayer.Clients
{
    public class ClientDisplaysManager : MonoBehaviour 
    {
        public readonly Dictionary<Client, ClientDisplay> Displays = new ();
    
        [SerializeField] protected ClientDisplay localClientPrefab;
        [SerializeField] protected ClientDisplay remoteClientPrefab;
        [SerializeField] protected Transform localClientParent;
        [SerializeField] protected Transform remoteClientParent;
        
        private void OnValidate()
        {
            if (!localClientParent) localClientParent = transform;
            if (!remoteClientParent) remoteClientParent = transform;
        }

        protected virtual void Awake()
        {
            OnValidate();
        }
        
        protected virtual void Start()
        {
            ClientsManager.OnClientsChanged += RefreshDisplays;
            RefreshDisplays();
        }

        protected virtual void OnDestroy()
        {
            ClientsManager.OnClientsChanged -= RefreshDisplays;
        }
        
        public ClientDisplay GetDisplay(Client client)
        {
            return Displays.ContainsKey(client)
                ? Displays[client]
                : null;
        }

        protected virtual void RefreshDisplays()
        {
            foreach (Client client in ClientsManager.Clients)
            {
                if (client) AddDisplay(client);
            }
            List<Client> playersToRemove = new List<Client>();
            foreach (KeyValuePair<Client, ClientDisplay> entry in Displays)
            {
                if (ClientsManager.Clients.Contains(entry.Key)) return;
                playersToRemove.Add(entry.Key);
            }
            foreach (Client client in playersToRemove)
            {
                if (client) RemoveDisplay(client);
            }
        }
        
        protected virtual ClientDisplay GetPrefab(Client client)
        {
            return client.IsLocalPlayer ? localClientPrefab : remoteClientPrefab;
        }
        
        protected virtual Transform GetParent(Client client)
        {
            return client.IsLocalPlayer ? localClientParent : remoteClientParent;
        }

        protected virtual ClientDisplay AddDisplay(Client client)
        {
            if (!client) return null;
            ClientDisplay display = GetDisplay(client);
            if (display) return display;
            ClientDisplay prefab = GetPrefab(client);
            Transform parent = GetParent(client);
            if (!prefab || !parent) return null;
            display = Instantiate(prefab, parent);
            display.Client = client;
            if (Displays.ContainsKey(client))
            {
                Displays[client] = display;
            }
            else Displays.Add(client, display);
            return display;
        }

        protected virtual void RemoveDisplay(Client client)
        {
            if (!client || !Displays.ContainsKey(client)) return;
            ClientDisplay display = GetDisplay(client);
            if (!display) return;
            Destroy(display.gameObject);
            Displays.Remove(client);
        }
    }
}