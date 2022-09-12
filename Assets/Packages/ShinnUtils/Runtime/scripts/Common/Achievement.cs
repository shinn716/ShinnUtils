using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Achievement : MonoBehaviour
{
    [Serializable]
    public class Item
    {
        public Item(string _name, bool _check, string _iconUrl)
        {
            name = _name;
            iconUrl = _iconUrl;
            check = _check;
            iconUrl = _iconUrl;
        }

        public string name = string.Empty;
        public string iconUrl = string.Empty;
        public bool check = false;
    }
    [Serializable]
    private class AchievementTable
    {
        public Item[] items;
    }

    [SerializeField] private bool LoadFromPlayerPrefs = true;
    [SerializeField] private AchievementTable achievementTable;


    public Dictionary<string, Item> Storage { get; private set; } = new Dictionary<string, Item>();
    public string[] NameList { get; private set; }


    private void Awake()
    {
        if (!LoadFromPlayerPrefs)
            return;
        var load = PlayerPrefs.GetString("achievementTable");
        var json = JsonUtility.FromJson<AchievementTable>(load);

        if (json == null)
            return;

        if (json.items.Length != 0)
            achievementTable = json;
    }
    private void Start()
    {
        if (achievementTable.items.Length == 0)
            return;

        NameList = new string[achievementTable.items.Length];
        foreach (var i in achievementTable.items)
        {
            Storage.Add(i.name, new Item(i.name, i.check, i.iconUrl));
            NameList[Array.IndexOf(achievementTable.items, i)] = i.name;
        }
    }
    private void OnDestroy()
    {
        if (!LoadFromPlayerPrefs)
            return;

        var output = JsonUtility.ToJson(achievementTable);
        PlayerPrefs.SetString("achievementTable", output);
        print(output);
    }


    public void SetValue(string _name, bool _check = true)
    {
        Storage[_name].check = _check;
    }
    public void Clear()
    {
        Storage.Clear();
        NameList = new string[0];
    }
    public void DeletePlayerPrefs()
    {
        if(LoadFromPlayerPrefs)
            PlayerPrefs.DeleteAll();
    }
}
