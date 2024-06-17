using System.Collections.Generic;

public class CharacterManager : Singleton<CharacterManager>
{
    private Player player;

    public Player Player
    {
        get { return player; }
        set { player = value; }
    }

    private List<Enemy> enemy = new List<Enemy>();

    public List<Enemy> Enemy
    {
        get { return enemy; }
        set { enemy = value; }
    }
}
