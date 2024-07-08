using Unity.Mathematics;
using UnityEngine;

public class HealthNumberManager : MonoBehaviour
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
    public void InstantiateHealthNumber(int amount, Vector3 positionOfEntity, TypeOfHealthNumber typeOfHealthNumber)
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
                gameObject.GetComponent<HealthNumberAnimation>().InitNumber(amount, typeOfHealthNumber);
                return;
            }
        }

        Debug.LogWarning("No damage numbers GO's left!!!");


        /*GameObject gameObject = Instantiate(DamageNumberPrefab, positionOfEntity, numberRotation);
        gameObject.GetComponent<DamageNumberAnimation>().InitDamageNumber(damageAmount, hasBurst, fadesOut, shrinks, Rises);*/
    }
}
