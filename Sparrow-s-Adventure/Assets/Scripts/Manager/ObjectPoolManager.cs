using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject[] prefabs;
        public int size;
    }

    [SerializeField] private List<Pool> Pools;
    public Dictionary<string, List<GameObject>> PoolDictionary;

    protected virtual void Awake()
    {
        PoolDictionary = new Dictionary<string, List<GameObject>>();

        InitializePool();
    }

    void InitializePool()
    {
        for (int i = 0; i < Pools.Count; i++)
        {
            List<GameObject> list = new List<GameObject>();
            for (int j = 0; j < Pools[i].size; j++)
            {
                GameObject obj = SpawnRandomObj(Pools[i].prefabs);
                obj.transform.SetParent(transform.GetChild(i));
                obj.SetActive(false);
                list.Add(obj);
            }
            PoolDictionary.Add(Pools[i].tag, list);
        }
    }

    GameObject SpawnRandomObj(GameObject[] prefabs)
    {
        int _id = Random.Range(0, prefabs.Length);
        GameObject _obj = Instantiate(prefabs[_id]);
        _obj.transform.rotation = Quaternion.Euler(_obj.transform.rotation.eulerAngles.x, _obj.transform.rotation.eulerAngles.y, prefabs[_id].transform.rotation.eulerAngles.z);

        return _obj;
    }

    public GameObject SpawnFromPool(string tag)
    {
        foreach (GameObject obj in PoolDictionary[tag])
        {
            if (!obj.activeSelf)
            {
                obj.transform.position = new Vector3(0,0,65 + (135 * (GameManager.Instance.Wave-1)));
                obj.SetActive(true);
                return obj;
            }
        }
        return null;
    }

    public int CountActiveInHierarchy(string tag)
    {
        int count = 0;
        foreach (GameObject obj in PoolDictionary[tag])
        {
            if (obj.activeInHierarchy)
            {
               count++;
            }
        }
        return count;
    }
}