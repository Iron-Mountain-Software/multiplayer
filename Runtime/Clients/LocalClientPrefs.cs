using System;
using UnityEngine;

namespace IronMountain.Multiplayer.Clients
{
    public static class LocalClientPrefs
    {
        public static event Action OnLocalClientNameChanged;
                
        private const string DisplayNameKey = "Local Client Name";
        private const string DisplayNameDefaultValue = "Client";
        
        public static string DisplayName
        {
            get => PlayerPrefs.GetString(DisplayNameKey, DisplayNameDefaultValue);
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    PlayerPrefs.DeleteKey(DisplayNameKey);
                }
                else PlayerPrefs.SetString(DisplayNameKey, value);
                OnLocalClientNameChanged?.Invoke();
            }
        }

        public static bool DisplayNameSet => PlayerPrefs.HasKey(DisplayNameKey);
    }
}