using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class On_enemy_hit : MonoBehaviour
{
    public ParticleSystem enemyHiteffect;
    private GameObject enemyGameObject;

    public int damage;
    public int staggrDamage;

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayEnemyHitEffect()
    {

    }

    void OnTriggerEnter(Collider thisCollider)
    {  

        if (thisCollider.tag == "Enemy")
        {
            enemyGameObject = thisCollider.gameObject;
            enemyHiteffect = enemyGameObject.GetComponentInChildren<ParticleSystem>();
            enemyHiteffect.Play();
            thisCollider.GetComponent<Slime_Damaged>().FlashStart();
            
            thisCollider.GetComponent<EnemyHealth>().EnemyRecieveDamage(damage, staggrDamage);
        }
    }

    
}
