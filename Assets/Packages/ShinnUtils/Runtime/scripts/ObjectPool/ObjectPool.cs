using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shinn
{
    public class ObjectPool : MonoBehaviour
    {
        public static ObjectPool Instance = null;

        [System.Serializable]
        public class Pool
        {
            public string tag;
            public GameObject prefab;
            public int size;
        }

        public List<Pool> pools = new List<Pool>();

        private Dictionary<string, Queue<GameObject>> poolDictionary;

        private void Awake()
        {
            Instance = this;
        }
        private void Start()
        {
            poolDictionary = new Dictionary<string, Queue<GameObject>>();
            foreach (Pool pool in pools)
            {
                Queue<GameObject> objectPool = new Queue<GameObject>();
                for (int i = 0; i < pool.size; i++)
                {
                    GameObject go = Instantiate(pool.prefab);
                    DontDestroyOnLoad(go);
                    go.SetActive(false);
                    objectPool.Enqueue(go);
                }
                poolDictionary.Add(pool.tag, objectPool);
            }
        }

        public GameObject SpawnFromPool(string _tag, Vector3 _pos, Quaternion _rot, Vector3 _scl)
        {
            if (!poolDictionary.ContainsKey(_tag))
            {
                Debug.LogError("Pool with tag " + tag + " doesn't excist.");
                return null;
            }

            GameObject objectToSpawn = poolDictionary[_tag].Dequeue();
            objectToSpawn.SetActive(true);
            objectToSpawn.transform.SetPositionAndRotation(_pos, _rot);
            objectToSpawn.transform.localScale = _scl;
            objectToSpawn.SendMessage("OnObjectSwam");
            poolDictionary[_tag].Enqueue(objectToSpawn);

            return objectToSpawn;
        }
    }
}