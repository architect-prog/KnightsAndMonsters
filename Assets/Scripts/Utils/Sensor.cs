using UnityEngine;

namespace Game.Utils
{
    public class Sensor : MonoBehaviour
    {
        [SerializeField] private LayerMask _scanningMask;
        public bool Collide { get; private set; }
        public bool NotCollide { get => !Collide; }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (_scanningMask.Includes(collision.gameObject.layer))
            {
                Collide = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (_scanningMask.Includes(collision.gameObject.layer))
            {
                Collide = false;
            }
        }           
    }
}
