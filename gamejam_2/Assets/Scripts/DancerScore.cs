using UnityEngine.Networking;

namespace DefaultNamespace
{
    public class DancerScore : NetworkBehaviour
    {
        [SyncVar] private int _myPoints = 0;
        [SyncVar] private int _enemyPoints = 0;


        public void DeltaPoints(int delta)
        {
            if (isLocalPlayer)
            {
                CmdSendDeltaToServer(delta);
            }
        }


        [Command]
        private void CmdSendDeltaToServer(int value)
        {
            if (isServer)
            {
                _myPoints += value;
                RpcSendResult(_myPoints);
            }
        }

        [ClientRpc(channel = 0)]
        private void RpcSendResult(int value)
        {
            if (isLocalPlayer)
            {
                _myPoints = value;
            }
            else
            {
                if (isClient)
                {
                    _enemyPoints = value;
                }
            }
            ShowProgress();
        }

        private void ShowProgress()
        {
           DanceManager.Instance.SetProgress(_myPoints - _enemyPoints);
            
        }
    }
}