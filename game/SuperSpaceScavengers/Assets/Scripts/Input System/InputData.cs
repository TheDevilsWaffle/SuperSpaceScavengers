///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — InputData.cs
///////////////////////////////////////////////////////////////////////////////////////////////////

//#pragma warning disable 0169
//#pragma warning disable 0649
//#pragma warning disable 0108
//#pragma warning disable 0414

using UnityEngine;
using System.Collections;
//using System.Collections.Generic;
using UnityEngine.UI;

#region ENUMS
public enum InputStatus
{
    RELEASED,
    HELD,
    PRESSED,
    INACTIVE
};
public enum ArcadeAxis
{
    INACTIVE,
    UP,
    UP_RIGHT,
    RIGHT,
    DOWN_RIGHT,
    DOWN,
    DOWN_LEFT,
    LEFT,
    UP_LEFT
};
#endregion

#region EVENTS
//public class EVENT_EXAMPLE : GameEvent
//{
//    public EVENT_EXAMPLE() { }
//}
#endregion

public class InputData
{
    #region FIELDS
    //name
    string name;
    public string Name
    {
        get { return name; }
        private set { name = value; }
    }
    Sprite icon_xbox;
    public Sprite IconXBox
    {
        get { return icon_xbox;}
        private set { icon_xbox = value; }
    }
    //current InputStatus
    InputStatus status;
    public InputStatus Status
    {
        get { return status; }
        private set { status = value; }
    }
    //x & y values
    Vector2 xy;
    public Vector2 XYValues
    {
        get { return xy; }
        private set { xy = value; }
    }
    //x & y raw values
    Vector2 xyRaw;
    public Vector2 XYRawValues
    {
        get { return xyRaw; }
        private set { xyRaw = value; }
    }
    //stick angle
    float angle;
    public float Angle
    {
        get { return angle; }
        private set { angle = value; }
    }
    //held
    float held = 0f;
    public float HeldDuration
    {
        get { return held; }
        private set { held = value; }
    }
    //inactive
    float inactive = 0f;
    public float InactiveDuration
    {
        get { return inactive; }
        private set { inactive = value; }
    }
    //arcade axis
    ArcadeAxis arcadeAxis;
    public ArcadeAxis ArcadeAxis
    {
        get { return arcadeAxis; }
        private set { arcadeAxis = value; }
    }
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

        //set/check initial values


    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
    
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Start
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
    
    }
    #endregion

    #region PUBLIC METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void SetName(string _name)
    {
        Name = _name;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void SetIcon_XBox(Sprite _sprite)
    {
        IconXBox = _sprite;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void SetStatus(InputStatus _status)
    {
        Status = _status;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void SetXYValue(Vector2 _values)
    {
        XYValues = _values;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void SetXYValue(float _x, float _y)
    {
        XYValues = new Vector2(_x, _y);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void SetXYRawValue(Vector2 _values)
    {
        XYRawValues = _values;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void SetXYRawValue(float _x, float _y)
    {
        XYRawValues = new Vector2(_x, _y);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void SetAngle(float _angle)
    {
        Angle = _angle;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void SetHeldDuration(float _duration)
    {
        if (_duration == 0f)
            HeldDuration = _duration;
        else
            HeldDuration += _duration;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void SetInactiveDuration(float _duration)
    {
        if (_duration == 0f)
            InactiveDuration = _duration;
        else
            InactiveDuration += _duration;
    }
    public void SetArcadeAxis(ArcadeAxis _axis)
    {
        ArcadeAxis = _axis;
    }
    #endregion
}