using Platformer.Utils;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Platformer.Components;

namespace Platformer.Creatures.Mobs
{
    public class TotemTower : MonoBehaviour
    {
        [SerializeField] private List<ShootingTrapAi> _traps;
        [SerializeField] private Cooldown _cooldown;

        private int _currentTrap;

        private void Start()
        {
            foreach (var shootingTrapAI in _traps)
            {
                shootingTrapAI.enabled = false;
                var hp = shootingTrapAI.GetComponent<HealthComponent>();
                hp._onDie.AddListener(() => OnTrapDead(shootingTrapAI));
            }
        }

        private void OnTrapDead(ShootingTrapAi shootingTrapAi)
        {
            var index = _traps.IndexOf(shootingTrapAi);
            _traps.Remove(shootingTrapAi);
            if (index < _currentTrap)
            {
                _currentTrap--;
            }
        }


        private void Update()
        {
            if(_traps.Count == 0)
            {
                enabled = false;
                Destroy(gameObject, 1f);
            }

            var hasAnyTarget = _traps.Any(x => x._vision.IsTouchingLayer);
            if (hasAnyTarget)
            {
                if (_cooldown.IsReady)
                {
                    _traps[_currentTrap].Shoot();
                    _cooldown.Reset();
                    _currentTrap = (int) Mathf.Repeat(_currentTrap + 1, _traps.Count);
                }
            }
        }
    }
}