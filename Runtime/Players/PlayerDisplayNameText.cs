using UnityEngine;
using UnityEngine.UI;

namespace IronMountain.Multiplayer.Players
{
    public class PlayerDisplayNameText : MonoBehaviour
    {
        [SerializeField] private Text text;
        [SerializeField] private PlayerDisplay playerDisplay;

        private Player _player;
        
        private Player Player
        {
            get => _player;
            set
            {
                if (_player == value) return;
                if (_player) _player.OnDisplayNameChanged -= RefreshText;
                _player = value;
                if (_player) _player.OnDisplayNameChanged += RefreshText;
                RefreshText();
            }
        }

        private void OnValidate()
        {
            if (!text) text = GetComponent<Text>();
            if (!playerDisplay) playerDisplay = GetComponentInParent<PlayerDisplay>();
        }

        private void Awake() => OnValidate();

        private void OnEnable()
        {
            if (playerDisplay) playerDisplay.OnPlayerChanged += RefreshPlayer;
            RefreshPlayer();
        }

        private void OnDisable()
        {
            if (playerDisplay) playerDisplay.OnPlayerChanged -= RefreshPlayer;
        }

        private void RefreshPlayer()
        {
            Player = playerDisplay ? playerDisplay.Player : null;
            RefreshText();
        }

        private void RefreshText()
        {
            if (!text) return;
            text.text = Player ? Player.DisplayName : string.Empty;
        }
    }
}