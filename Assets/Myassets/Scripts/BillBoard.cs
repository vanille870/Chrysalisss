using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    Camera mainCamera;

    void OnEnable()
    {
        CustomGameLoop.LateupdateLoopFunctionsSubscriber += MakeBillboard;
    }
    
    void OnDisable()
    {
        CustomGameLoop.LateupdateLoopFunctionsSubscriber -= MakeBillboard;
    }

    void Start()
    {
        mainCamera = Camera.main;
    }

    void MakeBillboard()
    {
        transform.LookAt(mainCamera.transform);
    }
}
