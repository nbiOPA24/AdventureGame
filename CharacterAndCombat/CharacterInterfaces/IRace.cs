public interface IRace
{
    string RaceName{get;}
    List<Ability> Abilities{get;}
    int AdjustHealth(int baseHealth);
    int AdjustDamage(int baseDamage);
    List<Ability> GetAbilities();
}
