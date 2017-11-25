using UnityEngine;
using UnityEngine.UI;

namespace game_jam.UI
{
    public class PlayerInfo : MonoBehaviour
    {
        [SerializeField] private Text _playerName;
        [SerializeField] private Text _level;
        [SerializeField] private Image _avatar;
    }
}