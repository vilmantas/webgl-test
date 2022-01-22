using DefaultNamespace;

namespace Modules
{
    public class Fighter : IAliveObject
    {
        public string Name;
        
        public Health Health { get; }
        
        private readonly Strength _strength;

        private readonly float _attackSpeed;

        public float Power => _strength.Value;
        
        public float AttackSpeed => _attackSpeed;
        
        public float HealthValue => Health.Value;

        public float MaxHealth => Health.Max;

        public bool IsDead => Health.IsDead;

        public Fighter(float health, float strength, float attackSpeed)
        {
            Health = new Health(health);
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
            Health.Lose(attacker.Power);
        }
    }
}

