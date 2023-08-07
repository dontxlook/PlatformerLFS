using System;
using UnityEngine;

namespace Platformer.Model
{
    [Serializable]
    public class PlayerData
    {
        public int Coin;
        public int Hp;
        public bool IsArmed;
        public int Swords;

        public PlayerData Clone()
        {
            var json = JsonUtility.ToJson(this);
            return JsonUtility.FromJson<PlayerData>(json);
        }
    }
}

