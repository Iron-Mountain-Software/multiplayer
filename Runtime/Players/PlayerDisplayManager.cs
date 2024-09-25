using System.Collections.Generic;
using UnityEngine;

namespace IronMountain.Multiplayer.Players
{
    public class PlayerDisplayManager : MonoBehaviour 
    {
        public readonly Dictionary<Player, PlayerDisplay> Displays = new ();
    
        [SerializeField] protected PlayerDisplay playerDisplayPrefab;
        [SerializeField] protected PlayerDisplay opponentDisplayPrefab;
        [SerializeField] protected Transform localPlayerParent;
        [SerializeField] protected Transform opponentParent;
        
        private void OnValidate()
        {
            if (!localPlayerParent) localPlayerParent = transform;
            if (!opponentParent) opponentParent = transform;
        }

        protected virtual void Awake()
        {
            OnValidate();
        }
        
        protected virtual void Start()
        {
            PlayersManager.OnPlayersChanged += RefreshDisplays;
            RefreshDisplays();
        }

        protected virtual void OnDestroy()
        {
            PlayersManager.OnPlayersChanged -= RefreshDisplays;
        }
        
        public PlayerDisplay GetDisplay(Player player)
        {
            return Displays.ContainsKey(player)
                ? Displays[player]
                : null;
        }

        protected virtual void RefreshDisplays()
        {
            foreach (Player player in PlayersManager.Players)
            {
                if (player) AddDisplay(player);
            }
            List<Player> playersToRemove = new List<Player>();
            foreach (KeyValuePair<Player, PlayerDisplay> entry in Displays)
            {
                if (PlayersManager.Players.Contains(entry.Key)) return;
                playersToRemove.Add(entry.Key);
            }
            foreach (Player player in playersToRemove)
            {
                if (player) RemoveDisplay(player);
            }
        }
        
        protected virtual PlayerDisplay GetPrefab(Player player)
        {
            return player.IsLocalPlayer ? playerDisplayPrefab : opponentDisplayPrefab;
        }
        
        protected virtual Transform GetParent(Player player)
        {
            return player.IsLocalPlayer ? localPlayerParent : opponentParent;
        }

        protected virtual PlayerDisplay AddDisplay(Player player)
        {
            if (!player) return null;
            PlayerDisplay display = GetDisplay(player);
            if (display) return display;
            PlayerDisplay prefab = GetPrefab(player);
            Transform parent = GetParent(player);
            if (!prefab || !parent) return null;
            display = Instantiate(prefab, parent);
            display.Player = player;
            if (Displays.ContainsKey(player))
            {
                Displays[player] = display;
            }
            else Displays.Add(player, display);
            return display;
        }

        protected virtual void RemoveDisplay(Player player)
        {
            if (!player || !Displays.ContainsKey(player)) return;
            PlayerDisplay display = GetDisplay(player);
            if (!display) return;
            Destroy(display.gameObject);
            Displays.Remove(player);
        }
    }
}