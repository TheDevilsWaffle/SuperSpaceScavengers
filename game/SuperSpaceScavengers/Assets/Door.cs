﻿///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — Door.cs
///////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
//using UnityEngine.UI;
using System.Collections;
//using System.Collections.Generic;

#region ENUMS
public enum DoorType
{
    MANUAL, AUTOMATIC,
};
public enum DoorStatus
{
    OPEN, CLOSED, LOCKED, DISABLED
};
public enum PowerStatus
{
    POWERED, UNPOWERED
};
public enum DoorOpenType
{
    UP, RIGHT, DOWN, LEFT
};
#endregion

public class Door : MonoBehaviour
{
    #region FIELDS

    Transform tr;
    Transform door;
    AnimatePosition ap;

    BoxCollider detector;

    [Header("ATTRIBUTES")]
    [SerializeField]
    DoorType type = DoorType.AUTOMATIC;
    public DoorType Type
    {
        get { return type; }
        private set { type = value; }
    }
    [SerializeField]
    DoorStatus status = DoorStatus.CLOSED;
    public DoorStatus Status
    {
        get { return status; }
        private set { status = value; }
    }
    [SerializeField]
    PowerStatus power = PowerStatus.POWERED;
    public PowerStatus Power
    {
        get { return power; }
        private set { power = value; }
    }
    [SerializeField]
    DoorOpenType openType = DoorOpenType.LEFT;
    public DoorOpenType OpenType
    {
        get { return openType; }
        private set { openType = value; }
    }
    [SerializeField]
    float openSize = 1f;
    
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
        tr = GetComponent<Transform>();
        door = tr.GetChild(0);
        ap = door.GetComponent<AnimatePosition>();

        //initial values
        ap.startValue = door.localPosition;
        ap.endValue = SetOpenPosition(openType);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
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
        //Events.instance.AddListener<event>(function);
    }
    #endregion

    #region PUBLIC METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// function
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////

    #endregion

    #region PRIVATE METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// set the correct open position based on DoorOpenType
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    Vector3 SetOpenPosition(DoorOpenType _openType)
    {
        switch (_openType)
        {
            case DoorOpenType.UP:
                return ap.startValue + new Vector3(0f, openSize, 0f);
            case DoorOpenType.RIGHT:
                return ap.startValue + new Vector3(openSize, 0f, 0f);
            case DoorOpenType.DOWN:
                return ap.startValue + new Vector3(0f, -openSize, 0f);
            case DoorOpenType.LEFT:
                return ap.startValue + new Vector3(-openSize, 0f, 0f);
            default:
                Debug.LogWarning("invalid DoorOpenType sent, please check openType!");
                return Vector3.zero;
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// function
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void AutomaticDoorCheck()
    {
        if (Power == PowerStatus.POWERED)
        {
            if (Type == DoorType.AUTOMATIC)
            {
                if (Status == DoorStatus.CLOSED)
                    OpenDoor();
                else if (Status == DoorStatus.OPEN)
                    CloseDoor();
            }
            
        }
    }
    public void InteractWithDoor()
    {
        if(Power == PowerStatus.POWERED)
        {
            if (Type == DoorType.MANUAL)
            {
                if (status == DoorStatus.CLOSED)
                {
                    OpenDoor();

                }
                else if (status == DoorStatus.OPEN)
                {
                    CloseDoor();

                }
            }
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// open the door using Animate Position and set Status to DoorStatus.OPEN
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void OpenDoor()
    {
        ap.Play();
        Status = DoorStatus.OPEN;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// close the door using Animate Position and set Status to DoorStatus.CLOSED
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void CloseDoor()
    {
        ap.Play(false, false);
        Status = DoorStatus.CLOSED;
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
        //remove listeners
        //Events.instance.RemoveListener<>();
    }
    #endregion

}