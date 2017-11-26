using UnityEngine;

namespace DefaultNamespace.Dance.Animations
{
    public class DancerAnimation : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        
        
        public void MissClick()
        {
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("idle"))
            {
                return;
            }
            else
            {
                _animator.Play("idle");
            }
        }

        public void SuccessMove()
        {
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("idle"))
            {
                _animator.Play(GetRandomDanceAnimation());
            }
        }

        private string GetRandomDanceAnimation()
        {
            string animationName = "move" + Random.Range(1, 3);
            //Debug.Log("ANIMATION NAME IS " + animationName);
            return animationName;
        }
    }
}