using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad_Manager : MonoBehaviour
{
    public static DontDestroyOnLoad_Manager Instance;

    public List<GameObject> objs_DontDestroy;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            if (Instance != this)
            {
                Destroy(this.gameObject);
            }
        }

        DontDestroy();
    }

    private void DontDestroy()
    {
        for (int i = 0; i < objs_DontDestroy.Count; i++)
        {
            DontDestroyOnLoad(objs_DontDestroy[i]);
        }
    }

    public void AddDontDestroy(GameObject _addObj)
    {
        objs_DontDestroy.Add(_addObj); 

        DontDestroyOnLoad(_addObj);
    }
}
