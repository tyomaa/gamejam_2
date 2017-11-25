using UnityEngine;

namespace game_jam.UI
{
    public class BattleSearchScreen : BaseScreen
    {
        [SerializeField] private PlayerInfo _playerInfo;
        [SerializeField] private PlayerInfo _enemyInfo;
        [SerializeField] private RectTransform _searchInProgress;
        [SerializeField] private RectTransform _cancelButton;



        public void OnCancelClick()
        {
            Main.Instance.GetGuiManager().ShowScreen<CityScreen>(ScreenType.CITY).Init(1);
        }
    }
}