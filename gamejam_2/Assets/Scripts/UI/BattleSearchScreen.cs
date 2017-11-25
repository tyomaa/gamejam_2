﻿using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

namespace game_jam.UI
{
    public class BattleSearchScreen : BaseScreen
    {
        [SerializeField] private PlayerInfo _playerInfo;
        [SerializeField] private PlayerInfo _enemyInfo;
        [SerializeField] private RectTransform _searchInProgress;
        [SerializeField] private RectTransform _cancelButton;

        private List<PlayerInfo> _players = new List<PlayerInfo>{};

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


        public void AddPlayer(PlayerInfo info)
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
                        LobbyManager.Instance.ServerChangeScene(LobbyManager.Instance.playScene);
                    }
                }
            }
        }

        private void InitPlayer(PlayerInfo playerInfo, PlayerInfo info)
        {
            playerInfo.gameObject.SetActive(true);
            playerInfo.Init(info.playerName, info.playerLevel);
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
            _started = false;
            LobbyManager.Instance.StartClient();
        }
    }
}