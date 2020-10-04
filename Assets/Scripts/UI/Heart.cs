using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class Heart : MonoBehaviour
    {
        [SerializeField] private Image background;
        [SerializeField] private Image _heart;

        public bool IsActive 
        { 
            set
            {
                _heart.gameObject.SetActive(value);
            } 
        }   

        public void SetColor(Color color)
        {
            _heart.color = color;
        }
    }
}
