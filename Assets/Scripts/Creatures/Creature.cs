using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Components;

namespace Platformer.Creatures
{
    public class Creature : MonoBehaviour
    {

        [Header("Params")] 
        [SerializeField] private float _speed;
        [SerializeField] protected float _jumpSpeed;
        [SerializeField] private float _damageVelocity;
        [SerializeField] protected bool _isBounced;
        [SerializeField] private bool _invertScale; 

        [Header("Checkers")]
        [SerializeField] private LayerCheck _groundCheck;
        [SerializeField] private CheckCircleOverlap _attackRange;
        [SerializeField] protected SpawnListComponent _particles;

        protected Rigidbody2D Rigidbody;
        protected Vector2 Direction;
        protected Animator Animator;
        protected bool IsGrounded;

        private static readonly int IsGroundKey = Animator.StringToHash("is-grounded");
        private static readonly int IsRunning = Animator.StringToHash("is-running");
        private static readonly int VerticalVelocity = Animator.StringToHash("vertical-velocity");
        private static readonly int Hit = Animator.StringToHash("hit");
        private static readonly int AttackKey = Animator.StringToHash("attack");

        protected virtual void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();
        }

        public void SetDirection(Vector2 direction)
        {
            Direction = direction;
        }

        protected virtual void Update()
        {
            IsGrounded = _groundCheck.IsTouchingLayer;
        }

        private void FixedUpdate()
        {
            var xVelocity = Direction.x * _speed;
            var yVelocity = CalculateYVelocity();
            Rigidbody.velocity = new Vector2(xVelocity, yVelocity);

            Animator.SetBool(IsGroundKey, IsGrounded);
            Animator.SetBool(IsRunning, Direction.x != 0);
            Animator.SetFloat(VerticalVelocity, Rigidbody.velocity.y);

            UpdateSpriteDirection();
        }

        protected virtual float CalculateYVelocity()
        {
            var yVelocity = Rigidbody.velocity.y;
            var isJumpPressing = Direction.y > 0;

            if (isJumpPressing)
            {

                var isFalling = Rigidbody.velocity.y <= 0.001f;
                yVelocity = isFalling ? CalculateJumpVelocity(yVelocity) : yVelocity;
            }
            else if (!_isBounced && Rigidbody.velocity.y > 0)
            {
                yVelocity *= 0.5f;
            }
            else if (_isBounced && Rigidbody.velocity.y < 0)
            {
                _isBounced = false;
            }
            return yVelocity;
        }

        protected virtual float CalculateJumpVelocity(float yVelocity)
        {
            if (IsGrounded)
            {
                yVelocity = _jumpSpeed;
                _particles.Spawn("Jump");
            }
            return yVelocity;
        }

        private void UpdateSpriteDirection()
        {
            var multiplier = _invertScale ? -1 : 1;
            if (Direction.x > 0)
            {
                transform.localScale = new Vector3(multiplier, 1, 1);
            }
            else if (Direction.x < 0)
            {
                transform.localScale = new Vector3(-1 * multiplier, 1, 1);
            }
        }

        public virtual void TakeDamage()
        {
            Animator.SetTrigger(Hit);
            Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, _damageVelocity);
        }

        public virtual void Attack()
        {
            Animator.SetTrigger(AttackKey);
        }

        public void OnDoAttack()
        {
            _attackRange.Check();
            _particles.Spawn("Slash");
        }
    }
}
