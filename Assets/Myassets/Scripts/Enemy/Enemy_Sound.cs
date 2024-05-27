using UnityEngine;
using UnityEngine.AI;

public class Enemy_Sound : MonoBehaviour
{

    public enum AttacksEnum { Swipe = 0, Punch };

    [SerializeField] Enemy_Audio enemy_AudioSO;
    [SerializeField] AudioSource EnemyAudioSourceMain;
    [SerializeField] AudioSource EnemyAudioSourceEffects;


    [Range(0, 1)]
    [SerializeField]
    float speedPercent;

    float CurrentMagnitude;

    [SerializeField] NavMeshAgent EnemyAgent;
    [SerializeField] AudioClip AlertSound;

    void OnEnable()
    {
        CustomGameLoop.LateupdateLoopFunctionsSubscriber += UpdateMovingSounds;
    }
    
    void OnDisable()
    {
        CustomGameLoop.LateupdateLoopFunctionsSubscriber -= UpdateMovingSounds;
    }

    // Update is called once per frame
    void UpdateMovingSounds()
    {
        MovingSounds();
    }

    void MovingSounds()
    {
        CurrentMagnitude = EnemyAgent.velocity.magnitude;
        speedPercent = CurrentMagnitude / 0.7f;
        EnemyAudioSourceMain.volume = speedPercent;
    }

    public void PlayAlertSound()
    {
        EnemyAudioSourceEffects.PlayOneShot(AlertSound);
    }

    public void PlayAttackSound(AttacksEnum attackName)
    {
        EnemyAudioSourceEffects.PlayOneShot(enemy_AudioSO.GetAttackClip((int)attackName));
    }

    public void PlayHitSound()
    {
        EnemyAudioSourceEffects.PlayOneShot(enemy_AudioSO.GetHitCLip());
    }

    public void PlayDeathSound()
    {
        EnemyAudioSourceEffects.PlayOneShot(enemy_AudioSO.GetDeathNoiseClip());
    }

    public void PlayDeathSlashSound()
    {
          EnemyAudioSourceEffects.PlayOneShot(enemy_AudioSO.GetDeathSlashClip());
    }
}
