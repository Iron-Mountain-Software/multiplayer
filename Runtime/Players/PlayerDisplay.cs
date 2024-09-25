using System;
using UnityEngine;

namespace IronMountain.Multiplayer.Players
{
    public class PlayerDisplay : MonoBehaviour
    {
        public event Action OnPlayerChanged;

        private Player _player;

        public Player Player
        {
            get => _player;
            set
            {
                if (_player == value) return;
                _player = value;
                OnPlayerChanged?.Invoke();
            }
        }
    }
}