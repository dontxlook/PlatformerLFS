using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Creatures;

namespace Platformer.Components
{
    public class BounceComponent : MonoBehaviour
    {
        [Range(5f, 100f)] public float _bounceSpeed;
        [SerializeField] private Hero _hero;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                _hero.SetBounce(true);
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * _bounceSpeed, ForceMode2D.Impulse);
            }
        }
    }
}