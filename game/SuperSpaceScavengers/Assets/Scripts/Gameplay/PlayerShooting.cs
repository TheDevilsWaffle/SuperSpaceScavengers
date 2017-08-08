using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerShooting : Shooting
{
    private Player player;
    private Shooting shooting;

    // Use this for initialization
    private void Start()
    {
        shooting = GetComponentInChildren<Shooting>();

        InputEvents.UseItem.Subscribe(OnShoot);
        //InputEvents.LeftBasic.Subscribe(OnAim);
    }

    private void OnAim(InputEventInfo _inputEventInfo)
    {
        float _rightStickRight = GamePadInput.GetInputValue(0, GamePadInput.Axis.RightStickRight);
        float _rightStickLeft = GamePadInput.GetInputValue(0, GamePadInput.Axis.RightStickLeft);
        float _rightStickUp = GamePadInput.GetInputValue(0, GamePadInput.Axis.RightStickUp);
        float _rightStickDown = GamePadInput.GetInputValue(0, GamePadInput.Axis.RightStickDown);

        Vector3 _rightStickInput = new Vector2(_rightStickRight - _rightStickLeft, _rightStickUp - _rightStickDown);
        transform.LookAt(transform.position + new Vector3(_rightStickInput.x, 0, _rightStickInput.y));
    }

    private void OnShoot(InputEventInfo _inputEventInfo)
    {
        if (_inputEventInfo.inputState == InputState.Triggered)
        {
            //shooting.firing = true;
        }
        if (_inputEventInfo.inputState == InputState.Active)
        {
            shooting.Fire();
        }
        else if (_inputEventInfo.inputState == InputState.Released)
        {
            //shooting.firing = false;
        }
    }

    void OnDestroy()
    {
        InputEvents.UseItem.Unsubscribe(OnShoot);
        //InputEvents.LeftBasic.Unsubscribe(OnAim);
    }
}
