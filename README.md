# Multiplayer
*Version: 1.0.0*
## Description: 
Systems and tools for Unity NGO multiplayer.
## Dependencies: 
* com.unity.multiplayer.tools (1.1.1)
* com.unity.netcode.gameobjects (1.9.1)
* com.unity.services.relay (1.1.1)
---
## Key Scripts & Components: 
### __ G E N
### Multiplayer
1. public class **CurrentRoomCode** : MonoBehaviour
1. public class **RelayManager** : MonoBehaviour
   * Methods: 
      * public void ***Start***()
      * public void ***CreateRelay***()
      * public void ***JoinRelay***(String joinCode)
### Multiplayer. Chatroom
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
1. public class **ChatSubmitButton** : MonoBehaviour
