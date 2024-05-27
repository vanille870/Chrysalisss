using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CostumGameLoopPause : MonoBehaviour
{
    public delegate void PauseUpdateLoopFunction();
    public static event PauseUpdateLoopFunction PauseUpdateLoopFunctionsSubscriber;

    
    public delegate void PauseLateUpdateLoopFunction();
    public static event PauseLateUpdateLoopFunction PauseLateUpdateLoopFunctionsSubscriber;

    void Start()
    {
        PauseUpdateLoopFunctionsSubscriber += testTemp;
        PauseLateUpdateLoopFunctionsSubscriber += testTemp;
    }

    // Update is called once per frame
    void Update()
    {
        PauseUpdateLoopFunctionsSubscriber();
    }

    void LateUpdate()
    {
        PauseLateUpdateLoopFunctionsSubscriber();
    }

    public void DisableLoop()
    {
        enabled = false;
    }

    public void EnableLoop()
    {
        enabled = true;
    }


    //avoid errors from empty events
    void testTemp()
    {

    }
}

