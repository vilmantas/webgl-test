namespace Modules
{
    public class Fighter
    {
        private readonly Health _health;
        private readonly Strength _strength;

        private readonly float _attackSpeed;

        public float AttackSpeed => _attackSpeed;
        
        public float Health => _health.Value;

        public float MaxHealth => _health.Max;

        public float Power => _strength.Value;

        public bool IsDead => _health.IsDead;

        public Fighter(float health, float strength, float attackSpeed)
        {
            _health = new Health(health);
            _strength = new Strength(strength);
            _attackSpeed = attackSpeed;
        }
        
        public Fighter() : this(Modules.Health.DEFAULT, Modules.Strength.DEFAULT, 1f) { }

        public void Attack(Fighter victim)
        {
            victim.Defend(this);
        }

        public void Defend(Fighter attacker)
        {
            _health.Lose(attacker.Power);
        }
    }
}

