///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — InputTester.cs
///////////////////////////////////////////////////////////////////////////////////////////////////

#pragma warning disable 0169
#pragma warning disable 0649
#pragma warning disable 0108
#pragma warning disable 0414

using UnityEngine;
using System.Collections;
//using System.Collections.Generic;
using UnityEngine.UI;

#region ENUMS
//public enum EnumStatus
//{
//	
//};
#endregion

#region EVENTS
//public class EVENT_EXAMPLE : GameEvent
//{
//    public EVENT_EXAMPLE() { }
//}
#endregion

public class InputTester : MonoBehaviour
{
    #region FIELDS
    [Header("BUTTONS")]
    [SerializeField]
    RectTransform y;
    RectTransform y_icon;
    Text y_status;
    Text y_value;
    Text y_held;
    Text y_inactive;

    [SerializeField]
    RectTransform b;
    RectTransform b_icon;
    Text b_status;
    Text b_value;
    Text b_held;
    Text b_inactive;

    [SerializeField]
    RectTransform a;
    RectTransform a_icon;
    Text a_status;
    Text a_value;
    Text a_held;
    Text a_inactive;

    [SerializeField]
    RectTransform x;
    RectTransform x_icon;
    Text x_status;
    Text x_value;
    Text x_held;
    Text x_inactive;

    [SerializeField]
    RectTransform select;
    RectTransform select_icon;
    Text select_status;
    Text select_value;
    Text select_held;
    Text select_inactive;

    [SerializeField]
    RectTransform start;
    RectTransform start_icon;
    Text start_status;
    Text start_value;
    Text start_held;
    Text start_inactive;

    [Header("BUMPERS")]
    [SerializeField]
    RectTransform lb;
    RectTransform lb_icon;
    Text lb_status;
    Text lb_value;
    Text lb_held;
    Text lb_inactive;

    [SerializeField]
    RectTransform rb;
    RectTransform rb_icon;
    Text rb_status;
    Text rb_value;
    Text rb_held;
    Text rb_inactive;

    [Header("TRIGGERS")]
    [SerializeField]
    RectTransform lt;
    RectTransform lt_icon;
    Text lt_status;
    Text lt_value;
    Text lt_raw;
    Text lt_held;
    Text lt_inactive;

    [SerializeField]
    RectTransform rt;
    RectTransform rt_icon;
    Text rt_status;
    Text rt_value;
    Text rt_raw;
    Text rt_held;
    Text rt_inactive;

    [Header("DPAD")]
    [SerializeField]
    RectTransform up;
    RectTransform up_icon;
    Text up_status;
    Text up_value;
    Text up_held;
    Text up_inactive;

    [SerializeField]
    RectTransform right;
    RectTransform right_icon;
    Text right_status;
    Text right_value;
    Text right_held;
    Text right_inactive;

    [SerializeField]
    RectTransform down;
    RectTransform down_icon;
    Text down_status;
    Text down_value;
    Text down_held;
    Text down_inactive;

    [SerializeField]
    RectTransform left;
    RectTransform left_icon;
    Text left_status;
    Text left_value;
    Text left_held;
    Text left_inactive;

    [Header("ANALOG STICKS")]
    [SerializeField]
    RectTransform l3;
    RectTransform l3_icon;
    Text l3_status;
    Text l3_value;
    Text l3_held;
    Text l3_inactive;

    [SerializeField]
    RectTransform ls;
    RectTransform ls_icon;
    Text ls_status;
    Text ls_value;
    Text ls_held;
    Text ls_inactive;
    Text ls_angle;
    Text ls_aa;

    [SerializeField]
    RectTransform r3;
    RectTransform r3_icon;
    Text r3_status;
    Text r3_value;
    Text r3_held;
    Text r3_inactive;

    [SerializeField]
    RectTransform rs;
    RectTransform rs_icon;
    Text rs_status;
    Text rs_value;
    Text rs_held;
    Text rs_inactive;
    Text rs_angle;
    Text rs_aa;

    [Header("COMBO")]
    [SerializeField]
    Image[] comboButtons;
    
    #endregion

    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// OnValidate
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void OnValidate()
    {
        //refs
        if(y != null)
        {
            y_icon = y.transform.Find("Icon").GetComponent<RectTransform>();
            y_status = y.transform.Find("Data").GetChild(0).GetComponent<Text>();
            y_value = y.transform.Find("Data").GetChild(1).GetChild(1).GetComponent<Text>();
            y_held = y.transform.Find("Data").GetChild(2).GetChild(1).GetComponent<Text>();
            y_inactive = y.transform.Find("Data").GetChild(3).GetChild(1).GetComponent<Text>();
        }
        if (b != null)
        {
            b_icon = b.transform.Find("Icon").GetComponent<RectTransform>();
            b_status = b.transform.Find("Data").GetChild(0).GetComponent<Text>();
            b_value = b.transform.Find("Data").GetChild(1).GetChild(1).GetComponent<Text>();
            b_held = b.transform.Find("Data").GetChild(2).GetChild(1).GetComponent<Text>();
            b_inactive = b.transform.Find("Data").GetChild(3).GetChild(1).GetComponent<Text>();
        }
        if (a != null)
        {
            a_icon = a.transform.Find("Icon").GetComponent<RectTransform>();
            a_status = a.transform.Find("Data").GetChild(0).GetComponent<Text>();
            a_value = a.transform.Find("Data").GetChild(1).GetChild(1).GetComponent<Text>();
            a_held = a.transform.Find("Data").GetChild(2).GetChild(1).GetComponent<Text>();
            a_inactive = a.transform.Find("Data").GetChild(3).GetChild(1).GetComponent<Text>();
        }
        if (x != null)
        {
            x_icon = x.transform.Find("Icon").GetComponent<RectTransform>();
            x_status = x.transform.Find("Data").GetChild(0).GetComponent<Text>();
            x_value = x.transform.Find("Data").GetChild(1).GetChild(1).GetComponent<Text>();
            x_held = x.transform.Find("Data").GetChild(2).GetChild(1).GetComponent<Text>();
            x_inactive = x.transform.Find("Data").GetChild(3).GetChild(1).GetComponent<Text>();
        }
        if (select != null)
        {
            select_icon = select.transform.Find("Icon").GetComponent<RectTransform>();
            select_status = select.transform.Find("Data").GetChild(0).GetComponent<Text>();
            select_value = select.transform.Find("Data").GetChild(1).GetChild(1).GetComponent<Text>();
            select_held = select.transform.Find("Data").GetChild(2).GetChild(1).GetComponent<Text>();
            select_inactive = select.transform.Find("Data").GetChild(3).GetChild(1).GetComponent<Text>();
        }
        if (start != null)
        {
            start_icon = start.transform.Find("Icon").GetComponent<RectTransform>();
            start_status = start.transform.Find("Data").GetChild(0).GetComponent<Text>();
            start_value = start.transform.Find("Data").GetChild(1).GetChild(1).GetComponent<Text>();
            start_held = start.transform.Find("Data").GetChild(2).GetChild(1).GetComponent<Text>();
            start_inactive = start.transform.Find("Data").GetChild(3).GetChild(1).GetComponent<Text>();
        }
        if (lb != null)
        {
            lb_icon = lb.transform.Find("Icon").GetComponent<RectTransform>();
            lb_status = lb.transform.Find("Data").GetChild(0).GetComponent<Text>();
            lb_value = lb.transform.Find("Data").GetChild(1).GetChild(1).GetComponent<Text>();
            lb_held = lb.transform.Find("Data").GetChild(2).GetChild(1).GetComponent<Text>();
            lb_inactive = lb.transform.Find("Data").GetChild(3).GetChild(1).GetComponent<Text>();
        }
        if (rb != null)
        {
            rb_icon = rb.transform.Find("Icon").GetComponent<RectTransform>();
            rb_status = rb.transform.Find("Data").GetChild(0).GetComponent<Text>();
            rb_value = rb.transform.Find("Data").GetChild(1).GetChild(1).GetComponent<Text>();
            rb_held = rb.transform.Find("Data").GetChild(2).GetChild(1).GetComponent<Text>();
            rb_inactive = rb.transform.Find("Data").GetChild(3).GetChild(1).GetComponent<Text>();
        }
        if (lt != null)
        {
            lt_icon = lt.transform.Find("Icon").GetComponent<RectTransform>();
            lt_status = lt.transform.Find("Data").GetChild(0).GetComponent<Text>();
            lt_value = lt.transform.Find("Data").GetChild(1).GetChild(1).GetComponent<Text>();
            lt_raw = lt.transform.Find("Data").GetChild(2).GetChild(1).GetComponent<Text>();
            lt_held = lt.transform.Find("Data").GetChild(3).GetChild(1).GetComponent<Text>();
            lt_inactive = lt.transform.Find("Data").GetChild(4).GetChild(1).GetComponent<Text>();
        }
        if (rt != null)
        {
            rt_icon = rt.transform.Find("Icon").GetComponent<RectTransform>();
            rt_status = rt.transform.Find("Data").GetChild(0).GetComponent<Text>();
            rt_value = rt.transform.Find("Data").GetChild(1).GetChild(1).GetComponent<Text>();
            rt_raw = rt.transform.Find("Data").GetChild(2).GetChild(1).GetComponent<Text>();
            rt_held = rt.transform.Find("Data").GetChild(3).GetChild(1).GetComponent<Text>();
            rt_inactive = rt.transform.Find("Data").GetChild(4).GetChild(1).GetComponent<Text>();
        }
        if (up != null)
        {
            up_icon = up.transform.Find("Icon").GetComponent<RectTransform>();
            up_status = up.transform.Find("Data").GetChild(0).GetComponent<Text>();
            up_value = up.transform.Find("Data").GetChild(1).GetChild(1).GetComponent<Text>();
            up_held = up.transform.Find("Data").GetChild(2).GetChild(1).GetComponent<Text>();
            up_inactive = up.transform.Find("Data").GetChild(3).GetChild(1).GetComponent<Text>();
        }
        if (right != null)
        {
            right_icon = right.transform.Find("Icon").GetComponent<RectTransform>();
            right_status = right.transform.Find("Data").GetChild(0).GetComponent<Text>();
            right_value = right.transform.Find("Data").GetChild(1).GetChild(1).GetComponent<Text>();
            right_held = right.transform.Find("Data").GetChild(2).GetChild(1).GetComponent<Text>();
            right_inactive = right.transform.Find("Data").GetChild(3).GetChild(1).GetComponent<Text>();
        }
        if (down != null)
        {
            down_icon = down.transform.Find("Icon").GetComponent<RectTransform>();
            down_status = down.transform.Find("Data").GetChild(0).GetComponent<Text>();
            down_value = down.transform.Find("Data").GetChild(1).GetChild(1).GetComponent<Text>();
            down_held = down.transform.Find("Data").GetChild(2).GetChild(1).GetComponent<Text>();
            down_inactive = down.transform.Find("Data").GetChild(3).GetChild(1).GetComponent<Text>();
        }
        if (left != null)
        {
            left_icon = left.transform.Find("Icon").GetComponent<RectTransform>();
            left_status = left.transform.Find("Data").GetChild(0).GetComponent<Text>();
            left_value = left.transform.Find("Data").GetChild(1).GetChild(1).GetComponent<Text>();
            left_held = left.transform.Find("Data").GetChild(2).GetChild(1).GetComponent<Text>();
            left_inactive = left.transform.Find("Data").GetChild(3).GetChild(1).GetComponent<Text>();
        }
        if (l3 != null)
        {
            l3_icon = l3.transform.Find("Icon").GetComponent<RectTransform>();
            l3_status = l3.transform.Find("Data").GetChild(0).GetComponent<Text>();
            l3_value = l3.transform.Find("Data").GetChild(1).GetChild(1).GetComponent<Text>();
            l3_held = l3.transform.Find("Data").GetChild(2).GetChild(1).GetComponent<Text>();
            l3_inactive = l3.transform.Find("Data").GetChild(3).GetChild(1).GetComponent<Text>();
        }
        if (ls != null)
        {
            ls_icon = ls.transform.Find("Icon").GetComponent<RectTransform>();
            ls_status = ls.transform.Find("Data").GetChild(0).GetComponent<Text>();
            ls_value = ls.transform.Find("Data").GetChild(1).GetChild(1).GetComponent<Text>();
            ls_held = ls.transform.Find("Data").GetChild(2).GetChild(1).GetComponent<Text>();
            ls_inactive = ls.transform.Find("Data").GetChild(3).GetChild(1).GetComponent<Text>();
            ls_angle = ls.transform.Find("Data").GetChild(4).GetChild(1).GetComponent<Text>();
            ls_aa = ls.transform.Find("Data").GetChild(5).GetChild(1).GetComponent<Text>();
        }
        if (r3 != null)
        {
            r3_icon = r3.transform.Find("Icon").GetComponent<RectTransform>();
            r3_status = r3.transform.Find("Data").GetChild(0).GetComponent<Text>();
            r3_value = r3.transform.Find("Data").GetChild(1).GetChild(1).GetComponent<Text>();
            r3_held = r3.transform.Find("Data").GetChild(2).GetChild(1).GetComponent<Text>();
            r3_inactive = r3.transform.Find("Data").GetChild(3).GetChild(1).GetComponent<Text>();
        }
        if (rs != null)
        {
            rs_icon = rs.transform.Find("Icon").GetComponent<RectTransform>();
            rs_status = rs.transform.Find("Data").GetChild(0).GetComponent<Text>();
            rs_value = rs.transform.Find("Data").GetChild(1).GetChild(1).GetComponent<Text>();
            rs_held = rs.transform.Find("Data").GetChild(2).GetChild(1).GetComponent<Text>();
            rs_inactive = rs.transform.Find("Data").GetChild(3).GetChild(1).GetComponent<Text>();
            rs_angle = rs.transform.Find("Data").GetChild(4).GetChild(1).GetComponent<Text>();
            rs_aa = rs.transform.Find("Data").GetChild(5).GetChild(1).GetComponent<Text>();
        }
        //set/check initial values

    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
        //listen to events
        SetSubscriptions();
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Start
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
    
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// SetSubscriptions
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void SetSubscriptions()
    {
        Events.instance.AddListener<EVENT_GAMEPAD_P1>(TestButtons);
    }
    #endregion

    #region UPDATE
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Update()
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Update()
    {

    #if false
        UpdateTesting();
    #endif

    }
    #endregion

    #region PUBLIC METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////

    #endregion

    #region PRIVATE METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void TestButtons(EVENT_GAMEPAD_P1 _event)
    {
        //y button
        y_status.text = _event.gamepad.y.Status.ToString();
        y_value.text = _event.gamepad.y.XYValues.x.ToString("F2");
        y_held.text = _event.gamepad.y.HeldDuration.ToString("F2");
        y_inactive.text = _event.gamepad.y.InactiveDuration.ToString("F2");
        //b button
        b_status.text = _event.gamepad.b.Status.ToString();
        b_value.text = _event.gamepad.b.XYValues.x.ToString("F2");
        b_held.text = _event.gamepad.b.HeldDuration.ToString("F2");
        b_inactive.text = _event.gamepad.b.InactiveDuration.ToString("F2");
        //a button
        a_status.text = _event.gamepad.a.Status.ToString();
        a_value.text = _event.gamepad.a.XYValues.x.ToString("F2");
        a_held.text = _event.gamepad.a.HeldDuration.ToString("F2");
        a_inactive.text = _event.gamepad.a.InactiveDuration.ToString("F2");
        //x button
        x_status.text = _event.gamepad.x.Status.ToString();
        x_value.text = _event.gamepad.x.XYValues.x.ToString("F2");
        x_held.text = _event.gamepad.x.HeldDuration.ToString("F2");
        x_inactive.text = _event.gamepad.x.InactiveDuration.ToString("F2");
        //select button
        select_status.text = _event.gamepad.select.Status.ToString();
        select_value.text = _event.gamepad.select.XYValues.x.ToString("F2");
        select_held.text = _event.gamepad.select.HeldDuration.ToString("F2");
        select_inactive.text = _event.gamepad.select.InactiveDuration.ToString("F2");
        //start button
        start_status.text = _event.gamepad.start.Status.ToString();
        start_value.text = _event.gamepad.start.XYValues.x.ToString("F2");
        start_held.text = _event.gamepad.start.HeldDuration.ToString("F2");
        start_inactive.text = _event.gamepad.start.InactiveDuration.ToString("F2");
        //lb
        lb_status.text = _event.gamepad.lb.Status.ToString();
        lb_value.text = _event.gamepad.lb.XYValues.x.ToString("F2");
        lb_held.text = _event.gamepad.lb.HeldDuration.ToString("F2");
        lb_inactive.text = _event.gamepad.lb.InactiveDuration.ToString("F2");
        //rb
        rb_status.text = _event.gamepad.rb.Status.ToString();
        rb_value.text = _event.gamepad.rb.XYValues.x.ToString("F2");
        rb_held.text = _event.gamepad.rb.HeldDuration.ToString("F2");
        rb_inactive.text = _event.gamepad.rb.InactiveDuration.ToString("F2");
        //lt
        lt_status.text = _event.gamepad.lt.Status.ToString();
        lt_value.text = _event.gamepad.lt.XYValues.x.ToString("F2");
        lt_raw.text = _event.gamepad.lt.XYRawValues.x.ToString("F2");
        lt_held.text = _event.gamepad.lt.HeldDuration.ToString("F2");
        lt_inactive.text = _event.gamepad.lt.InactiveDuration.ToString("F2");
        //rt
        rt_status.text = _event.gamepad.rt.Status.ToString();
        rt_value.text = _event.gamepad.rt.XYValues.x.ToString("F2");
        rt_raw.text = _event.gamepad.rt.XYRawValues.x.ToString("F2");
        rt_held.text = _event.gamepad.rt.HeldDuration.ToString("F2");
        rt_inactive.text = _event.gamepad.rt.InactiveDuration.ToString("F2");
        //dpad up
        up_status.text = _event.gamepad.dp_up.Status.ToString();
        up_value.text = _event.gamepad.dp_up.XYValues.x.ToString("F2");
        up_held.text = _event.gamepad.dp_up.HeldDuration.ToString("F2");
        up_inactive.text = _event.gamepad.dp_up.InactiveDuration.ToString("F2");
        //dpad right
        right_status.text = _event.gamepad.dp_right.Status.ToString();
        right_value.text = _event.gamepad.dp_right.XYValues.x.ToString("F2");
        right_held.text = _event.gamepad.dp_right.HeldDuration.ToString("F2");
        right_inactive.text = _event.gamepad.dp_right.InactiveDuration.ToString("F2");
        //dpad down
        down_status.text = _event.gamepad.dp_down.Status.ToString();
        down_value.text = _event.gamepad.dp_down.XYValues.x.ToString("F2");
        down_held.text = _event.gamepad.dp_down.HeldDuration.ToString("F2");
        down_inactive.text = _event.gamepad.dp_down.InactiveDuration.ToString("F2");
        //dpad left
        left_status.text = _event.gamepad.dp_left.Status.ToString();
        left_value.text = _event.gamepad.dp_left.XYValues.x.ToString("F2");
        left_held.text = _event.gamepad.dp_left.HeldDuration.ToString("F2");
        left_inactive.text = _event.gamepad.dp_left.InactiveDuration.ToString("F2");
        //l3
        l3_status.text = _event.gamepad.l3.Status.ToString();
        l3_value.text = _event.gamepad.l3.XYValues.x.ToString("F2");
        l3_held.text = _event.gamepad.l3.HeldDuration.ToString("F2");
        l3_inactive.text = _event.gamepad.l3.InactiveDuration.ToString("F2");
        //ls
        ls_status.text = _event.gamepad.ls.Status.ToString();
        ls_value.text = _event.gamepad.ls.XYValues.x.ToString("F2");
        ls_held.text = _event.gamepad.ls.HeldDuration.ToString("F2");
        ls_inactive.text = _event.gamepad.ls.InactiveDuration.ToString("F2");
        ls_angle.text = _event.gamepad.ls.Angle.ToString();
        ls_aa.text = _event.gamepad.ls.ArcadeAxis.ToString();

        //r3
        r3_status.text = _event.gamepad.r3.Status.ToString();
        r3_value.text = _event.gamepad.r3.XYValues.x.ToString("F2");
        r3_held.text = _event.gamepad.r3.HeldDuration.ToString("F2");
        r3_inactive.text = _event.gamepad.r3.InactiveDuration.ToString("F2");
        //ls
        rs_status.text = _event.gamepad.rs.Status.ToString();
        rs_value.text = _event.gamepad.rs.XYValues.x.ToString("F2");
        rs_held.text = _event.gamepad.rs.HeldDuration.ToString("F2");
        rs_inactive.text = _event.gamepad.rs.InactiveDuration.ToString("F2");
        rs_angle.text = _event.gamepad.rs.Angle.ToString();
        rs_aa.text = _event.gamepad.rs.ArcadeAxis.ToString();

        if (_event.gamepad.comboTracker != null)
        {
            InputData[] temp = new InputData[10];
            _event.gamepad.comboTracker.CopyTo(temp, 0);
            for (int i = 0; i < 10; ++i)
            {
                if (temp[i] != null)
                {
                    comboButtons[i].sprite = temp[i].IconXBox;
                }
            }
        }
    }
    #endregion

    #region ONDESTORY
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// OnDestroy
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void OnDestroy()
    {
        //remove event listeners
        Events.instance.RemoveListener<EVENT_GAMEPAD_P1>(TestButtons);
    }
    #endregion

    #region TESTING
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// UpdateTesting
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void UpdateTesting()
    {
        //Keypad 0
        if(Input.GetKeyDown(KeyCode.Keypad0))
        {

        }
        //Keypad 1
        if(Input.GetKeyDown(KeyCode.Keypad1))
        {
            
        }
        //Keypad 2
        if(Input.GetKeyDown(KeyCode.Keypad2))
        {
            
        }
        //Keypad 3
        if(Input.GetKeyDown(KeyCode.Keypad3))
        {
            
        }
        //Keypad 4
        if(Input.GetKeyDown(KeyCode.Keypad4))
        {
            
        }
        //Keypad 5
        if(Input.GetKeyDown(KeyCode.Keypad5))
        {
            
        }
        //Keypad 6
        if(Input.GetKeyDown(KeyCode.Keypad6))
        {
            
        }
    }
    #endregion
}