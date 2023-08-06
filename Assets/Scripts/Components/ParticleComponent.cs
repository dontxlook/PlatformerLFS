using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Components
{
    public class ParticleComponent : MonoBehaviour
    {
        [SerializeField] private SpawnComponent _footstepsParticles;
        [SerializeField] private SpawnComponent _jumpParticles;
        [SerializeField] private SpawnComponent _fallParticles;
        [SerializeField] private SpawnComponent _attackParticles;

        public void SpawnFootDust()
        {
            _footstepsParticles.Spawn();
        }

        public void SpawnJumpDust()
        {
            _jumpParticles.Spawn();
        }

        public void SpawnFallDust()
        {
            _fallParticles.Spawn();
        }        
        
        public void SpawnAttackParticles()
        {
            _attackParticles.Spawn();
        }
    }
}

