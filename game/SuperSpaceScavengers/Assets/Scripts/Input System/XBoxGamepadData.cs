///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — XBoxGamepadData.cs
///////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class XBoxGamepadData
{
    #region FIELDS
    public Queue<InputData> comboTracker;

    #region ANALOG STICKS
    public InputData ls;
    public InputData l3;
    public InputData rs;
    public InputData r3;
    #endregion
    #region DPAD
    public InputData dp_up;
    public InputData dp_right;
    public InputData dp_down;
    public InputData dp_left;
    #endregion
    #region BUTTONS
    public InputData y;
    public InputData b;
    public InputData a;
    public InputData x;

    public InputData select;
    public InputData start;
    #endregion
    #region BUMPERS
    public InputData lb;
    public InputData rb;
    #endregion
    #region TRIGGERS
    public InputData lt;
    public InputData rt;
    #endregion

    #endregion

    #region INITIALIZE
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Constructor
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public XBoxGamepadData()
    {
        //create new InputDatas
        comboTracker = new Queue<InputData>();

        ls = new InputData();
        l3 = new InputData();
        rs = new InputData();
        r3 = new InputData();
        dp_up = new InputData();
        dp_right = new InputData();
        dp_down = new InputData();
        dp_left = new InputData();

        y = new InputData();
        b = new InputData();
        a = new InputData();
        x = new InputData();

        select = new InputData();
        start = new InputData();

        lb = new InputData();
        rb = new InputData();

        lt = new InputData();
        rt = new InputData();

        //set name and sprites
        ls.SetName("Left Stick");
        ls.SetIcon_XBox(Resources.Load<Sprite>("UI/XBoxIcons/spr_xbox_analog_left") as Sprite);
        rs.SetName("Right Stick");
        rs.SetIcon_XBox(Resources.Load<Sprite>("UI/XBoxIcons/spr_xbox_analog_right") as Sprite);

        l3.SetName("L3");
        l3.SetIcon_XBox(Resources.Load<Sprite>("UI/XBoxIcons/spr_xbox_analog_left_3") as Sprite);
        r3.SetName("R3");
        r3.SetIcon_XBox(Resources.Load<Sprite>("UI/XBoxIcons/spr_xbox_analog_right_3") as Sprite);

        dp_up.SetName("DPAD Up");
        dp_up.SetIcon_XBox(Resources.Load<Sprite>("UI/XBoxIcons/spr_xbox_dpad_up") as Sprite);
        dp_right.SetName("DPAD Right");
        dp_right.SetIcon_XBox(Resources.Load<Sprite>("UI/XBoxIcons/spr_xbox_dpad_right") as Sprite);
        dp_down.SetName("DPAD Down");
        dp_down.SetIcon_XBox(Resources.Load<Sprite>("UI/XBoxIcons/spr_xbox_dpad_down") as Sprite);
        dp_left.SetName("DPAD Left");
        dp_left.SetIcon_XBox(Resources.Load<Sprite>("UI/XBoxIcons/spr_xbox_dpad_left") as Sprite);

        y.SetName("Y");
        y.SetIcon_XBox(Resources.Load<Sprite>("UI/XBoxIcons/spr_xbox_y") as Sprite);
        b.SetName("B");
        b.SetIcon_XBox(Resources.Load<Sprite>("UI/XBoxIcons/spr_xbox_b") as Sprite);
        a.SetName("A");
        a.SetIcon_XBox(Resources.Load<Sprite>("UI/XBoxIcons/spr_xbox_a") as Sprite);
        x.SetName("X");
        x.SetIcon_XBox(Resources.Load<Sprite>("UI/XBoxIcons/spr_xbox_x") as Sprite);
        select.SetName("Select");
        select.SetIcon_XBox(Resources.Load<Sprite>("UI/XBoxIcons/spr_xbox_view") as Sprite);
        start.SetName("Start");
        start.SetIcon_XBox(Resources.Load<Sprite>("UI/XBoxIcons/spr_xbox_menu") as Sprite);

        lb.SetName("Left Bumper");
        lb.SetIcon_XBox(Resources.Load<Sprite>("UI/XBoxIcons/spr_xbox_bumper_left") as Sprite);
        rb.SetName("Right Bumper");
        rb.SetIcon_XBox(Resources.Load<Sprite>("UI/XBoxIcons/spr_xbox_bumper_right") as Sprite);

        lt.SetName("Left Trigger");
        lt.SetIcon_XBox(Resources.Load<Sprite>("UI/XBoxIcons/spr_xbox_trigger_left") as Sprite);
        rt.SetName("Right Trigger");
        rt.SetIcon_XBox(Resources.Load<Sprite>("UI/XBoxIcons/spr_xbox_trigger_right") as Sprite);
    }
    #endregion
}
