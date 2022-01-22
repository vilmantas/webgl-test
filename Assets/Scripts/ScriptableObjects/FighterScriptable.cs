using System.Collections;
using System.Collections.Generic;
using Modules;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Fighter", fileName = "Simple Fighter")]
    public class FighterScriptable : ScriptableObject
    {
        public string Name;
        
        [Range(1, 100)]
        public int Health = 1;
        
        [Range(1, 5)]
        public int Strength = 1;

        [Range(0.1f, 5)]
        public float DelayBetweenAttacks = 1;

        
        private Fighter _fighter;

        public Fighter Build()
        {
            var instance = new Fighter(Health, Strength, DelayBetweenAttacks)
            {
                Name = Name,
            };
            return instance;
        }
    }    
}

