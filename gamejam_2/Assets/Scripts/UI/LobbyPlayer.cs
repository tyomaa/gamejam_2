using DefaultNamespace;
using UnityEngine;
using UnityEngine.Networking;

namespace game_jam.UI
{
    public class LobbyPlayer : NetworkLobbyPlayer
    {
        [SyncVar(hook = "OnMyName")] public string playerName = "";

        [SyncVar(hook = "OnMyLevel")] public string playerLevel = "";

        public override void OnStartLocalPlayer()
        {
            base.OnStartLocalPlayer();

            Debug.LogWarning("OnStartLocalPlayer");
            if (isLocalPlayer)
            {
                Debug.LogWarning("CLIENT READY TO BEGIN MESSAGE");
                SendReadyToBeginMessage();
            }
        }

        public override void OnClientEnterLobby()
        {
            base.OnClientEnterLobby();

            if (LobbyManager.Instance != null)
                LobbyManager.Instance.OnPlayersNumberModified(1);

            if (GUIManager.Instance.GetSearchScreen() != null)
            {
                GUIManager.Instance.GetSearchScreen().AddPlayer(this);

                if (isLocalPlayer)
                {
                    SetupLocalPlayer();
                }
                else
                {
                    SetupOtherPlayer();
                }

                //setup the player data on UI. The value are SyncVar so the player
                //will be created with the right value currently on server
                OnMyName(playerName);
                OnMyLevel(playerLevel);
            }
        }

        public override void OnClientReady(bool readyState)
        {
            Debug.LogError("CLIENT READY!!!" + readyState.ToString());
        }

        private void OnMyLevel(string level)
        {
            playerLevel = level;
        }

        public void OnMyName(string newName)
        {
            playerName = newName;
        }

        void SetupOtherPlayer()
        {
            //OnClientReady(false);
        }

        void SetupLocalPlayer()
        {
            //have to use child count of player prefab already setup as "this.slot" is not set yet
            if (playerName == "")
                CmdNameChanged("Player" + (GUIManager.Instance.GetSearchScreen().GetPlayersCount()));
            //when OnClientEnterLobby is called, the loval PlayerController is not yet created, so we need to redo that here to disable
            //the add button if we reach maxLocalPlayer. We pass 0, as it was already counted on OnClientEnterLobby
            if (LobbyManager.Instance != null)
                LobbyManager.Instance.OnPlayersNumberModified(0);
        }

        [Command]
        public void CmdNameChanged(string name)
        {
            playerName = name;
        }
    }
}