using System;
using UnityEngine;

namespace game_jam.UI
{
    public class PvePoint : MonoBehaviour
    {
        [SerializeField] private int _id;


        public Action<int> _clickAction;
        
        public void Init(Action<int> clickAction)
        {
            _clickAction = clickAction;
        }


        public void OnClick()
        {
            if (_clickAction != null)
            {
                _clickAction.Invoke(_id);
            }
        }
    }
}