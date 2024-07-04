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
        if (mainCamera != null)
        {
            mainCamera = Camera.main;
            transform.LookAt(new Vector3(mainCamera.transform.position.x, 0, mainCamera.transform.position.y));
        }


    }
}
