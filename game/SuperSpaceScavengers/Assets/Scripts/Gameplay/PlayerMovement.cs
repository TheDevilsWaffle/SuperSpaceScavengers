using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerMovement : Movement
{
    private Player player;

    public GameObject createOnLand = null;
    private float creationTimer = 0;

    void Start()
    {
        player = GetComponent<Player>();

        animationBaseSpeeds = new float[runAnimations.Length];
        for (int i = 0; i < runAnimations.Length; i++)
            animationBaseSpeeds[i] = runAnimations[i].speed;

        InputEvents.Movement.Subscribe(OnMove, player.index);
        //InputEvents.LeftBasic.Subscribe(OnAim);
    }

    private void OnAim(InputEventInfo _inputEventInfo)
    {
        if (_inputEventInfo.inputState == InputState.Triggered)
            faceVelocity = false;
        else if (_inputEventInfo.inputState == InputState.Released)
            faceVelocity = true;
    }

    private void OnMove(InputEventInfo _inputEventInfo)
    {
        if (_inputEventInfo.inputState == InputState.Triggered)
        {
            foreach (Animate animate in runAnimations)
                animate.Play();

            creationTimer = runAnimations[0].defaultStopRatio / animationBaseSpeeds[0];
        }
        else if (_inputEventInfo.inputState == InputState.Active)
        {
            creationTimer += Time.deltaTime;
            if (creationTimer > 0.5f / animationBaseSpeeds[0])
            {
                Instantiate(createOnLand, transform.position, transform.rotation).transform.localScale *= 0.45f;
                creationTimer -= 0.5f / animationBaseSpeeds[0];
            }

            for (int i = 0; i < runAnimations.Length; i++)
                runAnimations[i].speed = animationBaseSpeeds[i] * Mathf.Max(0.5f, inputMagLastFrame);

            Vector3 _projectedForward = Vector3.ProjectOnPlane(relativeTransform.forward, Vector3.up);
            float _verticalAngle = Vector3.Angle(_projectedForward, relativeTransform.forward);

            if (_verticalAngle > 45)
                targetDirection = new Vector3(_inputEventInfo.dualAxisValue.x, _inputEventInfo.dualAxisValue.y, 0);
            else //if (_verticalAngle > -45)
                targetDirection = new Vector3(_inputEventInfo.dualAxisValue.x, 0, _inputEventInfo.dualAxisValue.y);
            //else
            //    targetDirection = new Vector3(_inputEventInfo.dualAxisValue.x, -_inputEventInfo.dualAxisValue.y, 0);

            player.sprite.flipX = targetDirection.x < 0;

            if (targetDirection.x != 0)
                player.sprite.transform.GetChild(0).localScale = new Vector3(targetDirection.x < 0 ? -1 : 1, 0.9359878f, 1);

            transform.GetChild(0).GetChild(0).localEulerAngles = new Vector3(0, 0, targetDirection.x * -15);
            //player.sprite.transform.GetChild(0).GetChild(0).LookAt(player.sprite.transform.GetChild(0).GetChild(0).TransformPoint(targetDirection));

            if (targetDirection.z <= 0)
                player.sprite.transform.GetChild(0).localEulerAngles = new Vector3(-35, 180, 0);
            else if (targetDirection.z > 0)
                player.sprite.transform.GetChild(0).localEulerAngles = new Vector3(35, 0, 0);
        }
        else
        {
            foreach (Animate animate in runAnimations)
                animate.Stop(Animate.StopType.GoToDefault);

            targetDirection = Vector3.zero;
            transform.GetChild(0).GetChild(0).localEulerAngles = new Vector3(0, 0, 0);
        }
    }

    public static float SignedAngle(Vector3 vec1, Vector3 vec2)
    {
        float angle = Vector3.Angle(vec1, vec2);
        Vector3 cross = Vector3.Cross(vec1, vec2);
        if (cross.z < 0) angle = -angle;

        return angle;
    }

    void Update()
    {
        //Debug.Log(GamePadInput.GetInputActive(0, GamePadInput.Button.DpadLeft));
    }

    void OnDestroy()
    {
        InputEvents.Movement.Unsubscribe(OnMove, 0);
        //InputEvents.Jump.Unsubscribe(OnJump, 0);
    }
}
