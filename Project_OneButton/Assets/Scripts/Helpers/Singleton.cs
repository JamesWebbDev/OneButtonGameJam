using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject($"{typeof(T).Name}");
                instance = go.AddComponent<T>();
            }

            return instance;
        }
    }

    static T instance;

    internal void Awake()
    {
        if (instance == null)
            instance = GetComponent<T>();
        else
            Destroy(this);
    }
}