using UnityEngine;

namespace game_jam.UI
{
    public class HomeScreen : BaseScreen
    {
        
        public void OnExitHomeClick()
        {
            Main.Instance.GetGuiManager().ShowScreen<CityScreen>(ScreenType.CITY);
        }
        
    }
}