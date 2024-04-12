using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAudio", menuName = "ScriptableObjects/Audio/Player", order = 1)]
public class PlayerAudioStorage : ScriptableObject
{
    [System.Serializable]
    public class FootStepSounds
    {
        public Texture albedo;
        public AudioClip[] audioClips;

        public AudioClip GetClip()
        {
            return audioClips[Random.Range(0, audioClips.Length)];
        }
    }

    [System.Serializable]
    public class SwordAttackSounds
    {
        public string SwordAttack_DEBUG;
        [SerializeField] public int ID;
        public AudioClip[] audioClips;

        public AudioClip GetClip()
        {
            return audioClips[Random.Range(0, audioClips.Length)];
        }
    }

    [System.Serializable]
    public class SwordSounds
    {
        public SwordAttackSounds[] normalSwordAttackSounds;
    }

    [SerializeField] public FootStepSounds[] footStepSoundsClassArray;
    [SerializeField] public SwordSounds swordSounds;

    public AudioClip GetFootstepSoundClip(Texture ComparedTexture)
    {
        foreach (FootStepSounds ftsClass in footStepSoundsClassArray)
        {
            if (ftsClass.albedo == ComparedTexture)
            {
                return ftsClass.GetClip();
            }
        }

        Debug.LogWarning("Warning, No audoclip for footstep found");
        return null;
    }

    
    public AudioClip GetNormalSwordSoundClip(int ComparedINT)
    {
        foreach (SwordAttackSounds swordSoundsClass in swordSounds.normalSwordAttackSounds)
        {
           if (swordSoundsClass.ID == ComparedINT)
           {
             return  swordSoundsClass.GetClip();
           }
        }

        Debug.LogWarning("Warning, No audoclip for NormalSword found");
        return null;
    }
}
