using UnityEngine;
using UnityEngine.Networking;

namespace DefaultNamespace
{
    public class GamePlayer : NetworkBehaviour
    {
        private static GamePlayer _instance;

        [SerializeField] private DancerScore _dancerScore;
        
        
        public static GamePlayer Instance
        {
            get { return _instance; }
        }
        private void Awake()
        {
            _instance = this;
        }
        

        public void DeltaPoints(int delta)
        {
            _dancerScore.DeltaPoints(delta);
            
        }
    }
    
    
}