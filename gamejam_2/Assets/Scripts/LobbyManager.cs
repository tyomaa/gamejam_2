using UnityEngine;
using UnityEngine.Networking;

namespace DefaultNamespace
{
    public class LobbyManager : NetworkLobbyManager
    {
        private static LobbyManager _instance;
        private int _playerNumber = 0;
        
        public static LobbyManager Instance
        {
            get { return _instance; }
        }

        private void Awake()
        {
            _instance = this;

            var ipString = PlayerPrefs.GetString("ip", string.Empty);
            if (!string.IsNullOrEmpty(ipString))
            {
                this.networkAddress = ipString;
            }
            else
            {
                this.networkAddress = "localhost";
            }
        }

        //allow to handle the (+) button to add/remove player
        public void OnPlayersNumberModified(int count)
        {
            _playerNumber += count;

            int localPlayerCount = 0;
            foreach (PlayerController p in ClientScene.localPlayers)
                localPlayerCount += (p == null || p.playerControllerId == -1) ? 0 : 1;
        }

        public override void OnLobbyServerPlayersReady()
        {
            bool allready = true;
            for(int i = 0; i < lobbySlots.Length; ++i)
            {
                if(lobbySlots[i] != null)
                    allready &= lobbySlots[i].readyToBegin;
            }

            if (allready)
            {
                ServerChangeScene(playScene);
            }
        }

        public void SetIpAdress(string value)
        {
            networkAddress = value;
            PlayerPrefs.SetString("ip", value);
            PlayerPrefs.Save();
        }
    }
}