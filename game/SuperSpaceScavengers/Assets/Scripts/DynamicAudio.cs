using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMOD;
using FMODUnity;

public class DynamicAudio : MonoBehaviour {

    FMOD.Studio.EventInstance Music;
    FMOD.Studio.ParameterInstance DRUMHEALTHTEST;

    Player bd;

    void awake()
    {
        Music = FMOD.Unity.RuntimeManager.CreateInstance("event:/Music");
        Music.getparameter("DRUMHEALTHTEST", out DRUMHEALTHTEST);

        bd = GetComponent<health>();
    }

    // Use this for initialization
    void Start()
    {

        FMOD.Unity.RuntimeManager.AttachInstanceToGameObject(Music, GetComponent<Transform>(), GetComponent<Rigidbody>());
        Music.Start();

    }

    // Update is called once per frame
    void Update()
    {
        DRUMHEALTHTEST.setValue(bd);

    }
}

