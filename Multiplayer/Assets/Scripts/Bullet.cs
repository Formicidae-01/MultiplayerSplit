using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody rb;
    public Renderer ren;
    public Collider col;
    public ParticleSystem p;


    private void OnTriggerEnter(Collider other)
    {
        rb.isKinematic = true;
        ren.enabled = false;
        col.enabled = false;
        p.Emit(100);
        Destroy(gameObject, p.startLifetime);
    }
}
