using UnityEngine;
using System.Collections.Generic;

public class MapManager : Singleton<MapManager>
{
    [SerializeField] private List<GameObject> map;

    private int index;

    private void Start()
    {
        index = 0;
    }

    public void RespawnMap()
    {
        map[index].transform.position = new Vector3(0, 0,(135 * (GameManager.Instance.Wave-1)));
        index = index == 0 ? 1 : 0;
    }
}
