using UnityEngine;

namespace DefaultNamespace.Dance.Animations
{
    public class AnimationContainer : MonoBehaviour
    {
        [SerializeField] private DancerAnimation _chikenAnimation;

        [SerializeField] private DancerAnimation _fishermaAnimation;


        private float _lastRandomTime = 0;
        private float _changeAnimationPeriod = 1.5f;

        public void MySuccessAnimation()
        {
            _chikenAnimation.SuccessMove();
        }

        public void MyFailAnimation()
        {
            _chikenAnimation.MissClick();
        }

        public void EnemySuccesAnimation()
        {
            _fishermaAnimation.SuccessMove();
        }

        public void EnemyFailAnimation()
        {
            _fishermaAnimation.MissClick();
        }


        private void Update()
        {
           
        }
    }
}