public class CharacterManager : Singleton<CharacterManager>
{
    private Player player;

    public Player Player
    {
        get { return player; }
        set { player = value; }
    }
}
