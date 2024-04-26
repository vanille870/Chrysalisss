using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SlimeAudio", menuName = "ScriptableObjects/Audio/Enemy/Slime", order = 1)]
public class Enemy_Audio : ScriptableObject
{
    [System.Serializable]
    public class SlimeAttackSounds
    {
        public string slimeAttack_DEBUG;
        [SerializeField] public int ID;
        public AudioClip[] audioClips;

        public AudioClip GetClip()
        {
            return audioClips[Random.Range(0, audioClips.Length)];
        }
    }

    [System.Serializable]
    public class SlimeSounds
    {
        public SlimeAttackSounds[] attackSounds;
    }

    [SerializeField] SlimeSounds slimeSounds;
    [SerializeField] AudioClip[] hitSounds;
    [SerializeField] AudioClip[] deadNoiseSounds;
    [SerializeField] AudioClip[] deadSlashSounds;


    public AudioClip GetAttackClip(int ComparedINT)
    {
        foreach (SlimeAttackSounds slimAttackSounds in slimeSounds.attackSounds)
        {
            if (slimAttackSounds.ID == ComparedINT)
            {
                return slimAttackSounds.GetClip();
            }
        }

        Debug.LogWarning("Warning, No audoclip for NormalSword found");
        return null;
    }

    public AudioClip GetHitCLip()
    {
        return hitSounds[Random.Range(0, hitSounds.Length)];
    }

    public AudioClip GetDeathNoiseClip()
    {
        return deadNoiseSounds[Random.Range(0, deadNoiseSounds.Length)];
    }

    public AudioClip GetDeathSlashClip()
    {
        return deadSlashSounds[Random.Range(0, deadSlashSounds.Length)];
    }
}
