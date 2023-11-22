using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemPoolManager : MonoBehaviour
{
    public List<GameObject> particleSysList = new List<GameObject>();
    public GameObject poolParent;

    // Start is called before the first frame update
    void SetAllInactive()
    {

    }

    // Update is called once per frame
    public void GetParticleSys(Vector3 particleSysPos, GameObject GO)
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

   public void Start()
    {
        foreach (Transform childGO in poolParent.transform)
        {
            particleSysList.Add(childGO.gameObject);
        }
    }
}
