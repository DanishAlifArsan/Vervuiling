using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingSaw : MonoBehaviour
{
    [SerializeField] private float rotatingSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, 90 * rotatingSpeed * Time.deltaTime);
    }
}
