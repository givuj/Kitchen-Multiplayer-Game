using Unity.Netcode.Components;
using UnityEngine;

namespace Unity.Multiplayer.Samples.Utilities.ClientAuthority
{
    /// <summary>
    /// Used for syncing a transform with client side changes. This 
    /// for transforms that'll always be owned by the server.
    /// </summary>
    [DisallowMultipleComponent]
    public class ClientNetworkTransform : NetworkTransform
    {
        /// <summary>
        /// Used to determine who can write to this transform. Owner 
        /// This imposes state to the server. This is putting trust 
        /// </summary>
        protected override bool OnIsServerAuthoritative()
        {
            return false;
        }
    }
}