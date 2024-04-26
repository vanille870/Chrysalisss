
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{

    public GeneralAnimationWeapon generalAnimationWeapon;
    public Animator mainCharAnim;

    [SerializeField] AudioSource PlayerAudioSourceFootsteps;
    [SerializeField] AudioSource PlayerAudioSourceEffcts;
    [SerializeField] CharacterController characterController;


    [SerializeField] LayerMask floorMask;
    [SerializeField] Transform PlayerPoint;
    [SerializeField] PlayerAudioStorage playerAudioStorage;

    public enum NormalSwordAttacksEnum {Diagonal = 0, Sweep, Chop, Stab};


    float[,,] alphaMap;


    void PlayFootstepSound()
    {
        if (characterController.isGrounded == true)
        {
            Physics.Raycast(PlayerPoint.position, Vector3.down, out RaycastHit RayHit, 3, floorMask);

            if (RayHit.collider != null && RayHit.collider.tag == "Terrain")
            {
                Terrain currentTrain = RayHit.collider.gameObject.GetComponent<Terrain>();

                Vector3 positionOnTerrain = RayHit.point - currentTrain.transform.position;
                Vector3 splatMapPosition = new Vector3(positionOnTerrain.x / currentTrain.terrainData.size.x, 0, positionOnTerrain.z / currentTrain.terrainData.size.z);

                int x = Mathf.FloorToInt(splatMapPosition.x * currentTrain.terrainData.alphamapWidth);
                int z = Mathf.FloorToInt(splatMapPosition.z * currentTrain.terrainData.alphamapHeight);

                alphaMap = currentTrain.terrainData.GetAlphamaps(x, z, 1, 1);

                int primaryIndex = 0;

                for (int i = 0; i < alphaMap.Length; i++)
                {
                    if (alphaMap[0, 0, i] > alphaMap[0, 0, primaryIndex])
                    {
                        primaryIndex = i;
                    }
                }

                PlayerAudioSourceFootsteps.PlayOneShot(playerAudioStorage.GetFootstepSoundClip(currentTrain.terrainData.terrainLayers[primaryIndex].diffuseTexture));
            }
        }
    }

    void CanStartNextAttack()
    {
        mainCharAnim.SetBool("CanStartNextAttack", true);
        generalAnimationWeapon.isRessetingSpeed = true;
        //generalAnimationWeapon.speedTimer = 0;
    }

    void CanBreakObjects()
    {
        On_breakable_hit.canBreakObjects = true;
        On_breakable_hit.hasBreaked = false;
    }

    void SwordSwingSounds(AudioClip SwingSound)
    {
        PlayerAudioSourceEffcts.PlayOneShot(SwingSound);
    }

    void PlayNormalSwordSound(NormalSwordAttacksEnum swordAttack)
    {
        PlayerAudioSourceEffcts.PlayOneShot(playerAudioStorage.GetNormalSwordSoundClip((int)swordAttack));
    }

    void PlayDodgeSound()
    {
        PlayerAudioSourceEffcts.PlayOneShot(playerAudioStorage.Dodge[Random.Range(0, playerAudioStorage.Dodge.Length)]);
    }

    void PlayPainGrunt()
    {
        PlayerAudioSourceEffcts.PlayOneShot(playerAudioStorage.PainGrunts[Random.Range(0, playerAudioStorage.PainGrunts.Length)]);
    }



}
