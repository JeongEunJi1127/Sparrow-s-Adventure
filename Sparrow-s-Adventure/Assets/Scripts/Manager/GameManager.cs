using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public int Wave;
    public int Point;

    private void Awake()
    {
        Wave = 1;
    }

    public void NextWave()
    {
        Wave++;
        Point+=10;

        UIManager.Instance.UpdateNextWaveValText();
    }

    public void UsePoint()
    {
        Point--;
    }
}
