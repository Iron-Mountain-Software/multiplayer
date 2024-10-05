using Unity.Netcode;
using UnityEngine;

namespace IronMountain.Multiplayer
{
    public class HostGameObject : NetworkBehaviour
    {
        [SerializeField] private GameObject controlledObject;
        [SerializeField] private bool stateWhenIsHost;
        [SerializeField] private bool stateWhenIsNotHost;

        private void OnValidate()
        {
            if (!controlledObject) controlledObject = gameObject;
        }

        public override void OnNetworkSpawn() => Refresh();

        public override void OnNetworkDespawn() => Refresh();

        private void Refresh()
        {
            if (!controlledObject) return;
            controlledObject.SetActive(IsHost ? stateWhenIsHost : stateWhenIsNotHost);
        }
    }
}
