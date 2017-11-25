using game_jam.UI;
using Network;
using UnityEngine;
using UnityEngine.Networking;

namespace game_jam
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private Canvas _mainCanvas;

        [SerializeField] private NetworkHelper _networkHelper;

        [SerializeField] private GUIManager _guiManager;

        [SerializeField] private bool _isServer;

        private static Main _instance;

        void Awake()
        {
            _instance = this;
            DontDestroyOnLoad(_mainCanvas);
            DontDestroyOnLoad(_guiManager);
        }

        private void Start()
        {
            if (_isServer)
            {
                _networkHelper.StartServer();
            }
            else
            {
                var screen = _guiManager.ShowScreen<CityScreen>(ScreenType.CITY);
                screen.Init(1);
            }
        }

        public static Main Instance
        {
            get { return _instance; }
        }

        public GUIManager GetGuiManager()
        {
            return _guiManager;
        }
    }
}