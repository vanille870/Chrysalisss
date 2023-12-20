using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class ParticleSystemPoolManager : MonoBehaviour
{
    public List<GameObject> particleSysList = new List<GameObject>();
    public GameObject poolParent;

    bool foundParticleSys;

    // Start is called before the first frame update
    void SetAllInactive()
    {

    }

    // Update is called once per frame
    public void GetParticleSystemGO(Vector3 particleSysPos, GameObject GO)
    {
        foreach (GameObject ThisGo in particleSysList)
        {
            if (ThisGo.activeSelf == false)
            {
                ThisGo.SetActive(true);
                ThisGo.transform.position = particleSysPos;
                GO.GetComponent<BreakableObjectSpawner>().grassBladesPS = ThisGo.GetComponent<ParticleSystem>();
                GO.GetComponent<BreakableObjectSpawner>().particlSysName = ThisGo.name;
                foundParticleSys = true;
                break;
            }
        }

        if (foundParticleSys == false)
        {
            Debug.LogWarning("No particle system aviable");
        }

        foundParticleSys = false;
    }

    public void ReturnParticleSytem(string ParticleSysName)
    {
        foreach (GameObject ThisGo in particleSysList)
        {
            if (ThisGo.name == ParticleSysName)
            {
                ThisGo.transform.position = Vector3.zero;
                ThisGo.SetActive(false);
                break;
            }
        }
    }

    public void Start()
    {
        //make list at runtime
        foreach (Transform childGO in poolParent.transform)
        {
            particleSysList.Add(childGO.gameObject);
        }
    }
}
