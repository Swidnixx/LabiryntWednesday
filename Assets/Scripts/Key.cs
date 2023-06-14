using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

public class Key : Pickup
{
    public KeyType type;
    public string id;

    private void Start()
    {
        if (type == KeyType.Green) return;
        Register();

        bool alreadyPicked = AmIPickedYet();
        if (alreadyPicked)
            Destroy(gameObject);
    }

    protected override void OnPicked()
    {
        base.OnPicked();
        GameManager.Instance.AddKey( type );

        if (type == KeyType.Green) return;
        collectedKeys.Add(id, type); ///
    }

    public enum KeyType { Red, Green, Gold }


    static bool loaded = false;
    static List<string> registeredKeys = new List<string>();
    static Dictionary<string, KeyType> collectedKeys = new Dictionary<string, KeyType>();

    public void Register()
    {
        id = transform.position.x.ToString() + transform.position.y.ToString() + transform.position.z.ToString();

        if (registeredKeys.Contains(id)) return;

        registeredKeys.Add(id);
    }
    bool AmIPickedYet()
    {
        return collectedKeys.ContainsKey(id);
    }

    public static void SaveGoldKeys()
    {
        List<string> ids = collectedKeys.Where(pair => pair.Value == KeyType.Gold).Select( pair => pair.Key ).ToList();

        using( StreamWriter sw = new StreamWriter(Application.dataPath + "save.bin"))
        {
            foreach( string line in ids)
            {
                sw.WriteLine(line);
            }
        }
    }

    public static void LoadGoldKeys()
    {
        if (!File.Exists(Application.dataPath + "save.bin")) return;

        if (!loaded)
        {
            List<string> ids = new List<string>();
            using (StreamReader sr = new StreamReader(Application.dataPath + "save.bin"))
            {
                while (!sr.EndOfStream)
                {
                    ids.Add(sr.ReadLine());
                }
            }

            foreach (var id in ids)
            {
                collectedKeys.Add(id, KeyType.Gold);
            } 
        loaded = true;
        }

        foreach( var pair in collectedKeys)
        {
            GameManager.Instance.AddKey( pair.Value );
        }
    }

    public static void ClearRedKeys()
    {
        var redKeys = collectedKeys.Where(pair => pair.Value == KeyType.Red).ToList();
        foreach(var k in redKeys)
        {
            collectedKeys.Remove(k.Key);
        }
    }

    public static void PrintList()
    {
        Debug.Log("Registered:");
        foreach (var k in registeredKeys)
        {
            Debug.Log(k);
        }
        Debug.Log("Collected:");
        foreach (var k in collectedKeys)
        {
            Debug.Log(k.Key + ": " + k.Value);
        }
    }

}
