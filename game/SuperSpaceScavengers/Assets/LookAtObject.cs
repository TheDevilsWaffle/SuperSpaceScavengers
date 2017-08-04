using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//[ExecuteInEditMode]
public class LookAtObject : MonoBehaviour
{
    [HideInInspector]
    public new Transform transform;

    public Transform lookTarget;

    public Transform positionObject;
    public Vector3 positionOffset;

    public Vector3 restingPosition;

    public Vector3 startPosition;

    //LookAtObject()
    //{
    //    EditorApplication.update += Update;
    //}

    //~LookAtObject()
    //{
    //    EditorApplication.update -= Update;
    //}

    void Start()
    {
        transform = base.transform;
        transform.localPosition = restingPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (positionObject != null)
            transform.position = positionObject.TransformPoint(positionOffset);
        //else
        //    transform.localPosition = Vector3.up * -0.3f;

        transform.LookAt(lookTarget, lookTarget.up);
    }

    //void Destroy()
    //{
    //    transform.localPosition = new Vector3(-0.1f, -0.35f, 0);
    //}
}
