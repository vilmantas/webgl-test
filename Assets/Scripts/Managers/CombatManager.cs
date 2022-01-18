using System;
using UnityEngine;

namespace Managers
{
    // Have this class manage combat related interactions Attacking for starters. 
    public class CombatManager : MonoBehaviour
    {
        [NonSerialized] 
        public static CombatManager Instance;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public void HandleAttack(FighterScript attacker)
        {
            var defender = GameManagerScript.Instance.GetAdversaryFor(attacker);
            defender.DefendFrom(attacker);
        }
    }
}