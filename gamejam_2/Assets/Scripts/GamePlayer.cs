using UnityEngine;
using UnityEngine.Networking;

namespace DefaultNamespace
{
    public class GamePlayer : NetworkBehaviour
    {
        public int score = 0;

        public override void OnStartClient()
        {
            Debug.Log("OnStartClient");
            base.OnStartClient();
            score = 0;
        }

        public void OnScoreChanged(int value)
        {
            Debug.Log("OnScoreChanged " + value);
//            DanceManager.Instance.Update();
        }

        public void ChangeScore(int resultPoints)
        {
            if (!isLocalPlayer)
                return;

            score += resultPoints;
            CmdChangePoints(score);
            RpcChangePoints(resultPoints);
        }

        [Command]
        private void CmdChangePoints(int resultPoints)
        {
            score = resultPoints;
        }

        [ClientRpc]
        private void RpcChangePoints(int resultPoints)
        {
            score += resultPoints;
        }
    }
}