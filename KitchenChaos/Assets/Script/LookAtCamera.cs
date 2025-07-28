using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookACamera : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
    }
}
