using UnityEngine;
using UnityEngine.UI;

namespace Multiplayer
{
    public class CurrentRoomCode : MonoBehaviour
    {
        [SerializeField] private Text text;

        private void OnValidate()
        {
            if (!text) text = GetComponent<Text>();
        }

        private void OnEnable()
        {
            RelayManager.OnCurrentRoomChanged += Refresh;
            Refresh();
        }

        private void OnDisable()
        {
            RelayManager.OnCurrentRoomChanged -= Refresh;
        }

        private void Refresh()
        {
            if (!text) return;
            text.text = RelayManager.CurrentRoomCode;
        }
    }
}