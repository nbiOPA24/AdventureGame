public class Cleanse : CombatEffect
{
    public List<eCombatEffect> TypesToCleanse {get;set;}
    public Cleanse(List<eCombatEffect> typesToCleanse) : base(1,1,eCombatEffect.Cleanse)
    {
        TypesToCleanse = typesToCleanse;
    }

    public override void ApplyEffect(Character self,Character target)
    {   
        bool foundEffect = false;
        for(int i = 0 ; i< target.CurrentStatusEffects.Count;i++)
        {
            
            foreach(eCombatEffect effect in TypesToCleanse)
            {
                if(target.CurrentStatusEffects[i].Type == effect)
                {
                    target.ClearEffect(target.CurrentStatusEffects[i]);
                    foundEffect = true;
                }
            }
            
        }
        if(!foundEffect) Console.WriteLine("But had no effect");
        
    }
    public override CombatEffect CloneEffect()
    {
        return new Cleanse(TypesToCleanse);
    }
}