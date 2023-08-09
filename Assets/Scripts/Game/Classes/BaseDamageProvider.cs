using Game.Interfaces;

namespace Game.Classes
{
    public class BaseDamageProvider : IDamageProvider
    {
        public void CalculateDamage(DamageInfo damageInfo)
        {
            damageInfo.calculatedDamage = damageInfo.damage;
        }
    }
}
