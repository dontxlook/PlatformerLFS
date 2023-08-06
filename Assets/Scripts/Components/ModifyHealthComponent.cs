using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Components
{
    public class ModifyHealthComponent : MonoBehaviour
    {
        [SerializeField] private int _hpDelta;

        public void ChangeHP(GameObject target)
        {
            var healthComponent = target.GetComponent<HealthComponent>();
            if (healthComponent != null)
            {
                healthComponent.ModifyHealth(_hpDelta);
            }
        }
    }
}