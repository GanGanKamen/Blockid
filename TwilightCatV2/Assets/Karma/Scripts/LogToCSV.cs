using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LogToCSV : MonoBehaviour
{
    static bool existsInstance = false;
    static int circle, square, cross, L1, L2, R1, R2;
    static bool GameClear,IsCounted;
    static StreamWriter logs;
    static string Fname;

    void Awake()
    {
        if (existsInstance)
        {
            Destroy(gameObject);
            return;
        }
        existsInstance = true;
        DontDestroyOnLoad(gameObject);
        Cursor.visible = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        Fname = "KeyLog-" + System.DateTime.Now.Month + "-" + System.DateTime.Now.Day
            + "-" + System.DateTime.Now.Hour + "-" + System.DateTime.Now.Minute + ".csv";

        logs = new StreamWriter(Fname, true);
        logs.WriteLine("circle, square, cross, L1, L2, R1, R2,Clear");
        logs.Flush();
        logs.Close();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsCounted == true)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                cross += 1;
            }
            if (Input.GetButtonDown("Fire2"))
            {
                circle += 1;
            }
            if (Input.GetButtonDown("Fire0"))
            {
                square += 1;
            }
            if (Input.GetButtonDown("Shoulder1"))
            {
                L1 += 1;
            }
            if (Input.GetButtonDown("Shoulder2"))
            {
                R1 += 1;
            }
            if (Input.GetButtonDown("Trigger2"))
            {
                R2 += 1;
            }
            if (Input.GetButtonDown("Trigger1"))
            {
                L2 += 1;
            }

        }
    }

    public static void Log_CountStart()
    {
        circle = 0;
        square = 0;
        cross = 0;
        L1 = 0;
        L2 = 0;
        R1 = 0;
        R2 = 0;
        GameClear = false;
        IsCounted = true;
    }

    public static void Log_GameClear()
    {
        IsCounted = false;
        logs = new StreamWriter(Fname, true);
        logs.WriteLine(circle+","+ square+","+ cross+","+ L1+","+ L2+","+ R1+","+ R2+",true");
        logs.Flush();
        logs.Close();
        Debug.Log("save");
    }

    public static void Log_GameOver()
    {
        IsCounted = false;
        logs = new StreamWriter(Fname, true);
        logs.WriteLine(circle + "," + square + "," + cross + "," + L1 + "," + L2 + "," + R1 + "," + R2 + ",false");
        logs.Flush();
        logs.Close();
    }
}
