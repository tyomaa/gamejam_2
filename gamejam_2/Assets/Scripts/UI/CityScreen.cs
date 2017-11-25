using UnityEngine;

namespace game_jam.UI
{
    public class CityScreen : BaseScreen
    {
        [SerializeField] private RectTransform _background;
        [SerializeField] private PvePoint[] _pvePoints;

        public void OnHomeClick()
        {
            Debug.Log("Click Home!");
        }

        public void OnShopClick()
        {
            Debug.Log("Shop Click!");
        }


        public void OnDanceArenaClick()
        {
            Main.Instance.GetGuiManager().ShowScreen<BattleSearchScreen>(ScreenType.BATTLE_SEARCH);
        }

        public void OnNextCityClick()
        {
            Debug.Log("Next City Click!");
        }
        
        public void Init(int cityId)
        {
            foreach (var point in _pvePoints)
            {
                point.Init(OnPveClick);
            }
        }

        public void OnPveClick(int pveId)
        {
            Debug.Log("On Pve Point " + pveId + " was clicked!");
        }
    }
}