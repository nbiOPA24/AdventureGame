public class Cleanse : CombatEffect
{
    List<eCombatEffect> TypesToCleanse {get;set;}
    public Cleanse(List<eCombatEffect> typesToCleanse) : base(1,1,eCombatEffect.Cleanse)
    {
        TypesToCleanse = typesToCleanse;
    }

    public override void ApplyEffect(Character character)
    {   
        for(int i = 0 ; i< character.CurrentStatusEffects.Count;i++)
        {
            foreach(eCombatEffect effect in TypesToCleanse)
            {
                if(character.CurrentStatusEffects[i].Type == effect)
                {
                    character.ClearEffect(character.CurrentStatusEffects[i]);
                }
            }
        }
       
        
    }
    public override CombatEffect CloneEffect()
    {
        return new Cleanse(TypesToCleanse);
    }
}