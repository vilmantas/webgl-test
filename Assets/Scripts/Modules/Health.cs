
namespace Modules
{
    public class Health
    {
        public const int DEFAULT = 5;
        public float Value = DEFAULT;
        public readonly float Max = DEFAULT;

        public Health(float health)
        {
            Value = health;
            Max = health;
        }

        public Health() : this(DEFAULT) { }
    
        public void Lose(float amount)
        {
            Value -= amount;
        }

        public void Gain(float amount)
        {
            Value += amount;
        }

        public bool IsDead => Value <= 0;
    }
}

