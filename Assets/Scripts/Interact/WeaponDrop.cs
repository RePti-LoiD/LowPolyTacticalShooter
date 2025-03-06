using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class WeaponDrop : MonoBehaviour
{
    [SerializeField] private GunSwitch gunSwitch;
    [SerializeField] private float gunDropForce;
    [SerializeField] private int defaultLayerNumber;

    private GunAPI removedGun;
    private bool moveThisShit;

    public void OnDropGun()
    {
        removedGun = gunSwitch.RemoveCurrentGun();

        if (removedGun.TryGetComponent(out Rigidbody rb))
        {
            print("ASSS");

            removedGun.Disabled += (x) =>
            {
                rb.position = (Vector3.forward * 2);

                StartCoroutine(Wait(removedGun, rb, 0.1f));

                removedGun.Disabled = null;
            };

            
        }
    }

    private void FixedUpdate()
    {
        if (removedGun != null && moveThisShit)
            removedGun.GetComponent<Rigidbody>().position = (Camera.main.transform.position + Camera.main.transform.forward * 0.5f);
    }

    private IEnumerator Wait(GunAPI removedGun, Rigidbody rb, float time)
    {
        print("Я пидорас");
        yield return new WaitForFixedUpdate();
        moveThisShit = true;

        rb.transform.SetParent(null, true);

        rb.useGravity = true;
        rb.isKinematic = false;
        
        rb.AddForce(transform.forward * gunDropForce, ForceMode.VelocityChange);

        yield return new WaitForFixedUpdate();

        removedGun.transform.SetAllChildrenLayer(defaultLayerNumber);

        moveThisShit = false;
        
    }
}
