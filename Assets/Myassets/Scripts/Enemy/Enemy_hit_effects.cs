using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_hit_effects : MonoBehaviour
{
    SkinnedMeshRenderer enemyRenderer;
    public Color flashColor;
    private Color originalColor;
    public Color debug;
    public float flashTime;
    private float OriginalFresnelFactor;
    public float HitFresnelFactor;
    public float TargetFresnel;
    public float fresnelLerpSpeedMultiplier;
    public float lerpInterpolation;

    private bool lerpFresnel;
    public bool lerpFresnelForward;
    public float timeBetweenLerps;
    public float timerCount;

    MaterialPropertyBlock shaderProperties;

    // Start is called before the first frame update
    void Start()
    {
        shaderProperties = new MaterialPropertyBlock();

        enemyRenderer = this.GetComponentInParent<SkinnedMeshRenderer>();
        originalColor = enemyRenderer.material.color;
        OriginalFresnelFactor = enemyRenderer.material.GetFloat("_FresnelFactor");
        HitFresnelFactor = OriginalFresnelFactor;
    }

    void Update()
    {

        shaderProperties.SetFloat("_FresnelFactor", HitFresnelFactor);
        LerpFresnelFactor();
        enemyRenderer.SetPropertyBlock(shaderProperties);
        Timer();
    }

    public void FlashStart()
    {
        PropertyBlockFlash();
        Invoke("FlashStop", flashTime);
        lerpFresnel = true;
    }

    void FlashStop()
    {
        PropertyBlockNormal();
        lerpFresnel = false;
        HitFresnelFactor = OriginalFresnelFactor;
    }

    void LerpFresnelFactor()
    {
        if (lerpFresnel == true)
        {
            if (lerpFresnelForward == true)
            {
                lerpInterpolation += Time.deltaTime * fresnelLerpSpeedMultiplier;
                lerpInterpolation = Mathf.Clamp(lerpInterpolation, 0, 1);
                HitFresnelFactor = Mathf.Lerp(OriginalFresnelFactor, TargetFresnel, lerpInterpolation);
            }

            else if (lerpFresnelForward == false)
            {
                lerpInterpolation -= Time.deltaTime * fresnelLerpSpeedMultiplier;
                lerpInterpolation = Mathf.Clamp(lerpInterpolation, 0, 1);
                HitFresnelFactor = Mathf.Lerp(TargetFresnel, OriginalFresnelFactor, lerpInterpolation);
            }
        }
    }

    void Timer()
    {
        timerCount += Time.deltaTime;

        if (timerCount >= timeBetweenLerps)
        {
            timerCount = 0;
            lerpFresnelForward = !lerpFresnelForward;
        }
    }

    void PropertyBlockFlash()
    {
        shaderProperties.SetFloat("_FresnelFactor", HitFresnelFactor);
        shaderProperties.SetColor("_Color", flashColor);
    }

    void PropertyBlockNormal()
    {
        shaderProperties.SetFloat("_FresnelFactor", OriginalFresnelFactor);
        shaderProperties.SetColor("_Color", originalColor);
    }
}
