using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public int Wave;

    private void Awake()
    {
        Wave = 1;
    }
}
