using System;
using UnityEngine;
using UnityEngine.UI;

namespace Multiplayer.Chatroom
{
    public class ChatDisplay : MonoBehaviour
    {
        [SerializeField] private Color alertColor = Color.white;
        [SerializeField] private Color authorColor = Color.white;
        [SerializeField] private Color messageColor = Color.white;
        [Space]
        [SerializeField] private ChatManager manager;
        [SerializeField] private Text prefab;
        [SerializeField] private RectTransform parent;
        [SerializeField] private ScrollRect scrollView;

        private bool _shouldRebuild = false;
        
        private void OnValidate()
        {
            if (!manager) manager = GetComponentInParent<ChatManager>();
            if (!parent) parent = GetComponent<RectTransform>();
        }

        private void Awake() => OnValidate();

        private void OnEnable()
        {
            if (!manager) return;
            manager.OnMessageAdded += AddMessage;
            manager.OnAlertAdded += AddAlert;
        }

        private void OnDisable()
        {
            if (!manager) return;
            manager.OnMessageAdded -= AddMessage;
            manager.OnAlertAdded -= AddAlert;
        }

        private void OnGUI()
        {
            if (!_shouldRebuild) return;
            _shouldRebuild = false;
            Rebuild();
        }

        private void AddAlert(string alert)
        {
            if (!prefab || !parent) return;
            string content = $"<color=#{ColorUtility.ToHtmlStringRGBA(alertColor)}>{alert}</color>";
            Instantiate(prefab, parent.transform).text = content;
            _shouldRebuild = true;
            Rebuild();
        }
        
        private void AddMessage(string author, string message)
        {
            if (!prefab || !parent) return;
            string content = $"<color=#{ColorUtility.ToHtmlStringRGBA(authorColor)}><b>{author}</b></color>: <color=#{ColorUtility.ToHtmlStringRGBA(messageColor)}>{message}</color>";
            Instantiate(prefab, parent.transform).text = content;
            _shouldRebuild = true;
            Rebuild();
        }

        private void Rebuild()
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(parent);
            if (scrollView) scrollView.verticalNormalizedPosition = 0;
        }
    }
}