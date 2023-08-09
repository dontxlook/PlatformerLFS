using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Creatures;
using Platformer.Model;

namespace Platformer.Components.Collectables
{
    public class AddCoinsComponent : MonoBehaviour
    {
        [SerializeField] private int _cost;

        private Hero _hero;
        private GameSession _session;

        private void Start()
        {
            _hero = FindObjectOfType<Hero>();
            _session = FindObjectOfType<GameSession>();
        }

        public void AddCoins(int coin)
        {
            _session.Data.Coin += coin;
            Debug.Log($"Вы нашли {coin} монет! Всего денег: {_session.Data.Coin}");
        }
    }
}

