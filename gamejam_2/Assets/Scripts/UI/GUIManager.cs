using System;
using UnityEngine;

namespace game_jam.UI
{
    public enum ScreenType
    {
        CITY,
        BATTLE_SEARCH,
        BATTLE,
        HOME
    }

    public class GUIManager : MonoBehaviour
    {
        [SerializeField] private Canvas _mainCanvas;
        [SerializeField] private CityScreen _cityScreen;
        [SerializeField] private BattleSearchScreen _battleSearchScreen;
        [SerializeField] private HomeScreen _homeScreen;


        private BaseScreen _currentScreen;

        public T ShowScreen<T>(ScreenType type) where T : BaseScreen
        {
            HidePreviousDialog();
            switch (type)
            {
                case ScreenType.CITY:
                    _currentScreen = Instantiate(_cityScreen);
                    break;
                case ScreenType.BATTLE_SEARCH:
                    _currentScreen = Instantiate(_battleSearchScreen);
                    break;
                case ScreenType.HOME:
                    _currentScreen = Instantiate(_homeScreen);
                    break;
                case ScreenType.BATTLE:
                    break;
                default:
                    throw new ArgumentOutOfRangeException("type", type, null);
            }
            _currentScreen.transform.SetParent(_mainCanvas.transform, false);
            return (T) _currentScreen;
        }

        private void HidePreviousDialog()
        {
            if (_currentScreen != null)
            {
                _currentScreen.Hide();
            }
        }
    }
}