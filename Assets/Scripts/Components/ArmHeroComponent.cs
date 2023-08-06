﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Creatures;

namespace Platformer
{
    public class ArmHeroComponent : MonoBehaviour
    { 
        public void ArmHero(GameObject go)
        {
            var hero = go.GetComponent<Hero>();
            if (hero != null)
            {
                hero.ArmHero(); 
            }
        }
    }
}

