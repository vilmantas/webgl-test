namespace Modules
{
    public class Strength
    {
        public const int DEFAULT = 5;
        public float Value = DEFAULT;

        public Strength(float power)
        {
            Value = power;
        }

        public Strength() : this(DEFAULT) { }
        
        public void Lose(float amount)
        {
            Value -= amount;
        }

        public void Gain(float amount)
        {
            Value += amount;
        }
    }
}