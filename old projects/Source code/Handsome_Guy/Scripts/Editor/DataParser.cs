using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Graphs;
using UnityEngine;

public class DataTransformer : EditorWindow
{
#if UNITY_EDITOR

    [MenuItem("Tools/ParseExcel")]
    public static void ParseExcel()
    {
        ParseEnemyData("Enemy");
        ParsePlayerData("Player");
    }

    private static void ParseEnemyData(string fileName)
    {
        EnemyDataLoader loader = new();

        string[] lines = File.ReadAllText($"{Application.dataPath}/@Resources/Data/CsvData/{fileName}Data.csv").Split("\n");

        for (int y = 1; y < lines.Length; y++)
        {
            string[] row = lines[y].Replace("\r", "").Split(',');
            if (row.Length == 0 || string.IsNullOrEmpty(row[0])) continue;

            loader.Enemies.Add(new()
            {
                key = row[0],
                HP = ConvertValue<int>(row[1]),
                Speed = ConvertValue<float>(row[2]),
                Damage = ConvertValue<int>(row[3]),
            });
        }

        string jsonStr = JsonConvert.SerializeObject(loader, Formatting.Indented);
        File.WriteAllText($"{Application.dataPath}/@Resources/Data/JsonData/{fileName}Data.json", jsonStr);
        AssetDatabase.Refresh();
    }

    private static void ParsePlayerData(string fileName)
    {
        PlayerData loader = new();

        string[] lines = File.ReadAllText($"{Application.dataPath}/@Resources/Data/CsvData/{fileName}Data.csv").Split("\n");

        for (int y = 1; y < lines.Length; y++)
        {
            string[] row = lines[y].Replace("\r", "").Split(',');
            if (row.Length == 0 || string.IsNullOrEmpty(row[0])) continue;

            loader.id = row[0];
            loader.currrentHp = ConvertValue<float>(row[1]);
            loader.maxHp = ConvertValue<float>(row[2]);
            loader.moveSpeed = ConvertValue<float>(row[3]);
            loader.damage = ConvertValue<float>(row[4]);
            loader.attackSpeed = ConvertValue<float>(row[5]);
            loader.damageReduceRatio = ConvertValue<float>(row[6]);
            loader.sightRange = ConvertValue<float>(row[7]);
            loader.killCount = ConvertValue<int>(row[8]);
        }

        string jsonStr = JsonConvert.SerializeObject(loader, Formatting.Indented);
        File.WriteAllText($"{Application.dataPath}/@Resources/Data/JsonData/{fileName}Data.json", jsonStr);
        AssetDatabase.Refresh();
    }

    private static T ConvertValue<T>(string value)
    {
        if (string.IsNullOrEmpty(value)) return default;
        TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
        return (T)converter.ConvertFromString(value);
    }

    private static List<T> ConvertList<T>(string value)
    {
        if (string.IsNullOrEmpty(value)) return new();
        return value.Split('|').Select(x => ConvertValue<T>(x)).ToList();
    }

#endif
}