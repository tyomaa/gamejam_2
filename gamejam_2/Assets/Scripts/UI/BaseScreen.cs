using UnityEngine;

namespace game_jam.UI
{
    public class BaseScreen : MonoBehaviour
    {

        public void Hide()
        {
            GameObject.Destroy(this);
        }


        
    }
}