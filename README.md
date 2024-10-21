# Multiplayer
*Version: 1.1.5*
## Description: 
Systems and tools for Unity NGO multiplayer.
## Dependencies: 
* com.unity.multiplayer.tools (1.1.1)
* com.unity.netcode.gameobjects (1.9.1)
* com.unity.services.relay (1.1.1)
* com.unity.transport (2.3.0)
---
## Key Scripts & Components: 
### __ G E N
### Iron Mountain. Multiplayer
1. public class **ClientNetworkTransform** : NetworkTransform
1. public class **CurrentRoomCode** : MonoBehaviour
1. public class **HostGameObject** : NetworkBehaviour
   * Methods: 
      * public override void ***OnNetworkSpawn***()
      * public override void ***OnNetworkDespawn***()
1. public class **NetworkShutdownButton** : MonoBehaviour
1. public class **RelayManager** : MonoBehaviour
   * Methods: 
      * public void ***Start***()
      * public void ***CreateRelay***()
      * public void ***JoinRelay***(String joinCode)
### Iron Mountain. Multiplayer. Chatroom
1. public class **ChatDisplay** : MonoBehaviour
1. public class **ChatInput** : MonoBehaviour
   * Methods: 
      * public void ***Submit***()
1. public class **ChatManager** : NetworkBehaviour
   * Actions: 
      * public event Action ***OnAlertAdded*** 
      * public event Action ***OnMessageAdded*** 
   * Methods: 
      * public void ***AddAlertServerRpc***(String alert)
      * public void ***AddMessageServerRpc***(String author, String message)
### Iron Mountain. Multiplayer. Clients
1. public class **Client** : NetworkBehaviour
   * Actions: 
      * public event Action ***OnDisplayNameChanged*** 
   * Properties: 
      * public String ***DisplayName***  { get; }
   * Methods: 
      * public override void ***OnNetworkSpawn***()
      * public override void ***OnNetworkDespawn***()
1. public class **ClientDisplay** : MonoBehaviour
   * Actions: 
      * public event Action ***OnClientChanged*** 
   * Properties: 
      * public Client ***Client***  { get; set; }
1. public class **ClientDisplayNameText** : MonoBehaviour
1. public class **ClientDisplaysManager** : MonoBehaviour
   * Methods: 
      * public ClientDisplay ***GetDisplay***(Client client)
1. public static class **ClientsManager**
1. public static class **LocalClientPrefs**
1. public class **LocalPlayerPrefsNameInputField** : MonoBehaviour
