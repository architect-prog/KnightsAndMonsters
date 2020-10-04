using Game.Components;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Heart _heart;
        [SerializeField] private Color _heartColor;
        [SerializeField] private int _maxHeartsCount;
        [SerializeField] private float _distanceBtwHearts;
        [SerializeField] private List<Heart> _hearts;
        private int _currentHeartsCount;

        void Start()
        {
            _hearts = GetComponentsInChildren<Heart>().ToList();
        }
        
        /// <summary>
        /// Initialize max hearts count and refresh health bar
        /// </summary>
        /// <param name="health"></param>
        public void SetHealth(HealthComponent health)
        {
            _maxHeartsCount = Mathf.RoundToInt(health.MaxHealth);
            RenderHealthBarInGame();
            SetCurrentHealth(health);
        }
        
        /// <summary>
        /// Refresh health
        /// </summary>
        /// <param name="health"></param>
        public void SetCurrentHealth(HealthComponent health)
        {
            _currentHeartsCount = Mathf.RoundToInt(health.Health);           

            //Deactivate hearts from right part of health bar
            for (int i = _hearts.Count - 1; i >= _maxHeartsCount - _currentHeartsCount; i--)            
                _hearts[i].IsActive = false;           
        }

        /// <summary>
        /// Update healthbar in editor
        /// </summary>
        public void RenderHealthBarInEditor()
        {
            //Get all hearts if we have them
            _hearts = GetComponentsInChildren<Heart>().ToList();

            //Remove null values and destroy useless hearts 
            _hearts.RemoveAll(x => x == null);
            if (_hearts.Count > _maxHeartsCount)
            {
                for (int i = 0; _hearts.Count != _maxHeartsCount;)
                {
                    if (_hearts[i] != null)                    
                        DestroyImmediate(_hearts[i].gameObject);                                    
                    _hearts.RemoveAt(i);                                     
                }
            }
            //Add hearts if we need it
            CreateHealthBar();
        }

        /// <summary>
        /// Update healthbar in game
        /// </summary>
        public void RenderHealthBarInGame()
        {
            //Remove null values and destroy useless hearts 
            _hearts.RemoveAll(x => x == null);
            if (_hearts.Count > _maxHeartsCount)
            {
                for (int i = 0; _hearts.Count != _maxHeartsCount;)
                {
                    if (_hearts[i] != null)                    
                        Destroy(_hearts[i].gameObject);                    
                    _hearts.RemoveAt(i);
                }
            }
            //Add hearts if we need it
            CreateHealthBar();
        }

        private void CreateHealthBar()
        {
            //Create hearts
            for (int i = 0; _hearts.Count < _maxHeartsCount; i++)            
                _hearts.Add(Instantiate(_heart, transform));            
            //Set positions for all hearts
            for (int i = 0; i < _hearts.Count; i++)
            {
                _hearts[i].transform.position = transform.position + new Vector3(i * _distanceBtwHearts, 0);
                _hearts[i].SetColor(_heartColor);
                _hearts[i].IsActive = true;
            }
        }

    }

}
