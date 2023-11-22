using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class On_breakable_hit : MonoBehaviour
{

    public Transform center;
    public float radius;
    public Collider[] colliderArray;
    public static bool canBreakObjects;
    public static bool hasBreaked;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (canBreakObjects == true && hasBreaked == false)
        {
            Collider[] colliderArray = Physics.OverlapSphere(center.position, radius);
            hasBreaked = true;

            foreach (Collider col in colliderArray)
            {
                if (col.tag == "Breakable")
                {
                   col.GetComponent<GrassSpawner>().Damage(0f, radius, center.position);
                }
            }
        }
    }
}
