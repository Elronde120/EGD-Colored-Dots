using System;
using System.IO;
using UnityEngine;

public static class JSONFileIO 
{
    public static void SaveToFile(string path, string JSONString)
    {
        StreamWriter writer = new StreamWriter(path, false);
        writer.Write(JSONString);
        writer.Close();
    }
    
    public static string ReadFile(string path)
    {
        StreamReader reader = new StreamReader(path);
        string returnValue = reader.ReadToEnd();
        reader.Close();
        return returnValue;
    }
    
    public static void CheckDirectory(string filePath)
    {
        if (!Directory.Exists(filePath))
        {
            Directory.CreateDirectory(filePath);
        }
    }
}

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}
