using System;
using System.Collections.Generic;

namespace IronMountain.Multiplayer.Clients
{
        public static class ClientsManager
        {
                public static event Action OnClientsChanged;
                
                public static readonly List<Client> Clients = new ();
                public static Client LocalClient { get; private set; }
                
                public static void Register(Client client)
                {
                        if (!client || Clients.Contains(client)) return;
                        Clients.Add(client);
                        if (client.IsLocalPlayer) LocalClient = client;
                        OnClientsChanged?.Invoke();
                }
        
                public static void Unregister(Client client)
                {
                        if (!client || !Clients.Contains(client)) return;
                        Clients.Remove(client);
                        if (LocalClient == client) LocalClient = null;
                        OnClientsChanged?.Invoke();
                }

                public static Client GetClientByID(ulong ownerClientId)
                {
                        return Clients.Find(test => test.OwnerClientId == ownerClientId);
                }
        }
}