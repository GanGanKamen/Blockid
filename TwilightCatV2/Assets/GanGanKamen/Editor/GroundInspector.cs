using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GroundInspector : EditorWindow
{
    static GroundInspector groundInspector;
    string name = "ground";
    int sizeX = 50;
    int sizeY = 1;
    int sizeZ = 50;
    Vector3 pos = new Vector3(-25f, 0, -25f);
    SystemCtrl system = new SystemCtrl();

    [MenuItem("Window/ガンガン仮面/グランド作成")]
    public static void Open()
    {
        if(groundInspector == null)
        {
            groundInspector = CreateInstance<GroundInspector>();
        }
        groundInspector.ShowUtility();    
    }
    private void OnGUI()
    {
        GUILayout.Label("グランド設定", EditorStyles.boldLabel);

        name = EditorGUILayout.TextField("オブジェクト名前", name);
        sizeX = EditorGUILayout.IntField("X", sizeX);
        sizeY = EditorGUILayout.IntField("Y", sizeY);
        sizeZ = EditorGUILayout.IntField("Z", sizeZ);
        pos = EditorGUILayout.Vector3Field("位置", pos);
        if(GUI.Button(new Rect(100.0f, 200.0f, 120.0f, 20.0f), "作成"))
        {
            if(SystemCtrl.ground == null)
            {
                groundInspector.Close();
                Debug.Log("一度プロジェクトを実行してください");
            }
            else
            {
                system.GroundCreat(sizeX, sizeY, sizeZ, pos, name);
                groundInspector.Close();
                Debug.Log("大成功");
            }
            
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
