using Data;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataManager
{
    public Dictionary<string, EnemyData> Enemies = new();
    public Dictionary<string, Data.Skill> SkillDict { get; private set; } = new Dictionary<string, Data.Skill>();
    public PlayerData Player = new();

    public void Initialize()
    {
       Enemies = LoadJson<EnemyDataLoader, string, EnemyData>("EnemyData").MakeDictionary();
       SkillDict = LoadJson<Data.SkillData, string, Data.Skill>("skillData").MakeDictionary();
       Player = LoadJson("PlayerData");
    }


    Loader LoadJson<Loader, Key, Value> (string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Main.ResourceManager.GetResource<TextAsset>(path);
        return JsonConvert.DeserializeObject<Loader>(textAsset.text);
    }

    
    PlayerData LoadJson (string path)
    {
        TextAsset textAsset = Main.ResourceManager.GetResource<TextAsset>(path);
        return JsonConvert.DeserializeObject<PlayerData>(textAsset.text);
    }

    
    
}

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDictionary();
}
