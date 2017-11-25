using UnityEngine;
using UnityEngine.UI;

namespace game_jam.UI
{
    public class PlayerInfo : MonoBehaviour
    {
        [SerializeField] public Text _playerName;
        [SerializeField] public Text _level;


        private LobbyPlayer _lobbyPlayer;

        public void OnAvavatarClick()
        {
            _lobbyPlayer.SendReadyToBeginMessage();
        }
        
        public void Init(string playerName, string level, LobbyPlayer lobbyPlayer)
        {
            _playerName.text = playerName;
            _level.text = level;
            _lobbyPlayer = lobbyPlayer;
        }

    }
}