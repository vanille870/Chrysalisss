using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class TestHashes : MonoBehaviour
{
    public string one;
    public Hash128 oneINT;
    public string two;
    public Hash128 twoINT;

    // Start is called before the first frame update
    void Start()
    {
        oneINT = Hash128.Compute(one);
        twoINT = Hash128.Compute(two);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
