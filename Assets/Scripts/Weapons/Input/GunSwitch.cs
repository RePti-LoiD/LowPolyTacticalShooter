using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GunSwitch : MonoBehaviour
{
    [Header("Guns/events")]
    [SerializeField] private List<GunAPI> guns = new List<GunAPI>();
    [SerializeField] private GunAPIEvent GunSelected;
    [SerializeField] private UnityEvent<RuntimeGunData> GunDataChanged;

    [Space]
    [SerializeField] private float gunSwitchDelay;

    [Header("Data for oldGun")]
    [SerializeField] private Movement movement;
    [SerializeField] private MonoBehaviour coroutineRunner;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Transform gunTransform;
    [SerializeField] private RecoilRotationReceiver cameraRecoilRotationReceiver;
    [SerializeField] private ProjectileObjectPool projectilePool;

    private GunAPI currentGun;
    private GunAPI prevGun;

    private bool canSwitchGun = true;

    private int currentIndex = 0;
    public int CurrentIndex 
    { 
        private set
        {
            currentIndex = value;
        }
        get => Mathf.Abs(currentIndex) % 4;
    }

    private void Start()
    {
        if (projectilePool == null)
            projectilePool = FindAnyObjectByType<ProjectileObjectPool>();

        SelectGun(guns.First());
    }

    public GunAPI RemoveCurrentGun()
    {
        var gun = currentGun;
        guns.Remove(gun);

        SelectGun(guns.First());
        
        prevGun = null;

        return gun;
    }

    public void AddGun(GunAPI gun)
    {
        guns.Add(gun);
    }

    public void SwitchToPrevious()
    {
        if (prevGun == null) return;

        SelectGun(prevGun);
    }

    public void SelectGun(int index)
    {
        SelectGun(guns[index]);
    }

    public void SelectGun(GunAPI gun)
    {
        if (!canSwitchGun) return;
        if (currentGun == gun) return;

        if (currentGun != null)
        {
            currentGun.Disabled += (oldGun) =>
            {
                ActivateGun(gun);
                currentGun.Disabled = null;
            };

            currentGun.DisableGun();
        }
        else
        {
            ActivateGun(gun);
        }
    }

    private void ActivateGun(GunAPI gun)
    {
        if (currentGun != null)
            currentGun.GunDataChanged -= OnGunDataChanged;

        gun.EnableGun(new ExternalDataForGun
        {
            Movement = movement,
            CoroutineRunner = coroutineRunner,
            AudioSource = audioSource,
            GunTransform = gunTransform,
            CameraRecoilRotationReceiver = cameraRecoilRotationReceiver,
            ProjectilePool = projectilePool
        });

        prevGun = currentGun;
        currentGun = gun;

        currentGun.GunDataChanged += OnGunDataChanged;

        GunSelected?.Invoke(gun);
        GunDataChanged?.Invoke(gun.GunData);

        canSwitchGun = false;
        StartCoroutine(GunSwitchDelay(gunSwitchDelay));
    }

    private void OnGunDataChanged(RuntimeGunData data) =>
        GunDataChanged?.Invoke(data);

    private IEnumerator GunSwitchDelay(float time)
    {
        yield return new WaitForSeconds(time);

        canSwitchGun = true;
    }
}