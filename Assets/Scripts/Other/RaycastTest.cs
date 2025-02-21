using System;
using UnityEngine;

public class RaycastTest : MonoBehaviour
{
    [SerializeField] private LayerMask mask;
    [SerializeField] private float distance = 2f;

    private void Start()
    {
        print((mask.value, Convert.ToString(mask.value, 2)));
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * distance , Color.green);

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, distance, mask))
            print(hitInfo.transform.gameObject);
    }
}