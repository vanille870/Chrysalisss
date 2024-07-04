using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomGameLoop : MonoBehaviour
{
    public delegate void UpdateLoopFunction();
    public static event UpdateLoopFunction UpdateLoopFunctionsSubscriber;

    public delegate void LateUpdateLoopFunction();
    public static event LateUpdateLoopFunction LateupdateLoopFunctionsSubscriber;

    public delegate void FixedUpdateLoopFunction();
    public static event FixedUpdateLoopFunction FixedUpdateLoopFunctionSubsciber;

    // Update is called once per frame
    void Update()
    {
        UpdateLoopFunctionsSubscriber();
    }

    void LateUpdate()
    {
        LateupdateLoopFunctionsSubscriber();
    }

    void FixedUpdate()
    {
        FixedUpdateLoopFunctionSubsciber();
    }

    public void DisableLoop()
    {
        enabled = false;
    }

    public void EnableLoop()
    {
        enabled = true;
    }

    public void Start()
    {
        UpdateLoopFunctionsSubscriber += test;
        LateupdateLoopFunctionsSubscriber += test;
        FixedUpdateLoopFunctionSubsciber += test;
    }

    public void test()
    {

    }
}
