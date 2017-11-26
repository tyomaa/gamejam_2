using UnityEngine;

namespace DefaultNamespace.Dance.Animations
{
    public class AnimationContainer : MonoBehaviour
    {
        [SerializeField] private DancerAnimation _chikenAnimation;

        [SerializeField] private DancerAnimation _fishermaAnimation;


        private float _lastRandomTime = 0;
        private readonly float _changeAnimationPeriod = 1.5f;
        private bool needAnimate;

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


        public void UpdateRandom()
        {
            if (_lastRandomTime < Time.time - _changeAnimationPeriod)
            {
                _lastRandomTime = Time.time;

                if (needAnimate)
                {
                    EnemySuccesAnimation();
                    needAnimate = false;
                }
                else
                {
                    needAnimate = true;
                }
            }
        }
    }
}