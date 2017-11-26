using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

namespace game_jam.UI
{
    public class BattleSearchScreen : BaseScreen
    {
        [SerializeField] private PlayerInfo _playerInfo;
        [SerializeField] private PlayerInfo _enemyInfo;
        [SerializeField] private RectTransform _searchInProgress;
        [SerializeField] private RectTransform _cancelButton;
        [SerializeField] private InputField _ipAdressInputField;
        private List<LobbyPlayer> _players = new List<LobbyPlayer>{};

        private bool _started;
        private bool _host;

        public void OnCancelClick()
        {

            if (_started)
            {
                if (_host)
                {
                    LobbyManager.Instance.StopHost();
                }
                else
                {
                    LobbyManager.Instance.StartClient();
                }
                _started = false;
            }
            foreach (var player in _players)
            {
                GameObject.Destroy(player);
            }
            _players.Clear();
            GUIManager.Instance.ShowScreen<CityScreen>(ScreenType.CITY).Init(1);
        }


        public void AddPlayer(LobbyPlayer info)
        {
            if (_players.Count == 0)
            {
                InitPlayer(_playerInfo, info);
            }
            else
            {
                InitPlayer(_enemyInfo, info);
            }
            _players.Add(info);
            
            if (_players.Count == 2)
            {
                _searchInProgress.gameObject.SetActive(false);
                foreach (var player in _players)
                {
                    if (player.isLocalPlayer)
                    {
//                        LobbyManager.Instance.ServerChangeScene(LobbyManager.Instance.playScene);
                    }
                }
            }
        }

        private void InitPlayer(PlayerInfo playerInfo, LobbyPlayer info)
        {
            playerInfo.gameObject.SetActive(true);
            playerInfo.Init(info.playerName, info.playerLevel, info);
        }

        public int GetPlayersCount()
        {
            return _players.Count;
        }

        public void OnStartAsHost()
        {
            _started = true;
            _host = true;
            LobbyManager.Instance.StartHost();
        }

        public void OnStartAsClient()
        {
            _started = true;
            _host = false;
            LobbyManager.Instance.StartClient();
        }
        
        public void OnInputEntered(string value)
        {
            if (string.IsNullOrEmpty(_ipAdressInputField.text))
            {
                return;
            }
            LobbyManager.Instance.SetIpAdress(_ipAdressInputField.text);
        }
    }
}