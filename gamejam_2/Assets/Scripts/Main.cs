using game_jam.UI;
using UnityEngine;
using UnityEngine.Networking;

namespace game_jam
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private Canvas _mainCanvas;

        [SerializeField] private NetworkManager _manager;

        [SerializeField] private GUIManager _guiManager;

        private static Main _instance;
        void Awake()
        {
            _instance = this;
            DontDestroyOnLoad(_mainCanvas);
            DontDestroyOnLoad(_guiManager);
        }

        private void Start()
        {
            var screen = _guiManager.ShowScreen<CityScreen>(ScreenType.CITY);
            screen.Init(1);
        }

        public static Main Instance
        {
            get { return _instance; }
        }

        public GUIManager GetGuiManager ()
        {
            return _guiManager;
        }
    }
}