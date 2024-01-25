using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class AfterMiragesPlayer : MonoBehaviour
{
    public Renderer afterImageRenderer;
    public Material playerMaterial;
    public float afterImageOpacity;
    public float playerGlow;
    public float playerGlowOpacity;

    private float lerSpeedOpacityFloat = 1;
    private float lerpSpeedGlowFloat = 1;
    public float fadeSpeedMultiplier;

    public GameObject[] InvisibleGOs;
    public GameObject playerParent;
    bool playerIsInvisible;

    [System.Serializable]
    public struct LerpEvent
    {
        [SerializeField]
        public float DurationMultiplier;
        public float LerpFloat;

        public LerpEvent(float durationmult, float time = 0f)
        {
            DurationMultiplier = durationmult;
            LerpFloat = time;
        }

        public void Lerp()
        {
            LerpFloat += Time.deltaTime * DurationMultiplier;
        }

        public void ResetLerp()
        {
            LerpFloat = 0;
        }

        public bool LerpFinished => 1 <= LerpFloat;
    }

    [SerializeField]
    private LerpEvent LerpAfterImageOpacityEV = new LerpEvent();
    [SerializeField]
    private LerpEvent LerpPlayerGloweOpacityEV = new LerpEvent();
    [SerializeField]
    private LerpEvent LerpPlayerGlowPowerEV = new LerpEvent();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        LerpOpacity();
        LerpPlayerGlow();
        LerpPlayerGlowOpacity();
        //playerRenderer.material.SetFloat("er", 1);
    }

    public void SetAfterImageToPlayerPos(Vector3 playerPos, Vector3 playerRot)
    {
        playerPos.y -= 0.3f;
        transform.position = playerPos;
        transform.rotation = Quaternion.Euler(playerRot.x, playerRot.y, playerRot.z);
        ResetAllLerps();
        TogglePlayerVisibilty();
    }

    public void LerpOpacity()
    {
        if (LerpAfterImageOpacityEV.LerpFinished == false)
        {
            LerpAfterImageOpacityEV.Lerp();
            afterImageOpacity = Mathf.Lerp(1, 0, LerpAfterImageOpacityEV.LerpFloat);
            afterImageRenderer.material.SetFloat("_Opacity", afterImageOpacity);
        }
    }

    public void LerpPlayerGlow()
    {
        if (LerpPlayerGlowPowerEV.LerpFinished == false)
        {
            LerpPlayerGlowPowerEV.Lerp();
            playerGlow = Mathf.Lerp(1, 0, LerpPlayerGlowPowerEV.LerpFloat);
            playerMaterial.SetFloat("_GlowPower", playerGlow); 
        }
    }

    public void LerpPlayerGlowOpacity()
    {
        if (LerpPlayerGloweOpacityEV.LerpFinished == false)
        {
            LerpPlayerGloweOpacityEV.Lerp();
            playerGlowOpacity = Mathf.Lerp(1, 0, LerpPlayerGloweOpacityEV.LerpFloat);
            playerMaterial.SetFloat("_Opacity", playerGlowOpacity);
        }
    }

    public void ResetAllLerps()
    {
        LerpAfterImageOpacityEV.ResetLerp();
        LerpPlayerGloweOpacityEV.ResetLerp();
        LerpPlayerGlowPowerEV.ResetLerp();
    }

    public void TogglePlayerVisibilty()
    {
        if (playerIsInvisible == false)
        {
            foreach (GameObject GO in InvisibleGOs)
            {
                GO.SetActive(false);
                playerParent.tag = "Player_Dodging";
            }

            playerIsInvisible = true;
        }

        else
        {
            foreach (GameObject GO in InvisibleGOs)
            {
                GO.SetActive(true);
                playerParent.tag = "Player";
            }

            playerIsInvisible = false;
        }
    }
}
