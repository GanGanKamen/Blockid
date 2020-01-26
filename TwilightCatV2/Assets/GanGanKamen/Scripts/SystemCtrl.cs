using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemCtrl : MonoBehaviour
{
    static public GameObject block;
    static public GameObject ground;
    static public GameObject blockVFX;
    static public GameObject beam;
    static public GameObject doublebeam;
    static public GameObject lifeIcon;
    static public GameObject nomalBlock;
    static public GameObject nomalBlock2;
    static public GameObject rushBlock;
    static public GameObject bakudanBlock;
    static public GameObject gannerBlock;
    static public GameObject nomalCancel;
    static public GameObject rushCancel;
    static public GameObject bakudanCancel;
    static public GameObject gannerCancel;
    static public GameObject Tank;
    static public GameObject WallBlock;
    static public GameObject Trap;


    // Start is called before the first frame update
    private void Awake()
    {
        block = Resources.Load<GameObject>("Block");
        ground = Resources.Load<GameObject>("Ground");
        blockVFX = Resources.Load<GameObject>("BlockVFX");
        beam = Resources.Load<GameObject>("Beam/Beam");
        doublebeam = Resources.Load<GameObject>("Beam/DoubleBeam");
        lifeIcon = Resources.Load<GameObject>("GUI/Life");
        nomalBlock = Resources.Load<GameObject>("Block/NormalBlock");
        nomalBlock2 = Resources.Load<GameObject>("Block/NormalBlock2");
        rushBlock = Resources.Load<GameObject>("Block/RushBlock");
        bakudanBlock = Resources.Load<GameObject>("Block/BakudanBlock");
        //gannerBlock =
        nomalCancel = Resources.Load<GameObject>("BlockCancel/NormalCancel");
        rushCancel = Resources.Load<GameObject>("BlockCancel/RushCancel");
        bakudanCancel = Resources.Load<GameObject>("BlockCancel/BakudanCancel");
        Tank = Resources.Load<GameObject>("Block/Tank");
        WallBlock = Resources.Load<GameObject>("Block/Wall");
        Trap = Resources.Load<GameObject>("Block/SlowTrap");
        //gannerCancel = 
    }

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GroundCreat(int sizeX,int sizeY, int sizeZ, Vector3 pos, string name)  //地面作成ツール用関数
    {
        GameObject groundObj = new GameObject(name.ToString());
        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                for (int z = 0; z < sizeZ; z++)
                {
                    GameObject groundChild = Instantiate(ground, new Vector3(pos.x + x, pos.y + y, pos.z + z), Quaternion.identity);
                    groundChild.tag = "CanChange";
                    groundChild.transform.parent = groundObj.transform;
                }
            }
        }
    }
}
