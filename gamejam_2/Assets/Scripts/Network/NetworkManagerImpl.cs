using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

namespace Network
{
    public class NetworkManagerImpl : NetworkManager
    {
        public override NetworkClient StartHost(ConnectionConfig config, int maxConnections)
        {
            Debug.Log(networkAddress);
            Debug.Log(networkPort);
            return base.StartHost(config, maxConnections);
        }

        public override NetworkClient StartHost(MatchInfo info)
        {
            Debug.Log(networkAddress);
            Debug.Log(networkPort);
            return base.StartHost(info);
        }

        public override NetworkClient StartHost()
        {
            Debug.Log(networkAddress);
            Debug.Log(networkPort);
            return base.StartHost();
        }

        public override void OnStartServer()
        {
            Debug.Log(networkAddress);
            Debug.Log(networkPort);
            base.OnStartServer();
        }
    }
}