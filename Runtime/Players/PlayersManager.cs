using System;
using System.Collections.Generic;

namespace IronMountain.Multiplayer.Players
{
        public static class PlayersManager
        {
                public static event Action OnPlayersChanged;
                
                public static readonly List<Player> Players = new ();
                public static Player LocalPlayer { get; private set; }
                
                public static void Register(Player player)
                {
                        if (!player || Players.Contains(player)) return;
                        Players.Add(player);
                        if (player.IsOwner) LocalPlayer = player;
                        OnPlayersChanged?.Invoke();
                }
        
                public static void Unregister(Player player)
                {
                        if (!player || !Players.Contains(player)) return;
                        Players.Remove(player);
                        if (LocalPlayer == player) LocalPlayer = null;
                        OnPlayersChanged?.Invoke();
                }

                public static Player GetPlayerByClientID(ulong ownerClientId)
                {
                        return Players.Find(test => test.OwnerClientId == ownerClientId);
                }
        
                public static Player GetNextPlayerByClientID(ulong ownerClientId)
                {
                        return GetNextPlayer(GetPlayerByClientID(ownerClientId));
                }

                public static Player GetNextPlayer(Player player)
                {
                        if (Players.Count == 0) return null;
                        if (!player || !Players.Contains(player)) return Players[0];
                        int index = Players.IndexOf(player);
                        index++;
                        if (index >= Players.Count) index = 0;
                        return Players[index];
                }
        }
}