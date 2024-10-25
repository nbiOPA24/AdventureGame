public class Ability
{
    public string Name{get;set;}
    public int BaseDamage{get;set;}
    
    public Ability(string name,int baseDamage)
    {
        Name = name;
        BaseDamage = baseDamage;
    }


    public static List<string> ToStringList(List<Ability> abilityList)
    {
        //puts all the names of the abilities into a string list
            List<string> stringReturnList = new();
            foreach(Ability a in abilityList)
            {
                stringReturnList.Add(a.Name);
                Console.WriteLine(a.Name);
            }
            return stringReturnList;
    }
    public static List<string> ToStringList(Ability[] abilityList)
    {
        //puts all the names of the abilities into a string list
            List<string> stringReturnList = new();
            foreach(Ability a in abilityList)
            {
                if(a != null)
                {
                stringReturnList.Add(a.Name);
                Console.WriteLine(a.Name);
                }
            }
            return stringReturnList;
    }
}

