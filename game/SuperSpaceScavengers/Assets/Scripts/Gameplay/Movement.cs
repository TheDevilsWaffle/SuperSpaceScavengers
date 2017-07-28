using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class Movement : MonoBehaviour
{
    #region MonoBehaviour References
    [SerializeField]
    [HideInInspector]
    protected new Transform transform;

    [SerializeField]
    [HideInInspector]
    protected new Rigidbody rigidbody;

    [SerializeField]
    [HideInInspector]
    protected CapsuleCollider capsuleCollider;
    #endregion

    [System.NonSerialized]
    public Vector3 targetDirection = Vector3.zero;
    protected int frameSet = -1;

    public Transform relativeTransform;

    [Header("Speed")]
    public float maxSpeed = 20;
    public float acceleration = 1;
    public float deceleration = 1;
    public AnimationCurve accelerationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    public AnimationCurve decelerationCurve = AnimationCurve.EaseInOut(0, 1, 1, 0);

    protected float inputMagLastFrame = 0;
    protected Vector3 velocityLastFrame = Vector3.zero;
    protected Vector3 positionLastFrame = Vector3.zero;

    [Header("Sound and Animations")]
    public Animate[] runAnimations = new Animate[0];
    protected float[] animationBaseSpeeds = null;

    public float rotationSpeed = 1;
    public bool faceVelocity = true;

    void OnValidate()
    {
        transform = GetComponent<Transform>();
        rigidbody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        capsuleCollider.material.dynamicFriction = 0;
        capsuleCollider.material.staticFriction = 0;
        capsuleCollider.material.frictionCombine = PhysicMaterialCombine.Minimum;

        capsuleCollider.material.bounciness = 0;
        capsuleCollider.material.bounceCombine = PhysicMaterialCombine.Minimum;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < runAnimations.Length; i++)
        {
            if (runAnimations[i].speed > 0)
                runAnimations[i].speed = 1;
            else
                runAnimations[i].speed = -1;

            if (i > 0)
                runAnimations[i].timer = runAnimations[0].timer;
        }

        //if (Input.GetAxis("Jump") > 0.1f && onGround)
        //    rigidbody.velocity = new Vector3(rigidbody.velocity.x, 10, rigidbody.velocity.z);

        //if (Input.GetKeyDown(KeyCode.E))
        //    rigidbody.velocity = new Vector3(10, rigidbody.velocity.y, rigidbody.velocity.z);

        //if (Input.GetKeyDown(KeyCode.Q))
        //    rigidbody.velocity = new Vector3(-10, rigidbody.velocity.y, rigidbody.velocity.z);
    }
    void FixedUpdate()
    {
        UpdateMovement();

        float _speed = rigidbody.velocity.magnitude;

        //if (_speed > 0.1f)
        //{
        //    float _directionDelta = SignedAngle(Vector3.ProjectOnPlane(transform.forward, groundNormal), Vector3.ProjectOnPlane(velocityLastFrame, groundNormal));
        //    if (Mathf.Abs(_directionDelta) < 1)
        //        return;
        //    Debug.Log(_directionDelta);
        //    float _newZRotation = (_speed / maxSpeed) * Mathf.Sign(_directionDelta);
        //    _newZRotation = Mathf.Lerp(transform.GetChild(0).localEulerAngles.z, _newZRotation * 30, Time.fixedDeltaTime);
        //    transform.GetChild(0).localEulerAngles = new Vector3(transform.GetChild(0).localEulerAngles.x, transform.GetChild(0).localEulerAngles.y, _newZRotation);
        //}
    }

    private void UpdateMovement()
    {
        Vector3 _movement = targetDirection; //going to manipulate
        float _inputMagnitude = Mathf.Clamp01(_movement.magnitude); //store the magnitude for use later
        float _inputAngle = Vector3.Angle(Vector3.forward, _movement); //the direction of input

        if (relativeTransform != null)
            _movement = relativeTransform.TransformDirection(_movement); //make movement relative to set transform (e.g. camera)
        //else
        //    _movement = transform.TransformDirection(_movement); //make movement relative to player

        _movement = Vector3.ProjectOnPlane(_movement, Vector3.up); //"flatten" movement onto the ground (we want to move on the ground)
        _movement.Normalize();
        _movement *= maxSpeed * _inputMagnitude; //we are targeting the max speed if inputMag is at 1

        Vector2 _currentNonVertical = new Vector2(rigidbody.velocity.x, rigidbody.velocity.z); //the vector we are currently moving at
        Vector2 _desiredNonVertical = new Vector2(_movement.x, _movement.z); //the vector we would like to move at
        Vector2 _newNonVertical = Vector2.zero; //the vector that will be manipulated for out vector

        float _acceleration = acceleration * 10;
        float _deceleration = deceleration * 10;

        float _currentSpeed = _currentNonVertical.magnitude;
        float _desiredSpeed = _desiredNonVertical.magnitude;

        float _currentSpeedRatio = _currentSpeed / maxSpeed;
        float _desiredSpeedRatio = _desiredSpeed / maxSpeed;

        bool _decelerating = _desiredSpeed < _currentSpeed; //actual deceleration
        bool _attemptingAcceleration = _inputMagnitude > 0.1f && _inputMagnitude >= inputMagLastFrame; //our definition of acceleration
        float _accelerationRatio = (1 - accelerationCurve.Evaluate(_currentSpeedRatio));
        float _decelerationRatio = (1 - decelerationCurve.Evaluate(_currentSpeedRatio));

        //no! check if the new velocity will be bigger than current velocity (velocity + desired non-vertical).mag greater than or less than velocity.mag

        //what we actually want is the difference in input, if we are giving input, that is ACCELERATING!
        //needs special case for switching directions(?), extra boost if have speed already?
        //acceleration ratio should be different if not going from zero speed (changing directions)

        bool _currentlyUnderLimit = _currentSpeedRatio < 1;
        bool _willBeUnderLimit = Mathf.MoveTowards(_currentSpeed, _desiredSpeed, _acceleration * Time.fixedDeltaTime) < maxSpeed; //if accelerate

        if (_attemptingAcceleration) //accelerating
        {
            if (!_willBeUnderLimit)
                _newNonVertical = _newNonVertical.normalized * maxSpeed;

            if (_decelerating) //if we are still decelerating, we are actually changing directions
                _newNonVertical = Vector2.MoveTowards(_currentNonVertical, _desiredNonVertical, _acceleration * _accelerationRatio * Time.fixedDeltaTime);
            else
                _newNonVertical = Vector2.MoveTowards(_currentNonVertical, _desiredNonVertical, _acceleration * Time.fixedDeltaTime);

            //Debug.Log("Acceleration " + (_acceleration * _accelerationRatio));
        }
        else //decelerating
        {
            //Debug.Log("Deceleration " + (_deceleration * _decelerationRatio));
            _newNonVertical = Vector2.MoveTowards(_currentNonVertical, _desiredNonVertical, _deceleration * _decelerationRatio * Time.fixedDeltaTime);
        }

        //if (_desiredNonVertical.sqrMagnitude > _oldNonVertical.sqrMagnitude)
        //    _newNonVertical = FixedLerp(_oldNonVertical, _desiredNonVertical, _acceleration * 4 * Time.fixedDeltaTime);
        //else
        //    _newNonVertical = FixedLerp(_oldNonVertical, _desiredNonVertical, _decceleration * 4 * Time.fixedDeltaTime);

        //Debug.Log(_newNonVertical);
        //float _speedDelta = _currentSpeed - velocityLastFrame.magnitude;

        //Transform _childTransform = transform.GetChild(0).transform;
        //_childTransform.localScale = new Vector3(Mathf.Lerp(1, 0.35f, _speedDelta), _childTransform.localScale.y, Mathf.Lerp(1, 1.35f, _speedDelta));

        inputMagLastFrame = _inputMagnitude;

        velocityLastFrame = rigidbody.velocity;
        positionLastFrame = transform.position;
        rigidbody.velocity = new Vector3(_newNonVertical.x, rigidbody.velocity.y, _newNonVertical.y);

        //rotation
        if (faceVelocity == false)
            return;

        //Vector3 _newForward = Vector3.Lerp(transform.forward, new Vector3(_newNonVertical.x, 0, _newNonVertical.y), Time.fixedDeltaTime * rotationSpeed);
        Vector3 _newForward = Vector3.Lerp(transform.forward, targetDirection, Time.fixedDeltaTime * rotationSpeed);
        transform.LookAt(transform.position + _newForward);
    }
}
