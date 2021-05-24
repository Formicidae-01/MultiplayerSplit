using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform bodyToFollow;

    public float smooth;

    private void FixedUpdate()
    {
        transform.position = Vector3.Slerp(transform.position, bodyToFollow.position, smooth);
        transform.rotation = Quaternion.Slerp(transform.rotation, bodyToFollow.rotation, smooth);
    }
}
