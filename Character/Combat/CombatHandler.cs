using System.ComponentModel.DataAnnotations;

public static  class CombatHandler 
{
    
    /// <summary>
    /// Returns True if player beats the enemy list in combat
    /// </summary>
    /// <param name="enemyList"></param>
    /// <returns></returns>
    public static bool RunCombatScenario(List<Enemy> enemyList,Player player)
    {
        DisplayPlayerCombatOptions();
        

        //Checks if player is alive after the Combat has finnished
        if(player.CurrentHealth > 0 )
        {
            return true; //player killed all monsters
        }
        else
        {
            return false; // player died
        }   
    }
    public static void DisplayPlayerCombatOptions()
    {
        List<string> combatOptions = new()
        {
            "Attack",
            "Use item",
            "Flee"
        };

        switch(Utilities.PickIndexFromList(combatOptions))
        {
            case 0:
                //attack code here choose ability  is probably the right name for it
                break;
            case 1:
                //Lists usable consumables such as healing potions or such
                break;
            case 2:
                //attempt to flee results in a loss
                break;
        }
    }
}