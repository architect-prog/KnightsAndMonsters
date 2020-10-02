using Game.Projectiles;
using Game.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Environment.Traps
{
    public class ShootingTrap : MonoBehaviour
    {
        [SerializeField] private Projectile _projectile;
        [SerializeField] private Transform _firepoint;
        [SerializeField] private float _shootRate;
        private Timer _timer;
        void Start()
        {
            _timer = new Timer(_shootRate, SpawnProjectile);
        }

        public void StartShooting()
        {
            _timer.StartTimer(this);
        }

        public void StopShooting()
        {
            _timer.StopTimer(this);
        }
     
        private void SpawnProjectile()
        {            
            Projectile projectile = Instantiate(_projectile, _firepoint.position, _firepoint.rotation);
        }

        private void OnDisable()
        {
            _timer.Clear();
        }
    }
}

