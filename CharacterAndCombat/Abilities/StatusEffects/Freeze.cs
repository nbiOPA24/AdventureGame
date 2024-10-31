public class Freeze : CombatEffect
{
    public Freeze(int duration) : base(duration,1,eCombatEffect.Freeze)
    {

    }

    public override void ApplyEffect(Character character)
    {
        base.ApplyEffect(character);
        character.AbleToAct = false;
    }
    public override void PrintApplication(Character character)
    {
            Console.Write($"{character.Name} has been ");
            Utilities.ConsoleWriteColor("Frozen",ConsoleColor.Blue);
            Console.WriteLine($" for {Duration} rounds");
    }
        public override void EndOfRound(Character character)
    {
        Console.Write($"{character.Name} is ");
        Utilities.ConsoleWriteColor("Frozen ",ConsoleColor.Blue);
        Console.WriteLine($" and thus unable to act, {Duration} rounds remaining");
        character.AbleToAct = false;
        //reduces duration by 1round if its not the first round
        if(!FirstRound)
        {
            base.EndOfRound(character);
        }
        else
        {
            FirstRound = false;
        }
    }
    public override CombatEffect CloneEffect()
    {
        return new Freeze(Duration);
    }
}