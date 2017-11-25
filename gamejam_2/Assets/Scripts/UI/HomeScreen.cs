using UnityEngine;

namespace game_jam.UI
{
    public class HomeScreen : BaseScreen
    {
        
        public void OnExitHomeClick()
        {
            GUIManager.Instance.ShowScreen<CityScreen>(ScreenType.CITY);
        }
        
    }
}