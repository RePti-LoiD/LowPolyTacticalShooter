using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ReticleCycling
{
    public bool reticleCycling = false;

    public int reticleIndex;

    public List<Texture2D> allReticles;
}

[System.Serializable]
public class ReticleIllumnation
{
    public bool dynamicReticleIllumnation = false;

    public float illumnationSteps = 0.5f;

    public float currentIllumnation = 10;
    public float MaxIllumnation = 35;
    public float MinIllumnation = 10;
}

public class ReflexSightV2 : MonoBehaviour
{

    [Header("------ KEYCODES ------")]
    public KeyCode activatorKey = KeyCode.LeftControl;
    [Space(10)]
    public KeyCode switchReticleKey = KeyCode.Mouse1;
    [Space(10)]
    public KeyCode brightnessIncrease = KeyCode.UpArrow;
    public KeyCode brightnessDecrease = KeyCode.DownArrow;
    [Space(10)]
    public Material ReticleMaterial;
    [Space(10)]
    public ReticleCycling reticles;
    public ReticleIllumnation reticleIllumnation;
    [Space(10)]
    public AudioSource transitionSound;

    private void Start()
    {
        if (ReticleMaterial == null)
        {
            ReticleMaterial = GetComponent<MeshRenderer>().material;

            if (ReticleMaterial == null)
            {
                Debug.LogError("NO MATERIAL ASSIGNED! " + this.gameObject.name);
            }
        }

        if (reticles.reticleCycling)
        {
            cycleReticle();
        }

        if(reticleIllumnation.dynamicReticleIllumnation)
        {
            reticleIllumnation.currentIllumnation = ReticleMaterial.GetFloat("Reticle_Brightness");

            if (reticleIllumnation.currentIllumnation > reticleIllumnation.MaxIllumnation)
                reticleIllumnation.currentIllumnation = reticleIllumnation.MaxIllumnation;

            if (reticleIllumnation.currentIllumnation < reticleIllumnation.MinIllumnation)
                reticleIllumnation.currentIllumnation = reticleIllumnation.MinIllumnation;

            ReticleMaterial.SetFloat("Reticle_Brightness", reticleIllumnation.currentIllumnation);
        }
    }

    public void Update()
    {
        if (reticles.reticleCycling)
        {
            if (Input.GetKey(activatorKey))
            {
                if (Input.GetKeyDown(switchReticleKey))
                {
                    cycleReticle();
                }
            }
        }

        if (reticleIllumnation.dynamicReticleIllumnation)
        {
            if (Input.GetKey(brightnessIncrease))
            {
                reticleIllumnation.currentIllumnation += reticleIllumnation.illumnationSteps;

                if (reticleIllumnation.currentIllumnation > reticleIllumnation.MaxIllumnation)
                    reticleIllumnation.currentIllumnation = reticleIllumnation.MaxIllumnation;
            }
            if (Input.GetKey(brightnessDecrease))
            {
                reticleIllumnation.currentIllumnation -= reticleIllumnation.illumnationSteps;

                if (reticleIllumnation.currentIllumnation < reticleIllumnation.MinIllumnation)
                    reticleIllumnation.currentIllumnation = reticleIllumnation.MinIllumnation;
            }

            ReticleMaterial.SetFloat("Reticle_Brightness", reticleIllumnation.currentIllumnation);
        }
    }

    public void cycleReticle()
    {
        reticles.reticleIndex++;

        if (reticles.reticleIndex > reticles.allReticles.Count - 1)
            reticles.reticleIndex = 0;

        ReticleMaterial.SetTexture("Reticle", reticles.allReticles[reticles.reticleIndex]);


        if (transitionSound.clip != null)
            transitionSound.Play();
    }

}
