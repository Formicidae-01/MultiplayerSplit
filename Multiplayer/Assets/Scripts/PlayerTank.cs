using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTank : MonoBehaviour
{
    public Rigidbody rb;

    public float moveSpeed, brakeSpeed, rotateSpeed;


    public KeyCode leftK, rightK, frontK, backK, shootK;
    Vector3 moveVector, rotateVector;

    public GameObject bullet;
    public Transform shootOrigin;
    public float bulletSpeed, ySpeed;
    public float timeToShoot, timeSinceShoot;
    bool canShoot;

    public ParticleSystem gunParticle;

    public Animator cameraAnim;

    public int HP = 3;
    public Image hpBar;

    public TankColor tankColor;

    private void Update()
    {
        if (Input.GetKey(frontK))
        {
            moveVector = transform.forward * moveSpeed;
        }

        else if (Input.GetKey(backK))
        {
            moveVector = -transform.forward * brakeSpeed;
        }

        else
        {
            moveVector = Vector3.zero;
        }


        if (Input.GetKey(rightK))
        {
            rotateVector = new Vector3(0, 1, 0);
        }

        else if (Input.GetKey(leftK))
        {
            rotateVector = new Vector3(0, -1, 0);
        }

        else
        {
            rotateVector = Vector3.zero;
        }



        if (Input.GetKey(shootK))
        {
            if (canShoot)
            {
                Shoot();
            }
        }




    }

    private void FixedUpdate()
    {
        rb.AddForce(moveVector);
        rb.AddTorque(rotateVector * rotateSpeed);

        if (!canShoot)
        {
            timeSinceShoot += Time.deltaTime;
            if (timeSinceShoot > timeToShoot)
            {
                canShoot = true;
            }
        }
    }

    public void Shoot()
    {
        GameObject tmpBullet = Instantiate(bullet, shootOrigin.position, shootOrigin.rotation);
        tmpBullet.GetComponent<Rigidbody>().AddForce(new Vector3(transform.forward.x * bulletSpeed, ySpeed, transform.forward.z * bulletSpeed));

        gunParticle.Emit(10);

        ResetShootDelay();
    }

    public void ResetShootDelay()
    {
        canShoot = false;
        timeSinceShoot = 0;
    }

    public void TakeDamage()
    {
        if (GameManager.instance.gameIsOver)
        {
            return;
        }
        cameraAnim.SetTrigger("Shake");

        HP--;

        if (HP > 0)
        {
            hpBar.fillAmount -= 0.33f;
        }

        else
        {
            hpBar.fillAmount = 0;
            Die();
        }
    }

    public void Die()
    {
        GameManager.instance.FinishGame(tankColor);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            TakeDamage();
        }
    }
}

public enum TankColor
{
    GREEN,
    RED,
}
