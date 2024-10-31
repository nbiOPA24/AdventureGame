public class CombatSession
{
    public Character Player { get;set; }
    public List<Character> EnemyList { get;set; }
    public List<Character> PlayerList { get;set; }
    public int CurrentRound { get; set; }
    public bool StillInCombat { get; set; }

    public CombatSession(Character player, List<Character> enemyList)
    {
        Player = player;
        EnemyList = enemyList;
        PlayerList = new List<Character> { player };
        CurrentRound = 1;
        StillInCombat = true;
    }
}

