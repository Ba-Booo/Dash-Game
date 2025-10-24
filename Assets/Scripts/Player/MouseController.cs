using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{

    Vector3 screenPosition;
    Vector3 mouse;

    void FixedUpdate()
    {
        screenPosition = Input.mousePosition;
        screenPosition.z = Camera.main.nearClipPlane + 10;

        mouse = Camera.main.ScreenToWorldPoint( screenPosition );

        transform.position = mouse;
    }
}