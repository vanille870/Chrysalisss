using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemPool
{
    public List<GameObject> particleSysList = new List<GameObject>();
    public int test = 69;

    // Start is called before the first frame update
    public void Start(GameObject GO)
    {
        foreach (Transform childGO in GO.transform)
        {
            particleSysList.Add(childGO.gameObject);
        }
    }

    public void GetParticleSysInList(Vector3 particleSysPos, GameObject GO)
    {
        foreach (GameObject ThisGo in particleSysList)
        {
            if (ThisGo.activeSelf == false)
            {
                ThisGo.SetActive(true);
                ThisGo.transform.position = particleSysPos;
                GO.GetComponent<GrassSpawner>().grassBladesPS = ThisGo.GetComponent<ParticleSystem>();
                break;
            }
        }
    }
}
