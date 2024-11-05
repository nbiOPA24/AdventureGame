public class CombatSession
{
    public List<Character> EnemyList { get;set; }
    public List<Character> PlayerList { get;set; }
    public int CurrentRound { get; set; }
    public bool StillInCombat { get; set; }

    public CombatSession(List<Character> playerList, List<Character> enemyList)
    {
        EnemyList = enemyList;
        PlayerList = playerList;
        CurrentRound = 1;
        StillInCombat = true;
    }
}

