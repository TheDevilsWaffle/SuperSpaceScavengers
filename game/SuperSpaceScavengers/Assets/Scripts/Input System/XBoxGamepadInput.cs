///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — XBoxGamepadInput.cs
///////////////////////////////////////////////////////////////////////////////////////////////////

#pragma warning disable 0169
#pragma warning disable 0649
#pragma warning disable 0108
#pragma warning disable 0414

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XInputDotNetPure;

#region ENUMS
public enum XBoxButtons
{
    Y, B, A, X,
    SELECT, START,
    LT, RT, LB, RB,
    DP_UP, DP_RIGHT, DP_DOWN, DP_LEFT,
    LS, RS, L3, R3
};
#endregion

#region EVENTS
public class EVENT_GAMEPAD_P1 : GameEvent
{
    public XBoxGamepadData gamepad;
    public EVENT_GAMEPAD_P1(XBoxGamepadData _gamepad)
    {
        gamepad = _gamepad;
    }
}
public class EVENT_GAMEPAD_P2 : GameEvent
{
    public XBoxGamepadData gamepad;
    public EVENT_GAMEPAD_P2(XBoxGamepadData _gamepad)
    {
        gamepad = _gamepad;
    }
}
public class EVENT_GAMEPAD_P3 : GameEvent
{
    public XBoxGamepadData gamepad;
    public EVENT_GAMEPAD_P3(XBoxGamepadData _gamepad)
    {
        gamepad = _gamepad;
    }
}
public class EVENT_GAMEPAD_P4 : GameEvent
{
    public XBoxGamepadData gamepad;
    public EVENT_GAMEPAD_P4(XBoxGamepadData _gamepad)
    {
        gamepad = _gamepad;
    }
}
#endregion

public class XBoxGamepadInput : MonoBehaviour
{
    #region FIELDS
    [Header("ENABLE/DISABLE")]
    public bool isEnabled = true;

    [Header("PLAYER")]
    [Range(1, 4)]
    [SerializeField]
    public static int playerCount = 1;

    [Header("DEAD ZONES")]
    [Range(0, 1)]
    [SerializeField]
    float triggerDeadZone = 0.2f;
    [Range(0, 1)]
    [SerializeField]
    float analogStickDeadZone = 0.2f;

    [Header("MAX")]
    [SerializeField]
    float maxHeldDuration = 10f;
    [SerializeField]
    float maxInactiveDuration = 10f;
    [SerializeField]
    int maxButtonsRemembered = 10;

    //arcade axis values
    float up = -90f;
    float up_right = -45f;
    float right = 0f;
    float down_right = 45f;
    float down = 90f;
    float down_left = 135f;
    float left = -180f;
    float up_left = -135f;
    float axisLimit = 22.5f;

    //gamepad states
    GamePadState currentState;
    GamePadState previousState;

    //gamepads/players
    [HideInInspector]
    public static List<XBoxGamepadData> gamepads;
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
        up = -90f;
        up_right = -45f;
        right = 0f;
        down_right = 45f;
        down = 90f;
        down_left = 135f;
        left = -180f;
        up_left = -135f;
        axisLimit = 22.5f;

        gamepads = new List<XBoxGamepadData>();
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
        if (isEnabled)
        {
            //popluate list of players based on numberOfPlayers
            for (int _index = 0; _index < playerCount; ++_index)
            {
                GamePadState _testState = GamePad.GetState((PlayerIndex)_index);
                if (_testState.IsConnected)
                    gamepads.Add(new XBoxGamepadData());
            }
        }

        //SetSubscriptions();
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
        //Events.instance.AddListener<>();
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
        //gamepad enabled? go through the update loop
        if (isEnabled)
        {
            for (int _index = 0; _index < playerCount; ++_index)
            {
                //first test to make sure there's a controller there to update
                GamePadState _testState = GamePad.GetState((PlayerIndex)_index);
                if (_testState.IsConnected)
                {
                    //update the previous and currentstate
                    previousState = currentState;
                    currentState = GamePad.GetState((PlayerIndex)_index);

                    //check the gamepad for input
                    UpdateDPad(_index, previousState, currentState);
                    UpdateSticks(_index, previousState, currentState);
                    UpdateButtons(_index, previousState, currentState);
                    UpdateBumpers(_index, previousState, currentState);
                    UpdateTriggers(_index, previousState, currentState);

                    //raise new game event
                    Broadcast(_index);
                }
                else
                    Debug.LogWarning("WARNING! Player(" + _index + ") no longer exists? ");
            }
        }
    }
    #endregion

    #region PRIVATE METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// updates the statuses of the DPad
    /// </summary>
    /// <param name="_previous">last frame GamePad Input</param>
    /// <param name="_current">current frame GamePad Input</param>
    ////////////////////////////////////////////////////////////////////////////////////////////////
    void UpdateDPad(int _index, GamePadState _previous, GamePadState _current)
    {
        #region DPAD UP
        //RELEASED
        if (_previous.DPad.Up == ButtonState.Pressed && _current.DPad.Up == ButtonState.Released)
        {
            gamepads[_index].dp_up.SetStatus(InputStatus.RELEASED);
            gamepads[_index].dp_up.SetXYValue(0f, 0f);
            gamepads[_index].dp_up.SetInactiveDuration(Time.deltaTime);
        }
        //HELD
        else if (_previous.DPad.Up == ButtonState.Pressed && _current.DPad.Up == ButtonState.Pressed)
        {
            gamepads[_index].dp_up.SetStatus(InputStatus.HELD);
            gamepads[_index].dp_up.SetXYValue(1f, 1f);
            gamepads[_index].dp_up.SetHeldDuration(Time.deltaTime);
            gamepads[_index].dp_up.SetInactiveDuration(0f);
        }
        //PRESSED
        else if (_previous.DPad.Up == ButtonState.Released && _current.DPad.Up == ButtonState.Pressed)
        {
            gamepads[_index].dp_up.SetStatus(InputStatus.PRESSED);
            gamepads[_index].dp_up.SetXYValue(1f, 1f);
            gamepads[_index].dp_up.SetHeldDuration(Time.deltaTime);

            UpdateCombo(_index, gamepads[_index].dp_up);
        }
        //INACTIVE
        else
        {
            gamepads[_index].dp_up.SetStatus(InputStatus.INACTIVE);
            gamepads[_index].dp_up.SetXYValue(0f, 0f);
            gamepads[_index].dp_up.SetHeldDuration(0f);
            gamepads[_index].dp_up.SetInactiveDuration(Time.deltaTime);
        }
        #endregion
        #region DPAD RIGHT
        //RELEASED
        if (_previous.DPad.Right == ButtonState.Pressed && _current.DPad.Right == ButtonState.Released)
        {
            gamepads[_index].dp_right.SetStatus(InputStatus.RELEASED);
            gamepads[_index].dp_right.SetXYValue(0f, 0f);
            gamepads[_index].dp_right.SetInactiveDuration(Time.deltaTime);
        }
        //HELD
        else if (_previous.DPad.Right == ButtonState.Pressed && _current.DPad.Right == ButtonState.Pressed)
        {
            gamepads[_index].dp_right.SetStatus(InputStatus.HELD);
            gamepads[_index].dp_right.SetXYValue(1f, 1f);
            gamepads[_index].dp_right.SetHeldDuration(Time.deltaTime);
            gamepads[_index].dp_right.SetInactiveDuration(0f);
        }
        //PRESSED
        else if (_previous.DPad.Right == ButtonState.Released && _current.DPad.Right == ButtonState.Pressed)
        {
            gamepads[_index].dp_right.SetStatus(InputStatus.PRESSED);
            gamepads[_index].dp_right.SetXYValue(1f, 1f);
            gamepads[_index].dp_right.SetHeldDuration(Time.deltaTime);

            UpdateCombo(_index, gamepads[_index].dp_right);
        }
        //INACTIVE
        else
        {
            gamepads[_index].dp_right.SetStatus(InputStatus.INACTIVE);
            gamepads[_index].dp_right.SetXYValue(0f, 0f);
            gamepads[_index].dp_right.SetHeldDuration(0f);
            gamepads[_index].dp_right.SetInactiveDuration(Time.deltaTime);
        }
        #endregion
        #region DPAD DOWN
        //RELEASED
        if (_previous.DPad.Down == ButtonState.Pressed && _current.DPad.Down == ButtonState.Released)
        {
            gamepads[_index].dp_down.SetStatus(InputStatus.RELEASED);
            gamepads[_index].dp_down.SetXYValue(0f, 0f);
            gamepads[_index].dp_down.SetInactiveDuration(Time.deltaTime);
        }
        //HELD
        else if (_previous.DPad.Down == ButtonState.Pressed && _current.DPad.Down == ButtonState.Pressed)
        {
            gamepads[_index].dp_down.SetStatus(InputStatus.HELD);
            gamepads[_index].dp_down.SetXYValue(1f, 1f);
            gamepads[_index].dp_down.SetHeldDuration(Time.deltaTime);
            gamepads[_index].dp_down.SetInactiveDuration(0f);
        }
        //PRESSED
        else if (_previous.DPad.Down == ButtonState.Released && _current.DPad.Down == ButtonState.Pressed)
        {
            gamepads[_index].dp_down.SetStatus(InputStatus.PRESSED);
            gamepads[_index].dp_down.SetXYValue(1f, 1f);
            gamepads[_index].dp_down.SetHeldDuration(Time.deltaTime);

            UpdateCombo(_index, gamepads[_index].dp_down);
        }
        //INACTIVE
        else
        {
            gamepads[_index].dp_down.SetStatus(InputStatus.INACTIVE);
            gamepads[_index].dp_down.SetXYValue(0f, 0f);
            gamepads[_index].dp_down.SetHeldDuration(0f);
            gamepads[_index].dp_down.SetInactiveDuration(Time.deltaTime);
        }
        #endregion
        #region DPAD LEFT
        //RELEASED
        if (_previous.DPad.Left == ButtonState.Pressed && _current.DPad.Left == ButtonState.Released)
        {
            gamepads[_index].dp_left.SetStatus(InputStatus.RELEASED);
            gamepads[_index].dp_left.SetXYValue(0f, 0f);
            gamepads[_index].dp_left.SetInactiveDuration(Time.deltaTime);
        }
        //HELD
        else if (_previous.DPad.Left == ButtonState.Pressed && _current.DPad.Left == ButtonState.Pressed)
        {
            gamepads[_index].dp_left.SetStatus(InputStatus.HELD);
            gamepads[_index].dp_left.SetXYValue(1f, 1f);
            gamepads[_index].dp_left.SetHeldDuration(Time.deltaTime);
            gamepads[_index].dp_left.SetInactiveDuration(0f);
        }
        //PRESSED
        else if (_previous.DPad.Left == ButtonState.Released && _current.DPad.Left == ButtonState.Pressed)
        {
            gamepads[_index].dp_left.SetStatus(InputStatus.PRESSED);
            gamepads[_index].dp_left.SetXYValue(1f, 1f);
            gamepads[_index].dp_left.SetHeldDuration(Time.deltaTime);

            UpdateCombo(_index, gamepads[_index].dp_left);
        }
        //INACTIVE
        else
        {
            gamepads[_index].dp_left.SetStatus(InputStatus.INACTIVE);
            gamepads[_index].dp_left.SetXYValue(0f, 0f);
            gamepads[_index].dp_left.SetHeldDuration(0f);
            gamepads[_index].dp_left.SetInactiveDuration(Time.deltaTime);
        }
        #endregion
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// updates the statuses of the left/right analog sticks (including l3/r3)
    /// </summary>
    /// <param name="_previous">last frame GamePad Input</param>
    /// <param name="_current">current frame GamePad Input</param>
    ////////////////////////////////////////////////////////////////////////////////////////////////
    void UpdateSticks(int _index, GamePadState _previous, GamePadState _current)
    {
        //analog sticks !REMEMBER that angles are funky:
        //(UP is 90° RIGHT = 0°, DOWN = -90°, and LEFT = 180°)
        #region LEFT ANALOG STICK

        //store these values from the stick no matter what
        gamepads[_index].ls.SetXYValue(new Vector3(_current.ThumbSticks.Left.X, _current.ThumbSticks.Left.Y));
        gamepads[_index].ls.SetXYRawValue(new Vector3(Mathf.Ceil(_current.ThumbSticks.Left.X), Mathf.Ceil(_current.ThumbSticks.Left.Y)));
        gamepads[_index].ls.SetAngle(Mathf.Ceil(Mathf.Atan2(-_current.ThumbSticks.Left.Y, _current.ThumbSticks.Left.X) * Mathf.Rad2Deg));
        gamepads[_index].ls.SetArcadeAxis(DetermineArcadeAxis(gamepads[_index].ls.Status, gamepads[_index].ls.Angle));

        //check to see if the value of x and y is outside tolerance deadzone
        if (_current.ThumbSticks.Left.X < -analogStickDeadZone ||
            _current.ThumbSticks.Left.X > analogStickDeadZone ||
            _current.ThumbSticks.Left.Y < -analogStickDeadZone ||
            _current.ThumbSticks.Left.Y > analogStickDeadZone)
        {
            gamepads[_index].ls.SetStatus(InputStatus.HELD);
        }

        //else, we're inside the deadzone, update status
        else if (_current.ThumbSticks.Left.X > -analogStickDeadZone ||
                _current.ThumbSticks.Left.X < analogStickDeadZone ||
                _current.ThumbSticks.Left.Y > -analogStickDeadZone ||
                _current.ThumbSticks.Left.Y < analogStickDeadZone)
        {
            gamepads[_index].ls.SetStatus(InputStatus.INACTIVE);
        }

        //RELEASED
        if (_previous.Buttons.LeftStick == ButtonState.Pressed && _current.Buttons.LeftStick == ButtonState.Released)
        {
            gamepads[_index].l3.SetStatus(InputStatus.RELEASED);
            gamepads[_index].l3.SetXYValue(0f, 0f);
            gamepads[_index].l3.SetInactiveDuration(Time.deltaTime);
        }
        //HELD
        else if (_previous.Buttons.LeftStick == ButtonState.Pressed && _current.Buttons.LeftStick == ButtonState.Pressed)
        {
            gamepads[_index].l3.SetStatus(InputStatus.HELD);
            gamepads[_index].l3.SetXYValue(1f, 1f);
            gamepads[_index].l3.SetHeldDuration(Time.deltaTime);
            gamepads[_index].l3.SetInactiveDuration(0f);
        }
        //PRESSED
        else if (_previous.Buttons.LeftStick == ButtonState.Released && _current.Buttons.LeftStick == ButtonState.Pressed)
        {
            gamepads[_index].l3.SetStatus(InputStatus.PRESSED);
            gamepads[_index].l3.SetXYValue(1f, 1f);
            gamepads[_index].l3.SetHeldDuration(Time.deltaTime);

            UpdateCombo(_index, gamepads[_index].l3);
        }
        //INACTIVE
        else
        {
            gamepads[_index].l3.SetStatus(InputStatus.INACTIVE);
            gamepads[_index].l3.SetXYValue(0f, 0f);
            gamepads[_index].l3.SetHeldDuration(0f);
            gamepads[_index].l3.SetInactiveDuration(Time.deltaTime);
        }
        #endregion
        #region RIGHT ANALOG STICK

        //store these values from the stick no matter what
        gamepads[_index].rs.SetXYValue(new Vector3(_current.ThumbSticks.Right.X, _current.ThumbSticks.Right.Y));
        gamepads[_index].rs.SetXYRawValue(new Vector3(Mathf.Ceil(_current.ThumbSticks.Right.X), Mathf.Ceil(_current.ThumbSticks.Right.Y)));
        gamepads[_index].rs.SetAngle(Mathf.Ceil(Mathf.Atan2(-_current.ThumbSticks.Right.Y, _current.ThumbSticks.Right.X) * Mathf.Rad2Deg));
        gamepads[_index].rs.SetArcadeAxis(DetermineArcadeAxis(gamepads[_index].rs.Status, gamepads[_index].rs.Angle));

        //check to see if the value of x and y is inside tolerance deadzone
        if (_current.ThumbSticks.Right.X < -analogStickDeadZone ||
            _current.ThumbSticks.Right.X > analogStickDeadZone ||
            _current.ThumbSticks.Right.Y < -analogStickDeadZone ||
            _current.ThumbSticks.Right.Y > analogStickDeadZone)
        {
            gamepads[_index].rs.SetStatus(InputStatus.HELD);
        }

        //else, we're outside the deadzone, update status
        else if (_current.ThumbSticks.Right.X > -analogStickDeadZone ||
                _current.ThumbSticks.Right.X < analogStickDeadZone ||
                _current.ThumbSticks.Right.Y > -analogStickDeadZone ||
                _current.ThumbSticks.Right.Y < analogStickDeadZone)
        {
            gamepads[_index].rs.SetStatus(InputStatus.INACTIVE);
        }

        //RELEASED
        if (_previous.Buttons.RightStick == ButtonState.Pressed && _current.Buttons.RightStick == ButtonState.Released)
        {
            gamepads[_index].r3.SetStatus(InputStatus.RELEASED);
            gamepads[_index].r3.SetXYValue(0f, 0f);
            gamepads[_index].r3.SetInactiveDuration(Time.deltaTime);
        }
        //HELD
        else if (_previous.Buttons.RightStick == ButtonState.Pressed && _current.Buttons.RightStick == ButtonState.Pressed)
        {
            gamepads[_index].r3.SetStatus(InputStatus.HELD);
            gamepads[_index].r3.SetXYValue(1f, 1f);
            gamepads[_index].r3.SetHeldDuration(Time.deltaTime);
            gamepads[_index].r3.SetInactiveDuration(0f);
        }
        //PRESSED
        else if (_previous.Buttons.RightStick == ButtonState.Released && _current.Buttons.RightStick == ButtonState.Pressed)
        {
            gamepads[_index].r3.SetStatus(InputStatus.PRESSED);
            gamepads[_index].r3.SetXYValue(1f, 1f);
            gamepads[_index].r3.SetHeldDuration(Time.deltaTime);

            UpdateCombo(_index, gamepads[_index].r3);
        }
        //INACTIVE
        else
        {
            gamepads[_index].r3.SetStatus(InputStatus.INACTIVE);
            gamepads[_index].r3.SetXYValue(0f, 0f);
            gamepads[_index].r3.SetHeldDuration(0f);
            gamepads[_index].r3.SetInactiveDuration(Time.deltaTime);
        }
        #endregion
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// updates the statuses of the buttons
    /// </summary>
    /// <param name="_previous">last frame GamePad Input</param>
    /// <param name="_current">current frame GamePad Input</param>
    ////////////////////////////////////////////////////////////////////////////////////////////////
    void UpdateButtons(int _index, GamePadState _previous, GamePadState _current)
    {
        #region Y BUTTON
        //RELEASED
        if (_previous.Buttons.Y == ButtonState.Pressed && _current.Buttons.Y == ButtonState.Released)
        {
            gamepads[_index].y.SetStatus(InputStatus.RELEASED);
            gamepads[_index].y.SetXYValue(0f, 0f);
            gamepads[_index].y.SetInactiveDuration(Time.deltaTime);
        }
        //HELD
        else if (_previous.Buttons.Y == ButtonState.Pressed && _current.Buttons.Y == ButtonState.Pressed)
        {
            gamepads[_index].y.SetStatus(InputStatus.HELD);
            gamepads[_index].y.SetHeldDuration(Time.deltaTime);
            gamepads[_index].y.SetInactiveDuration(0f);
        }
        //PRESSED
        else if (_previous.Buttons.Y == ButtonState.Released && _current.Buttons.Y == ButtonState.Pressed)
        {
            gamepads[_index].y.SetStatus(InputStatus.PRESSED);
            gamepads[_index].y.SetXYValue(1f, 1f);
            gamepads[_index].y.SetHeldDuration(Time.deltaTime);

            UpdateCombo(_index, gamepads[_index].y);
        }
        //INACTIVE
        else
        {
            gamepads[_index].y.SetStatus(InputStatus.INACTIVE);
            gamepads[_index].y.SetXYValue(0f, 0f);
            gamepads[_index].y.SetHeldDuration(0f);
            gamepads[_index].y.SetInactiveDuration(Time.deltaTime);
        }
        #endregion
        #region B BUTTON
        //RELEASED
        if (_previous.Buttons.B == ButtonState.Pressed && _current.Buttons.B == ButtonState.Released)
        {
            gamepads[_index].b.SetStatus(InputStatus.RELEASED);
            gamepads[_index].b.SetXYValue(0f, 0f);
            gamepads[_index].b.SetInactiveDuration(Time.deltaTime);
        }
        //HELD
        else if (_previous.Buttons.B == ButtonState.Pressed && _current.Buttons.B == ButtonState.Pressed)
        {
            gamepads[_index].b.SetStatus(InputStatus.HELD);
            gamepads[_index].b.SetHeldDuration(Time.deltaTime);
            gamepads[_index].b.SetInactiveDuration(0f);
        }
        //PRESSED
        else if (_previous.Buttons.B == ButtonState.Released && _current.Buttons.B == ButtonState.Pressed)
        {
            gamepads[_index].b.SetStatus(InputStatus.PRESSED);
            gamepads[_index].b.SetXYValue(1f, 1f);
            gamepads[_index].b.SetHeldDuration(Time.deltaTime);

            UpdateCombo(_index, gamepads[_index].b);
        }
        //INACTIVE
        else
        {
            gamepads[_index].b.SetStatus(InputStatus.INACTIVE);
            gamepads[_index].b.SetXYValue(0f, 0f);
            gamepads[_index].b.SetHeldDuration(0f);
            gamepads[_index].b.SetInactiveDuration(Time.deltaTime);
        }
        #endregion
        #region A BUTTON
        //RELEASED
        if (_previous.Buttons.A == ButtonState.Pressed && _current.Buttons.A == ButtonState.Released)
        {
            gamepads[_index].a.SetStatus(InputStatus.RELEASED);
            gamepads[_index].a.SetXYValue(0f, 0f);
            gamepads[_index].a.SetInactiveDuration(Time.deltaTime);
        }
        //HELD
        else if (_previous.Buttons.A == ButtonState.Pressed && _current.Buttons.A == ButtonState.Pressed)
        {
            gamepads[_index].a.SetStatus(InputStatus.HELD);
            gamepads[_index].a.SetHeldDuration(Time.deltaTime);
            gamepads[_index].a.SetInactiveDuration(0f);
        }
        //PRESSED
        else if (_previous.Buttons.A == ButtonState.Released && _current.Buttons.A == ButtonState.Pressed)
        {
            gamepads[_index].a.SetStatus(InputStatus.PRESSED);
            gamepads[_index].a.SetXYValue(1f, 1f);
            gamepads[_index].a.SetHeldDuration(Time.deltaTime);

            UpdateCombo(_index, gamepads[_index].a);
        }
        //INACTIVE
        else
        {
            gamepads[_index].a.SetStatus(InputStatus.INACTIVE);
            gamepads[_index].a.SetXYValue(0f, 0f);
            gamepads[_index].a.SetHeldDuration(0f);
            gamepads[_index].a.SetInactiveDuration(Time.deltaTime);
        }
        #endregion
        #region X BUTTON
        //RELEASED
        if (_previous.Buttons.X == ButtonState.Pressed && _current.Buttons.X == ButtonState.Released)
        {
            gamepads[_index].x.SetStatus(InputStatus.RELEASED);
            gamepads[_index].x.SetXYValue(0f, 0f);
            gamepads[_index].x.SetInactiveDuration(Time.deltaTime);
        }
        //HELD
        else if (_previous.Buttons.X == ButtonState.Pressed && _current.Buttons.X == ButtonState.Pressed)
        {
            gamepads[_index].x.SetStatus(InputStatus.HELD);
            gamepads[_index].x.SetHeldDuration(Time.deltaTime);
            gamepads[_index].x.SetInactiveDuration(0f);
        }
        //PRESSED
        else if (_previous.Buttons.X == ButtonState.Released && _current.Buttons.X == ButtonState.Pressed)
        {
            gamepads[_index].x.SetStatus(InputStatus.PRESSED);
            gamepads[_index].x.SetXYValue(1f, 1f);
            gamepads[_index].x.SetHeldDuration(Time.deltaTime);

            UpdateCombo(_index, gamepads[_index].x);
        }
        //INACTIVE
        else
        {
            gamepads[_index].x.SetStatus(InputStatus.INACTIVE);
            gamepads[_index].x.SetXYValue(0f, 0f);
            gamepads[_index].x.SetHeldDuration(0f);
            gamepads[_index].x.SetInactiveDuration(Time.deltaTime);
        }
        #endregion

        #region SELECT BUTTON
        //RELEASED
        if (_previous.Buttons.Back == ButtonState.Pressed && _current.Buttons.Back == ButtonState.Released)
        {
            gamepads[_index].select.SetStatus(InputStatus.RELEASED);
            gamepads[_index].select.SetXYValue(0f, 0f);
            gamepads[_index].select.SetInactiveDuration(Time.deltaTime);
        }
        //HELD
        else if (_previous.Buttons.Back == ButtonState.Pressed && _current.Buttons.Back == ButtonState.Pressed)
        {
            gamepads[_index].select.SetStatus(InputStatus.HELD);
            gamepads[_index].select.SetHeldDuration(Time.deltaTime);
            gamepads[_index].select.SetInactiveDuration(0f);
        }
        //PRESSED
        else if (_previous.Buttons.Back == ButtonState.Released && _current.Buttons.Back == ButtonState.Pressed)
        {
            gamepads[_index].select.SetStatus(InputStatus.PRESSED);
            gamepads[_index].select.SetXYValue(1f, 1f);
            gamepads[_index].select.SetHeldDuration(Time.deltaTime);

            UpdateCombo(_index, gamepads[_index].select);
        }
        //INACTIVE
        else
        {
            gamepads[_index].select.SetStatus(InputStatus.INACTIVE);
            gamepads[_index].select.SetXYValue(0f, 0f);
            gamepads[_index].select.SetHeldDuration(0f);
            gamepads[_index].select.SetInactiveDuration(Time.deltaTime);
        }
        #endregion
        #region START BUTTON
        //RELEASED
        if (_previous.Buttons.Start == ButtonState.Pressed && _current.Buttons.Start == ButtonState.Released)
        {
            gamepads[_index].start.SetStatus(InputStatus.RELEASED);
            gamepads[_index].start.SetXYValue(0f, 0f);
            gamepads[_index].start.SetInactiveDuration(Time.deltaTime);
        }
        //HELD
        else if (_previous.Buttons.Start == ButtonState.Pressed && _current.Buttons.Start == ButtonState.Pressed)
        {
            gamepads[_index].start.SetStatus(InputStatus.HELD);
            gamepads[_index].start.SetHeldDuration(Time.deltaTime);
            gamepads[_index].start.SetInactiveDuration(0f);
        }
        //PRESSED
        else if (_previous.Buttons.Start == ButtonState.Released && _current.Buttons.Start == ButtonState.Pressed)
        {
            gamepads[_index].start.SetStatus(InputStatus.PRESSED);
            gamepads[_index].start.SetXYValue(1f, 1f);
            gamepads[_index].start.SetHeldDuration(Time.deltaTime);

            UpdateCombo(_index, gamepads[_index].start);
        }
        //INACTIVE
        else
        {
            gamepads[_index].start.SetStatus(InputStatus.INACTIVE);
            gamepads[_index].start.SetXYValue(0f, 0f);
            gamepads[_index].start.SetHeldDuration(0f);
            gamepads[_index].start.SetInactiveDuration(Time.deltaTime);
        }
        #endregion
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// updates the statuses of the bumpers
    /// </summary>
    /// <param name="_previous">last frame GamePad Input</param>
    /// <param name="_current">current frame GamePad Input</param>
    ////////////////////////////////////////////////////////////////////////////////////////////////
    void UpdateBumpers(int _index, GamePadState _previous, GamePadState _current)
    {
        #region LB
        //RELEASED
        if (_previous.Buttons.LeftShoulder == ButtonState.Pressed && _current.Buttons.LeftShoulder == ButtonState.Released)
        {
            gamepads[_index].lb.SetStatus(InputStatus.RELEASED);
            gamepads[_index].lb.SetXYValue(0f, 0f);
            gamepads[_index].lb.SetInactiveDuration(Time.deltaTime);
        }
        //HELD
        else if (_previous.Buttons.LeftShoulder == ButtonState.Pressed && _current.Buttons.LeftShoulder == ButtonState.Pressed)
        {
            gamepads[_index].lb.SetStatus(InputStatus.HELD);
            gamepads[_index].lb.SetHeldDuration(Time.deltaTime);
            gamepads[_index].lb.SetInactiveDuration(0f);
        }
        //PRESSED
        else if (_previous.Buttons.LeftShoulder == ButtonState.Released && _current.Buttons.LeftShoulder == ButtonState.Pressed)
        {
            gamepads[_index].lb.SetStatus(InputStatus.PRESSED);
            gamepads[_index].lb.SetXYValue(1f, 1f);
            gamepads[_index].lb.SetHeldDuration(Time.deltaTime);

            UpdateCombo(_index, gamepads[_index].lb);
        }
        //INACTIVE
        else
        {
            gamepads[_index].lb.SetStatus(InputStatus.INACTIVE);
            gamepads[_index].lb.SetXYValue(0f, 0f);
            gamepads[_index].lb.SetHeldDuration(0f);
            gamepads[_index].lb.SetInactiveDuration(Time.deltaTime);
        }
        #endregion
        #region RB
        //RELEASED
        if (_previous.Buttons.RightShoulder == ButtonState.Pressed && _current.Buttons.RightShoulder == ButtonState.Released)
        {
            gamepads[_index].rb.SetStatus(InputStatus.RELEASED);
            gamepads[_index].rb.SetXYValue(0f, 0f);
            gamepads[_index].rb.SetInactiveDuration(Time.deltaTime);
        }
        //HELD
        else if (_previous.Buttons.RightShoulder == ButtonState.Pressed && _current.Buttons.RightShoulder == ButtonState.Pressed)
        {
            gamepads[_index].rb.SetStatus(InputStatus.HELD);
            gamepads[_index].rb.SetHeldDuration(Time.deltaTime);
            gamepads[_index].rb.SetInactiveDuration(0f);
        }
        //PRESSED
        else if (_previous.Buttons.RightShoulder == ButtonState.Released && _current.Buttons.RightShoulder == ButtonState.Pressed)
        {
            gamepads[_index].rb.SetStatus(InputStatus.PRESSED);
            gamepads[_index].rb.SetXYValue(1f, 1f);
            gamepads[_index].rb.SetHeldDuration(Time.deltaTime);

            UpdateCombo(_index, gamepads[_index].rb);
        }
        //INACTIVE
        else
        {
            gamepads[_index].rb.SetStatus(InputStatus.INACTIVE);
            gamepads[_index].rb.SetXYValue(0f, 0f);
            gamepads[_index].rb.SetHeldDuration(0f);
            gamepads[_index].rb.SetInactiveDuration(Time.deltaTime);
        }
        #endregion
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// updates the statuses of the triggers
    /// </summary>
    /// <param name="_previous">last frame GamePad Input</param>
    /// <param name="_current">current frame GamePad Input</param>
    ////////////////////////////////////////////////////////////////////////////////////////////////
    void UpdateTriggers(int _index, GamePadState _previous, GamePadState _current)
    {
        #region LT
        //RELEASED
        if (gamepads[_index].lt.Status == InputStatus.PRESSED && _current.Triggers.Left < triggerDeadZone)
        {
            gamepads[_index].lt.SetStatus(InputStatus.RELEASED);
            gamepads[_index].lt.SetXYValue(_current.Triggers.Left, _current.Triggers.Left);
            gamepads[_index].lt.SetXYRawValue(Mathf.Ceil(_current.Triggers.Left), Mathf.Ceil(_current.Triggers.Left));
            gamepads[_index].lt.SetInactiveDuration(Time.deltaTime);
        }
        //HELD
        else if (gamepads[_index].lt.Status == InputStatus.PRESSED || gamepads[_index].lt.Status == InputStatus.HELD && _current.Triggers.Left > triggerDeadZone)
        {
            gamepads[_index].lt.SetStatus(InputStatus.HELD);
            gamepads[_index].lt.SetXYValue(_current.Triggers.Left, _current.Triggers.Left);
            gamepads[_index].lt.SetXYRawValue(Mathf.Ceil(_current.Triggers.Left), Mathf.Ceil(_current.Triggers.Left));
            gamepads[_index].lt.SetHeldDuration(Time.deltaTime);
            gamepads[_index].lt.SetInactiveDuration(0f);

        }
        //PRESSED
        else if (gamepads[_index].lt.Status == InputStatus.INACTIVE && _current.Triggers.Left > triggerDeadZone)
        {
            gamepads[_index].lt.SetStatus(InputStatus.PRESSED);
            gamepads[_index].lt.SetXYValue(_current.Triggers.Left, _current.Triggers.Left);
            gamepads[_index].lt.SetXYRawValue(Mathf.Ceil(_current.Triggers.Left), Mathf.Ceil(_current.Triggers.Left));
            gamepads[_index].lt.SetHeldDuration(Time.deltaTime);

            UpdateCombo(_index, gamepads[_index].lt);
        }
        //INACTIVE
        else
        {
            gamepads[_index].lt.SetStatus(InputStatus.INACTIVE);
            gamepads[_index].lt.SetXYValue(_current.Triggers.Left, _current.Triggers.Left);
            gamepads[_index].lt.SetXYRawValue(Mathf.Ceil(_current.Triggers.Left), Mathf.Ceil(_current.Triggers.Left));
            gamepads[_index].lt.SetHeldDuration(0f);
            gamepads[_index].lt.SetInactiveDuration(Time.deltaTime);
        }
        #endregion
        #region RT
        //RELEASED
        if (gamepads[_index].rt.Status == InputStatus.PRESSED && _current.Triggers.Right < triggerDeadZone)
        {
            gamepads[_index].rt.SetStatus(InputStatus.RELEASED);
            gamepads[_index].rt.SetXYValue(_current.Triggers.Right, _current.Triggers.Right);
            gamepads[_index].rt.SetXYRawValue(Mathf.Ceil(_current.Triggers.Right), Mathf.Ceil(_current.Triggers.Right));
            gamepads[_index].rt.SetInactiveDuration(Time.deltaTime);
        }
        //HELD
        else if (gamepads[_index].rt.Status == InputStatus.PRESSED || gamepads[_index].rt.Status == InputStatus.HELD && _current.Triggers.Right > triggerDeadZone)
        {
            gamepads[_index].rt.SetStatus(InputStatus.HELD);
            gamepads[_index].rt.SetXYValue(_current.Triggers.Right, _current.Triggers.Right);
            gamepads[_index].rt.SetXYRawValue(Mathf.Ceil(_current.Triggers.Right), Mathf.Ceil(_current.Triggers.Right));
            gamepads[_index].rt.SetHeldDuration(Time.deltaTime);
            gamepads[_index].rt.SetInactiveDuration(0f);

        }
        //PRESSED
        else if (gamepads[_index].rt.Status == InputStatus.INACTIVE && _current.Triggers.Right > triggerDeadZone)
        {
            gamepads[_index].rt.SetStatus(InputStatus.PRESSED);
            gamepads[_index].rt.SetXYValue(_current.Triggers.Right, _current.Triggers.Right);
            gamepads[_index].rt.SetXYRawValue(Mathf.Ceil(_current.Triggers.Right), Mathf.Ceil(_current.Triggers.Right));
            gamepads[_index].rt.SetHeldDuration(Time.deltaTime);

            UpdateCombo(_index, gamepads[_index].rt);
        }
        //INACTIVE
        else
        {
            gamepads[_index].rt.SetStatus(InputStatus.INACTIVE);
            gamepads[_index].rt.SetXYValue(_current.Triggers.Right, _current.Triggers.Right);
            gamepads[_index].rt.SetXYRawValue(Mathf.Ceil(_current.Triggers.Right), Mathf.Ceil(_current.Triggers.Right));
            gamepads[_index].rt.SetHeldDuration(0f);
            gamepads[_index].rt.SetInactiveDuration(Time.deltaTime);
        }
        #endregion
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// tracks the last 'x' buttons pressed from the current player's gamepad
    /// </summary>
    /// <param name="_index">current player index</param>
    /// <param name="_button">last button pressed</param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void UpdateCombo(int _index, InputData _button)
    {
        //add in the new button
        gamepads[_index].comboTracker.Enqueue(_button);

        //dequeue the oldest button if we're at max rememberence
        if (gamepads[_index].comboTracker.Count > maxButtonsRemembered)
        {
            gamepads[_index].comboTracker.Dequeue();
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// ArcadeAxis and the angle of the stick are used to create a retro joystick feel
    /// </summary>
    /// <param name="_status">is the stick currently being used</param>
    /// <param name="_angle">angle of the stick</param>
    /// <returns></returns>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    ArcadeAxis DetermineArcadeAxis(InputStatus _status, float _angle)
    {
        //is the stick active?
        if (_status == InputStatus.INACTIVE)
            return ArcadeAxis.INACTIVE;

        //if so, continue below
        //up
        else if (_angle == up || (_angle > (up + -axisLimit) && _angle < (up + axisLimit)))
            return ArcadeAxis.UP;
        //right
        else if (_angle == right || (_angle > (right + -axisLimit) && _angle < (right + axisLimit)))
            return ArcadeAxis.RIGHT;
        //up_right
        else if (_angle == up_right || (_angle > (up_right + -axisLimit) && _angle < (up_right + axisLimit)))
            return ArcadeAxis.UP_RIGHT;
        //down
        else if (_angle == down || (_angle > (down + -axisLimit) && _angle < (down + axisLimit)))
            return ArcadeAxis.DOWN;
        //down_right
        else if (_angle == down_right || (_angle > (down_right + -axisLimit) && _angle < (down_right + axisLimit)))
            return ArcadeAxis.DOWN_RIGHT;
        //left
        else if (_angle == left || (_angle > (left + -axisLimit) && _angle < (left + axisLimit)))
            return ArcadeAxis.LEFT;
        //up_left
        else if (_angle == up_left || (_angle > (up_left + -axisLimit) && _angle < (up_left + axisLimit)))
            return ArcadeAxis.UP_LEFT;
        //down_left
        else if (_angle == down_left || (_angle > (down_left + -axisLimit) && _angle < (down_left + axisLimit)))
            return ArcadeAxis.DOWN_LEFT;
        //default case
        else
            return ArcadeAxis.INACTIVE;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// creates the game event for each gamepad connected (1 – 4 gamepads connected)
    /// </summary>
    /// <param name="_index">gamepad/player index (0 = p1, 1 = p2, etc.)</param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Broadcast(int _index)
    {
        switch (_index)
        {
            case 0:
                Events.instance.Raise(new EVENT_GAMEPAD_P1(gamepads[_index]));
                break;
            case 1:
                Events.instance.Raise(new EVENT_GAMEPAD_P2(gamepads[_index]));
                break;
            case 2:
                Events.instance.Raise(new EVENT_GAMEPAD_P3(gamepads[_index]));
                break;
            case 3:
                Events.instance.Raise(new EVENT_GAMEPAD_P4(gamepads[_index]));
                break;
            default:
                Debug.LogWarning("INCORRECT PLAYER INDEX DETECTED(PLAYER " + _index+" ?)!");
                break;
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
    }
    #endregion

}