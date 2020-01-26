using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class StageSave : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Save()
    {
        UnityEditor.PrefabUtility.CreatePrefab("Assets/Resources/Stage.prefab", GameObject.FindGameObjectWithTag("Stage"));
        UnityEditor.AssetDatabase.SaveAssets();
    }
}
