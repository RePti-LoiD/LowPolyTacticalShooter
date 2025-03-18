using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ModifierManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> scopePrefabs;
    [SerializeField] private List<GameObject> ammoPrefabs;
    [SerializeField] private List<GameObject> muzzlePrefabs;
    [SerializeField] private List<GameObject> gripPrefabs;

    [SerializeField] private UnityEvent<GunScope> ScopeSet;
    [SerializeField] private UnityEvent<GunAmmo> AmmoSet;
    [SerializeField] private UnityEvent<GunMuzzle> MuzzleSet;
    [SerializeField] private UnityEvent<GunGrip> GripSet;

    [Space]
    [SerializeField] private ModifierAnchor scopeAnchor;
    [SerializeField] private ModifierAnchor ammoAnchor;
    [SerializeField] private ModifierAnchor muzzleAnchor;
    [SerializeField] private ModifierAnchor gripAnchor;

    [Space]
    [SerializeField] private GameObject ModifierUI;

    private GunScope currentScope;
    private GunAmmo currentAmmo;
    private GunMuzzle currentMuzzle;
    private GunGrip currentGrip;

    public GunScope CurrentScope 
    { 
        get => currentScope;
        set 
        { 
            scopeAnchor.SpawnModifier(value.gameObject);
            ScopeSet?.Invoke(value);

            currentScope = value; 
        }
    }

    public GunAmmo CurrentAmmo 
    { 
        get => currentAmmo; 
        set 
        { 
            ammoAnchor.SpawnModifier(value.gameObject);
            AmmoSet?.Invoke(value);

            currentAmmo = value; 
        }
    }

    public GunMuzzle CurrentMuzzle 
    { 
        get => currentMuzzle; 
        set 
        { 
            muzzleAnchor.SpawnModifier(value.gameObject);
            MuzzleSet?.Invoke(value);

            currentMuzzle = value; 
        } 
    }

    public GunGrip CurrentGrip
    {
        get => currentGrip;
        set
        {
            gripAnchor.SpawnModifier(value.gameObject);
            GripSet?.Invoke(value);

            currentGrip = value;
        }
    }

    private void Awake()
    {
        if (scopePrefabs.Count > 0)
            CurrentScope = scopePrefabs.FirstOrDefault().GetComponent<GunScope>();
        if (ammoPrefabs.Count > 0)
            CurrentAmmo = ammoPrefabs.FirstOrDefault().GetComponent<GunAmmo>();
        if (muzzlePrefabs.Count > 0)
        {
            print("Wait for muzzle");
            CurrentMuzzle = muzzlePrefabs.FirstOrDefault().GetComponent<GunMuzzle>();
        }
        if (gripPrefabs.Count > 0)
            CurrentGrip = gripPrefabs.FirstOrDefault().GetComponent<GunGrip>();
    }

    private int currentScopeIndex = 0;
    public void NextScope()
    {
        currentScopeIndex = (currentScopeIndex + 1) % scopePrefabs.Count;

        CurrentScope = scopePrefabs[currentScopeIndex].GetComponent<GunScope>();
    }

    private int currentMuzzleIndex = 0;
    public void NextMuzzle()
    {
        currentMuzzleIndex = (currentMuzzleIndex + 1) % muzzlePrefabs.Count;

        CurrentMuzzle = muzzlePrefabs[currentMuzzleIndex].GetComponent<GunMuzzle>();
    }

    public void EnableModifierUI()
    {
        ModifierUI.SetActive(true);
    }

    public void DisableModifierUI()
    {
        ModifierUI.SetActive(false);
    }
}