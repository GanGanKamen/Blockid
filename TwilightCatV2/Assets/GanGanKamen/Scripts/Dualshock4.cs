using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dualshock4 : MonoBehaviour
{
    //Dualshock4を複数使用するかつInputからボタンの割当が省くためのClass

    static public Vector2 LeftStick(int num)
    {
        switch (num)
        {
            case 1:
                return new Vector2(Input.GetAxis("L_XAxis_1"), -Input.GetAxis("L_YAxis_1"));
            case 2:
                return new Vector2(Input.GetAxis("L_XAxis_2"), -Input.GetAxis("L_YAxis_2"));
            case 3:
                return new Vector2(Input.GetAxis("L_XAxis_3"), -Input.GetAxis("L_YAxis_3"));
            case 4:
                return new Vector2(Input.GetAxis("L_XAxis_4"), -Input.GetAxis("L_YAxis_4"));
            default:
                return new Vector2(Input.GetAxis("L_XAxis_0"), -Input.GetAxis("L_YAxis_0"));
        }
    }

    static public Vector2 RightStick(int num)
    {
        switch (num)
        {
            case 1:
                return new Vector2(Input.GetAxis("R_XAxis_1"), -Input.GetAxis("R_YAxis_1"));
            case 2:
                return new Vector2(Input.GetAxis("R_XAxis_2"), -Input.GetAxis("R_YAxis_2"));
            case 3:
                return new Vector2(Input.GetAxis("R_XAxis_3"), -Input.GetAxis("R_YAxis_3"));
            case 4:
                return new Vector2(Input.GetAxis("R_XAxis_4"), -Input.GetAxis("R_YAxis_4"));
            default:
                return new Vector2(Input.GetAxis("R_XAxis_0"), -Input.GetAxis("R_YAxis_0"));
        }
    }

    static public Vector2 Directional(int num)
    {
        switch (num)
        {
            case 1:
                return new Vector2(Input.GetAxis("DPad_XAxis_1"), -Input.GetAxis("DPad_YAxis_1"));
            case 2:
                return new Vector2(Input.GetAxis("DPad_XAxis_2"), -Input.GetAxis("DPad_YAxis_2"));
            case 3:
                return new Vector2(Input.GetAxis("DPad_XAxis_3"), -Input.GetAxis("DPad_YAxis_3"));
            case 4:
                return new Vector2(Input.GetAxis("DPad_XAxis_4"), -Input.GetAxis("DPad_YAxis_4"));
            default:
                return new Vector2(Input.GetAxis("DPad_XAxis_0"), -Input.GetAxis("DPad_YAxis_0"));
        }
    }

    static public bool CircleDown(int num)
    {
        switch (num)
        {
            case 1:
                return Input.GetKeyDown(KeyCode.Joystick1Button2);
            case 2:
                return Input.GetKeyDown(KeyCode.Joystick2Button2);
            case 3:
                return Input.GetKeyDown(KeyCode.Joystick3Button2);
            case 4:
                return Input.GetKeyDown(KeyCode.Joystick4Button2);
            default:
                return Input.GetKeyDown(KeyCode.JoystickButton2);

        }
    }

    static public bool Circle(int num)
    {
        switch (num)
        {
            case 1:
                return Input.GetKey(KeyCode.Joystick1Button2);
            case 2:
                return Input.GetKey(KeyCode.Joystick2Button2);
            case 3:
                return Input.GetKey(KeyCode.Joystick3Button2);
            case 4:
                return Input.GetKey(KeyCode.Joystick4Button2);
            default:
                return Input.GetKey(KeyCode.JoystickButton2);
        }
    }

    static public bool CircleUp(int num)
    {
        switch (num)
        {
            case 1:
                return Input.GetKeyUp(KeyCode.Joystick1Button2);
            case 2:
                return Input.GetKeyUp(KeyCode.Joystick2Button2);
            case 3:
                return Input.GetKeyUp(KeyCode.Joystick3Button2);
            case 4:
                return Input.GetKeyUp(KeyCode.Joystick4Button2);
            default:
                return Input.GetKeyUp(KeyCode.JoystickButton2);
        }
    }

    static public bool CrossDown(int num)
    {
        switch (num)
        {
            case 1:
                return Input.GetKeyDown(KeyCode.Joystick1Button1);
            case 2:
                return Input.GetKeyDown(KeyCode.Joystick2Button1);
            case 3:
                return Input.GetKeyDown(KeyCode.Joystick3Button1);
            case 4:
                return Input.GetKeyDown(KeyCode.Joystick4Button1);
            default:
                return Input.GetKeyDown(KeyCode.JoystickButton1);
        }
    }

    static public bool Cross(int num)
    {
        switch (num)
        {
            case 1:
                return Input.GetKey(KeyCode.Joystick1Button1);
            case 2:
                return Input.GetKey(KeyCode.Joystick2Button1);
            case 3:
                return Input.GetKey(KeyCode.Joystick3Button1);
            case 4:
                return Input.GetKey(KeyCode.Joystick4Button1);
            default:
                return Input.GetKey(KeyCode.JoystickButton1);
        }
    }

    static public bool CrossUp(int num)
    {
        switch (num)
        {
            case 1:
                return Input.GetKeyUp(KeyCode.Joystick1Button1);
            case 2:
                return Input.GetKeyUp(KeyCode.Joystick2Button1);
            case 3:
                return Input.GetKeyUp(KeyCode.Joystick3Button1);
            case 4:
                return Input.GetKeyUp(KeyCode.Joystick4Button1);
            default:
                return Input.GetKeyUp(KeyCode.JoystickButton1);
        }
    }

    static public bool SquareDown(int num)
    {
        switch (num)
        {
            case 1:
                return Input.GetKeyDown(KeyCode.Joystick1Button0);
            case 2:
                return Input.GetKeyDown(KeyCode.Joystick2Button0);
            case 3:
                return Input.GetKeyDown(KeyCode.Joystick3Button0);
            case 4:
                return Input.GetKeyDown(KeyCode.Joystick4Button0);
            default:
                return Input.GetKeyDown(KeyCode.JoystickButton0);
        }
    }

    static public bool Square(int num)
    {
        switch (num)
        {
            case 1:
                return Input.GetKey(KeyCode.Joystick1Button0);
            case 2:
                return Input.GetKey(KeyCode.Joystick2Button0);
            case 3:
                return Input.GetKey(KeyCode.Joystick3Button0);
            case 4:
                return Input.GetKey(KeyCode.Joystick4Button0);
            default:
                return Input.GetKey(KeyCode.JoystickButton0);
        }
    }

    static public bool SqureUp(int num)
    {
        switch (num)
        {
            case 1:
                return Input.GetKeyUp(KeyCode.Joystick1Button0);
            case 2:
                return Input.GetKeyUp(KeyCode.Joystick2Button0);
            case 3:
                return Input.GetKeyUp(KeyCode.Joystick3Button0);
            case 4:
                return Input.GetKeyUp(KeyCode.Joystick4Button0);
            default:
                return Input.GetKeyUp(KeyCode.JoystickButton0);
        }
    }

    static public bool TriangleDown(int num)
    {
        switch (num)
        {
            case 1:
                return Input.GetKeyDown(KeyCode.Joystick1Button3);
            case 2:
                return Input.GetKeyDown(KeyCode.Joystick2Button3);
            case 3:
                return Input.GetKeyDown(KeyCode.Joystick3Button3);
            case 4:
                return Input.GetKeyDown(KeyCode.Joystick4Button3);
            default:
                return Input.GetKeyDown(KeyCode.JoystickButton3);
        }
    }

    static public bool Triangle(int num)
    {
        switch (num)
        {
            case 1:
                return Input.GetKey(KeyCode.Joystick1Button3);
            case 2:
                return Input.GetKey(KeyCode.Joystick2Button3);
            case 3:
                return Input.GetKey(KeyCode.Joystick3Button3);
            case 4:
                return Input.GetKey(KeyCode.Joystick4Button3);
            default:
                return Input.GetKey(KeyCode.JoystickButton3);
        }
    }

    static public bool TriangleUp(int num)
    {
        switch (num)
        {
            case 1:
                return Input.GetKeyUp(KeyCode.Joystick1Button3);
            case 2:
                return Input.GetKeyUp(KeyCode.Joystick2Button3);
            case 3:
                return Input.GetKeyUp(KeyCode.Joystick3Button3);
            case 4:
                return Input.GetKeyUp(KeyCode.Joystick4Button3);
            default:
                return Input.GetKeyUp(KeyCode.JoystickButton3);
        }
    }

    static public bool L1Down(int num)
    {
        switch (num)
        {
            case 1:
                return Input.GetKeyDown(KeyCode.Joystick1Button4);
            case 2:
                return Input.GetKeyDown(KeyCode.Joystick2Button4);
            case 3:
                return Input.GetKeyDown(KeyCode.Joystick3Button4);
            case 4:
                return Input.GetKeyDown(KeyCode.Joystick4Button4);
            default:
                return Input.GetKeyDown(KeyCode.JoystickButton4);

        }
    }

    static public bool L1(int num)
    {
        switch (num)
        {
            case 1:
                return Input.GetKey(KeyCode.Joystick1Button4);
            case 2:
                return Input.GetKey(KeyCode.Joystick2Button4);
            case 3:
                return Input.GetKey(KeyCode.Joystick3Button4);
            case 4:
                return Input.GetKey(KeyCode.Joystick4Button4);
            default:
                return Input.GetKey(KeyCode.JoystickButton4);

        }
    }

    static public bool L1Up(int num)
    {
        switch (num)
        {
            case 1:
                return Input.GetKeyUp(KeyCode.Joystick1Button4);
            case 2:
                return Input.GetKeyUp(KeyCode.Joystick2Button4);
            case 3:
                return Input.GetKeyUp(KeyCode.Joystick3Button4);
            case 4:
                return Input.GetKeyUp(KeyCode.Joystick4Button4);
            default:
                return Input.GetKeyUp(KeyCode.JoystickButton4);

        }
    }

    static public bool L2Down(int num)
    {
        switch (num)
        {
            case 1:
                return Input.GetKeyDown(KeyCode.Joystick1Button6);
            case 2:
                return Input.GetKeyDown(KeyCode.Joystick2Button6);
            case 3:
                return Input.GetKeyDown(KeyCode.Joystick3Button6);
            case 4:
                return Input.GetKeyDown(KeyCode.Joystick4Button6);
            default:
                return Input.GetKeyDown(KeyCode.JoystickButton6);

        }
    }

    static public bool L2(int num)
    {
        switch (num)
        {
            case 1:
                return Input.GetKey(KeyCode.Joystick1Button6);
            case 2:
                return Input.GetKey(KeyCode.Joystick2Button6);
            case 3:
                return Input.GetKey(KeyCode.Joystick3Button6);
            case 4:
                return Input.GetKey(KeyCode.Joystick4Button6);
            default:
                return Input.GetKey(KeyCode.JoystickButton6);

        }
    }

    static public bool L2Up(int num)
    {
        switch (num)
        {
            case 1:
                return Input.GetKeyUp(KeyCode.Joystick1Button6);
            case 2:
                return Input.GetKeyUp(KeyCode.Joystick2Button6);
            case 3:
                return Input.GetKeyUp(KeyCode.Joystick3Button6);
            case 4:
                return Input.GetKeyUp(KeyCode.Joystick4Button6);
            default:
                return Input.GetKeyUp(KeyCode.JoystickButton6);
        }
    }

    static public bool R1Down(int num)
    {
        switch (num)
        {
            case 1:
                return Input.GetKeyDown(KeyCode.Joystick1Button5);
            case 2:
                return Input.GetKeyDown(KeyCode.Joystick2Button5);
            case 3:
                return Input.GetKeyDown(KeyCode.Joystick3Button5);
            case 4:
                return Input.GetKeyDown(KeyCode.Joystick4Button5);
            default:
                return Input.GetKeyDown(KeyCode.JoystickButton5);

        }
    }

    static public bool R1(int num)
    {
        switch (num)
        {
            case 1:
                return Input.GetKey(KeyCode.Joystick1Button5);
            case 2:
                return Input.GetKey(KeyCode.Joystick2Button5);
            case 3:
                return Input.GetKey(KeyCode.Joystick3Button5);
            case 4:
                return Input.GetKey(KeyCode.Joystick4Button5);
            default:
                return Input.GetKey(KeyCode.JoystickButton5);

        }
    }

    static public bool R1Up(int num)
    {
        switch (num)
        {
            case 1:
                return Input.GetKeyUp(KeyCode.Joystick1Button5);
            case 2:
                return Input.GetKeyUp(KeyCode.Joystick2Button5);
            case 3:
                return Input.GetKeyUp(KeyCode.Joystick3Button5);
            case 4:
                return Input.GetKeyUp(KeyCode.Joystick4Button5);
            default:
                return Input.GetKeyUp(KeyCode.JoystickButton5);

        }
    }
    static public bool R2Down(int num)
    {
        switch (num)
        {
            case 1:
                return Input.GetKeyDown(KeyCode.Joystick1Button7);
            case 2:
                return Input.GetKeyDown(KeyCode.Joystick2Button7);
            case 3:
                return Input.GetKeyDown(KeyCode.Joystick3Button7);
            case 4:
                return Input.GetKeyDown(KeyCode.Joystick4Button7);
            default:
                return Input.GetKeyDown(KeyCode.JoystickButton7);

        }
    }

    static public bool R2(int num)
    {
        switch (num)
        {
            case 1:
                return Input.GetKey(KeyCode.Joystick1Button7);
            case 2:
                return Input.GetKey(KeyCode.Joystick2Button7);
            case 3:
                return Input.GetKey(KeyCode.Joystick3Button7);
            case 4:
                return Input.GetKey(KeyCode.Joystick4Button7);
            default:
                return Input.GetKey(KeyCode.JoystickButton7);

        }
    }

    static public bool R2Up(int num)
    {
        switch (num)
        {
            case 1:
                return Input.GetKeyUp(KeyCode.Joystick1Button7);
            case 2:
                return Input.GetKeyUp(KeyCode.Joystick2Button7);
            case 3:
                return Input.GetKeyUp(KeyCode.Joystick3Button7);
            case 4:
                return Input.GetKeyUp(KeyCode.Joystick4Button7);
            default:
                return Input.GetKeyUp(KeyCode.JoystickButton7);

        }
    }

    static public bool L3Down(int num)
    {
        switch (num)
        {
            case 1:
                return Input.GetKeyDown(KeyCode.Joystick1Button10);
            case 2:
                return Input.GetKeyDown(KeyCode.Joystick2Button10);
            case 3:
                return Input.GetKeyDown(KeyCode.Joystick3Button10);
            case 4:
                return Input.GetKeyDown(KeyCode.Joystick4Button10);
            default:
                return Input.GetKeyDown(KeyCode.JoystickButton10);
        }
    }

    static public bool L3(int num)
    {
        switch (num)
        {
            case 1:
                return Input.GetKey(KeyCode.Joystick1Button10);
            case 2:
                return Input.GetKey(KeyCode.Joystick2Button10);
            case 3:
                return Input.GetKey(KeyCode.Joystick3Button10);
            case 4:
                return Input.GetKey(KeyCode.Joystick4Button10);
            default:
                return Input.GetKey(KeyCode.JoystickButton10);
        }
    }

    static public bool L3Up(int num)
    {
        switch (num)
        {
            case 1:
                return Input.GetKeyUp(KeyCode.Joystick1Button10);
            case 2:
                return Input.GetKeyUp(KeyCode.Joystick2Button10);
            case 3:
                return Input.GetKeyUp(KeyCode.Joystick3Button10);
            case 4:
                return Input.GetKeyUp(KeyCode.Joystick4Button10);
            default:
                return Input.GetKeyUp(KeyCode.JoystickButton10);
        }
    }

    static public bool R3Down(int num)
    {
        switch (num)
        {
            case 1:
                return Input.GetKeyDown(KeyCode.Joystick1Button11);
            case 2:
                return Input.GetKeyDown(KeyCode.Joystick2Button11);
            case 3:
                return Input.GetKeyDown(KeyCode.Joystick3Button11);
            case 4:
                return Input.GetKeyDown(KeyCode.Joystick4Button11);
            default:
                return Input.GetKeyDown(KeyCode.JoystickButton11);
        }
    }

    static public bool R3(int num)
    {
        switch (num)
        {
            case 1:
                return Input.GetKey(KeyCode.Joystick1Button11);
            case 2:
                return Input.GetKey(KeyCode.Joystick2Button11);
            case 3:
                return Input.GetKey(KeyCode.Joystick3Button11);
            case 4:
                return Input.GetKey(KeyCode.Joystick4Button11);
            default:
                return Input.GetKey(KeyCode.JoystickButton11);
        }
    }

    static public bool R3Up(int num)
    {
        switch (num)
        {
            case 1:
                return Input.GetKeyUp(KeyCode.Joystick1Button11);
            case 2:
                return Input.GetKeyUp(KeyCode.Joystick2Button11);
            case 3:
                return Input.GetKeyUp(KeyCode.Joystick3Button11);
            case 4:
                return Input.GetKeyUp(KeyCode.Joystick4Button11);
            default:
                return Input.GetKeyUp(KeyCode.JoystickButton11);
        }
    }

    static public bool ShareDown(int num)
    {
        switch (num)
        {
            case 1:
                return Input.GetKeyDown(KeyCode.Joystick1Button8);
            case 2:
                return Input.GetKeyDown(KeyCode.Joystick2Button8);
            case 3:
                return Input.GetKeyDown(KeyCode.Joystick3Button8);
            case 4:
                return Input.GetKeyDown(KeyCode.Joystick4Button8);
            default:
                return Input.GetKeyDown(KeyCode.JoystickButton8);
        }
    }

    static public bool Share(int num)
    {
        switch (num)
        {
            case 1:
                return Input.GetKey(KeyCode.Joystick1Button8);
            case 2:
                return Input.GetKey(KeyCode.Joystick2Button8);
            case 3:
                return Input.GetKey(KeyCode.Joystick3Button8);
            case 4:
                return Input.GetKey(KeyCode.Joystick4Button8);
            default:
                return Input.GetKey(KeyCode.JoystickButton8);
        }
    }

    static public bool ShareUp(int num)
    {
        switch (num)
        {
            case 1:
                return Input.GetKeyUp(KeyCode.Joystick1Button8);
            case 2:
                return Input.GetKeyUp(KeyCode.Joystick2Button8);
            case 3:
                return Input.GetKeyUp(KeyCode.Joystick3Button8);
            case 4:
                return Input.GetKeyUp(KeyCode.Joystick4Button8);
            default:
                return Input.GetKeyUp(KeyCode.JoystickButton8);
        }
    }

    static public bool OptionsDown(int num)
    {
        switch (num)
        {
            case 1:
                return Input.GetKeyDown(KeyCode.Joystick1Button9);
            case 2:
                return Input.GetKeyDown(KeyCode.Joystick2Button9);
            case 3:
                return Input.GetKeyDown(KeyCode.Joystick3Button9);
            case 4:
                return Input.GetKeyDown(KeyCode.Joystick4Button9);
            default:
                return Input.GetKeyDown(KeyCode.JoystickButton9);
        }
    }

    static public bool Options(int num)
    {
        switch (num)
        {
            case 1:
                return Input.GetKey(KeyCode.Joystick1Button9);
            case 2:
                return Input.GetKey(KeyCode.Joystick2Button9);
            case 3:
                return Input.GetKey(KeyCode.Joystick3Button9);
            case 4:
                return Input.GetKey(KeyCode.Joystick4Button9);
            default:
                return Input.GetKey(KeyCode.JoystickButton9);
        }
    }

    static public bool OptionsUp(int num)
    {
        switch (num)
        {
            case 1:
                return Input.GetKeyUp(KeyCode.Joystick1Button9);
            case 2:
                return Input.GetKeyUp(KeyCode.Joystick2Button9);
            case 3:
                return Input.GetKeyUp(KeyCode.Joystick3Button9);
            case 4:
                return Input.GetKeyUp(KeyCode.Joystick4Button9);
            default:
                return Input.GetKeyUp(KeyCode.JoystickButton9);
        }
    }

    static public bool TouchPadDown(int num)
    {
        switch (num)
        {
            case 1:
                return Input.GetKeyDown(KeyCode.Joystick1Button13);
            case 2:
                return Input.GetKeyDown(KeyCode.Joystick2Button13);
            case 3:
                return Input.GetKeyDown(KeyCode.Joystick3Button13);
            case 4:
                return Input.GetKeyDown(KeyCode.Joystick4Button13);
            default:
                return Input.GetKeyDown(KeyCode.JoystickButton13);
        }
    }

    static public bool TouchPad(int num)
    {
        switch (num)
        {
            case 1:
                return Input.GetKey(KeyCode.Joystick1Button13);
            case 2:
                return Input.GetKey(KeyCode.Joystick2Button13);
            case 3:
                return Input.GetKey(KeyCode.Joystick3Button13);
            case 4:
                return Input.GetKey(KeyCode.Joystick4Button13);
            default:
                return Input.GetKey(KeyCode.JoystickButton13);
        }
    }

    static public bool TouchPadUp(int num)
    {
        switch (num)
        {
            case 1:
                return Input.GetKeyUp(KeyCode.Joystick1Button13);
            case 2:
                return Input.GetKeyUp(KeyCode.Joystick2Button13);
            case 3:
                return Input.GetKeyUp(KeyCode.Joystick3Button13);
            case 4:
                return Input.GetKeyUp(KeyCode.Joystick4Button13);
            default:
                return Input.GetKeyUp(KeyCode.JoystickButton13);
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
