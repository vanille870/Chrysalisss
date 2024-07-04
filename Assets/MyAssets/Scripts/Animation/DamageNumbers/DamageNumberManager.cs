using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class DamageNumberManager : MonoBehaviour
{
    public StaticReferencesStorage StaticReferencesStorage;

    [SerializeField] GameObject DamageNumberPrefab;
    GameObject PoolParent;

    quaternion numberRotation;

    public void Awake()
    {
        numberRotation = quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z);
        PoolParent = StaticReferencesStorage.DamageNumbrPool;
    }

    // Update is called once per frame
    public void InstantiateDamageNumber(int damageAmount, Vector3 positionOfEntity, EntityType entity, bool hasBurst, bool fadesOut, bool shrinks, bool Rises)
    {
        GameObject gameObject;

        foreach (Transform currentTrans in PoolParent.transform)
        {
            gameObject = currentTrans.gameObject;
            print("hii");

            if (gameObject.activeSelf == false)
            {
                gameObject.SetActive(true);
                gameObject.transform.position = positionOfEntity;
                gameObject.GetComponent<DamageNumberAnimation>().InitDamageNumber(damageAmount, entity, hasBurst, fadesOut, shrinks, Rises);
                return;
            }
        }

        Debug.LogWarning("No damage numbers GO's left!!!");
        

        /*GameObject gameObject = Instantiate(DamageNumberPrefab, positionOfEntity, numberRotation);
        gameObject.GetComponent<DamageNumberAnimation>().InitDamageNumber(damageAmount, hasBurst, fadesOut, shrinks, Rises);*/
    }
}
