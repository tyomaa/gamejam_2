using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

namespace game_jam.UI
{
    public class CityScreen : BaseScreen
    {
        [SerializeField] private PvePoint[] _pvePoints;
        [SerializeField] private InputField _ipAdressInputField;
        public void OnHomeClick()
        {
           GUIManager.Instance.ShowScreen<HomeScreen>(ScreenType.HOME);
        }

        public void OnShopClick()
        {
            Debug.Log("Shop Click!");
        }


        public void OnDanceArenaClick()
        {
            GUIManager.Instance.ShowScreen<BattleSearchScreen>(ScreenType.BATTLE_SEARCH);
        }

        public void OnNextCityClick()
        {
            Debug.Log("Next City Click!");
        }
        
        public void Init(int cityId)
        {
            /*foreach (var point in _pvePoints)
            {
                point.Init(OnPveClick);
            }*/
        }

        public void OnPveClick(int pveId)
        {
            Debug.Log("On Pve Point " + pveId + " was clicked!");
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