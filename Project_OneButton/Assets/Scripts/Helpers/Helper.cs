using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper 
{
    private static readonly Dictionary<float, WaitForSeconds> WaitDictionary = new Dictionary<float, WaitForSeconds>();
    public static WaitForSeconds GetWait(float time)
    {
        if (WaitDictionary.TryGetValue(time, out var wait)) return wait;

        WaitDictionary[time] = new WaitForSeconds(time);
        return WaitDictionary[time];
    }

    public static System.Random rand = new System.Random();

    public static void Shuffle<T>(this List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rand.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public static T GetRandom<T>(this List<T> list)
    {
        int count = list.Count;

        int randIndex = rand.Next(count);

        return list[randIndex];
    }

    public static Dictionary<TValue, TKey> Reverse<TKey, TValue>(this IDictionary<TKey, TValue> source)
    {
        var dictionary = new Dictionary<TValue, TKey>();
        foreach (var entry in source)
        {
            if (!dictionary.ContainsKey(entry.Value))
                dictionary.Add(entry.Value, entry.Key);
        }
        return dictionary;
    }
}
