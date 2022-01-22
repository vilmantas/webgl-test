
using UnityEngine;

namespace Modules
{
    public class Health
    {
        public const int DEFAULT = 5;
        public readonly float Max = DEFAULT;
        private float _realValue = DEFAULT;

        public float Value => Mathf.Max(_realValue, 0);
        
        public Health(float health)
        {
            _realValue = health;
            Max = health;
        }

        public Health() : this(DEFAULT) { }
    
        public void Lose(float amount)
        {
            _realValue -= amount;
        }

        public void Gain(float amount)
        {
            var newAmount = Value + amount;
            _realValue = Mathf.Min(newAmount, Max);
        }

        public bool IsDead => Value <= 0;
    }
}

