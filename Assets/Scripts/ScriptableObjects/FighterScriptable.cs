using System.Collections;
using System.Collections.Generic;
using Modules;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Fighters", fileName = "Simple Fighter")]
    public class FighterScriptable : ScriptableObject
    {
        [Range(1, 15)]
        public int Health = 1;
        
        [Range(1, 5)]
        public int Strength = 1;

        [Range(0.1f, 5)]
        public float DelayBetweenAttacks = 1;

        
        private Fighter _fighter;

        public Fighter Build()
        {
            var instance = new Fighter(Health, Strength, DelayBetweenAttacks);
            return instance;
        }
    }    
}

