using System;
using UnityEngine;
using UnityEditor.Animations;
using Platformer.Components;
using Platformer.Model;
using Platformer.Utils;

namespace Platformer.Creatures
{
    public class Hero : Creature
    {
        [SerializeField] private CheckCircleOverlap _interactionCheck;
        [SerializeField] private LayerCheck _wallcheck;

        [SerializeField] private float _interactionRadius;

        [SerializeField] private Cooldown _throwCooldown;
        [SerializeField] private AnimatorController _armed; 
        [SerializeField] private AnimatorController _disarmed;

        [Space] [Header("Particles")] [SerializeField] 
        private ParticleSystem _hitParticles;

        private static readonly int ThrowKey = Animator.StringToHash("throw");

        private bool _allowDoubleJump;
        private bool _isOnWall;

        private GameSession _session;
        private float _defaultGravityScale;

        protected override void Awake()
        {
            base.Awake();
            _defaultGravityScale = Rigidbody.gravityScale;
        }

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            var health = GetComponent<HealthComponent>();

            health.SetHealth(_session.Data.Hp);
            UpdateHeroWeapon();
        }


        public void OnHealthChanged(int currentHealth)
        {
            _session.Data.Hp = currentHealth;
        }

        protected override void Update()
        {
            base.Update();
            if (_wallcheck.IsTouchingLayer && Direction.x == transform.localScale.x)
            {
                _isOnWall = true;
                Rigidbody.gravityScale = 0;
            }
            else
            {
                _isOnWall = false;
                Rigidbody.gravityScale = _defaultGravityScale;
            }
        }

        protected override float CalculateYVelocity()
        {
            var isJumpPressing = Direction.y > 0;

            if (IsGrounded || _isOnWall)
            {
                _allowDoubleJump = true;
            }

            if (!isJumpPressing && _isOnWall)
            {
                return 0f;
            }
            return base.CalculateYVelocity();
        }

        protected override float CalculateJumpVelocity(float yVelocity)
        {
            if (!IsGrounded && _allowDoubleJump)
            {
                _particles.Spawn("Jump");
                _allowDoubleJump = false;
                return _jumpSpeed;
            }
            return base.CalculateJumpVelocity(yVelocity); ;
        }

        public void AddCoins(int coin)
        {
            _session.Data.Coin += coin;
            Debug.Log($"Вы нашли {coin} монет! Всего денег: {_session.Data.Coin}");
        }        
        
        public void AddSword()
        {
            _session.Data.Swords += 1;
        }

        public override void TakeDamage()
        {
            base.TakeDamage();
/*            SetBounce(true);*/

            if (_session.Data.Coin > 0)
            {
                SpawnCoins();
            }
        }

        private void SpawnCoins()
        {
            var numCoinsToDispose = Mathf.Min(_session.Data.Coin, 5);
            _session.Data.Coin -= numCoinsToDispose;

            var burst = _hitParticles.emission.GetBurst(0);
            burst.count = numCoinsToDispose;
            _hitParticles.emission.SetBurst(0, burst);

            _hitParticles.gameObject.SetActive(true);
            _hitParticles.Play();
        }

        public void Interact()
        {
            _interactionCheck.Check();
        }

        public void SetBounce(bool isBounce)
        {
            _isBounced = isBounce;
        }

        public override void Attack()
        {
            if (!_session.Data.IsArmed) return;
            base.Attack();
        }

        public void ArmHero()
        {
            _session.Data.IsArmed = true;
            _session.Data.Swords += 1;
            UpdateHeroWeapon();
        }

        private void UpdateHeroWeapon()
        {
            Animator.runtimeAnimatorController = _session.Data.IsArmed ? _armed : _disarmed;
        }

        public void OnDoThrow()
        {
            _particles.Spawn("Throw");
        }

        public void Throw()
        {
            if (!_session.Data.IsArmed) return;

            if (_throwCooldown.IsReady && _session.Data.Swords > 1)
            {
                _session.Data.Swords -= 1;
                Animator.SetTrigger(ThrowKey);
                _throwCooldown.Reset();
            }
        }
    }
}