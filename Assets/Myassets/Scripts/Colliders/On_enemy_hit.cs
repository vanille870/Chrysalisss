using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class On_enemy_hit : MonoBehaviour
{

    private Collider enemyCollider;
    public ParticleSystem enemyHiteffect;
    private GameObject enemyGameObject;

    // Start is called before the first frame update
    void Awake()
    {
        enemyCollider = this.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider thisCollider)
    {
        
        print(thisCollider);

        if (thisCollider.tag == "Enemy")
        {
            enemyGameObject = thisCollider.gameObject;
            enemyHiteffect = enemyGameObject.GetComponentInParent<ParticleSystem>();
            enemyHiteffect.Play();
            thisCollider.GetComponent<Enemy_hit_effects>().FlashStart();
        }
    }
}
