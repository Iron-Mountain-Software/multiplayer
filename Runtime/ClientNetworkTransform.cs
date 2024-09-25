using Unity.Netcode.Components;

namespace IronMountain.Multiplayer
{
    public class ClientNetworkTransform : NetworkTransform
    {
        protected override bool OnIsServerAuthoritative()
        {
            return false;
        }
    }
}