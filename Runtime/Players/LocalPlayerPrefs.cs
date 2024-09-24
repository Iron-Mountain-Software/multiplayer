using System;
using UnityEngine;

namespace IronMountain.Multiplayer.Players
{
    public static class LocalPlayerPrefs
    {
        public static event Action OnLocalPlayerNameChanged;
                
        private const string PlayerPrefsLocalPlayerNameKey = "Local Player Name";
        public const string PlayerPrefsLocalPlayerNameDefaultValue = "Player";
                
        public static Player LocalPlayer { get; private set; }

        public static string LocalPlayerName
        {
            get => PlayerPrefs.GetString(PlayerPrefsLocalPlayerNameKey, PlayerPrefsLocalPlayerNameDefaultValue);
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    PlayerPrefs.DeleteKey(PlayerPrefsLocalPlayerNameKey);
                }
                else PlayerPrefs.SetString(PlayerPrefsLocalPlayerNameKey, value);
                OnLocalPlayerNameChanged?.Invoke();
            }
        }

        public static bool LocalPlayerNameSet => PlayerPrefs.HasKey(PlayerPrefsLocalPlayerNameKey);
    }
}