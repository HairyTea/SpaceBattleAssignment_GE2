using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegoShipShoot : MonoBehaviour
{
    private GameObject projectile;
    private GameObject projectileTwo;
    public GameObject projectilePrefab;
    public Transform projectileSpawn;
    public Transform projectileSpawnTwo;
    public float projectileSpeed = 20f;
    public float lifeTime = 3f;
    public float spawnDelay = 1f;
    private bool canShoot = true;
    public GameObject soundShoot;

    public List<GameObject> bullets;
    public bool Good,Bad = false;
    private bool isShooting = false;

    void Start()
    {
        
    }

    void FixedUpdate()
    {

    }

    public void Fire()
    {
        projectile = Instantiate(projectilePrefab);
        projectile.transform.position = projectileSpawn.position;
        Vector3 rotation = projectile.transform.rotation.eulerAngles;
        projectile.transform.rotation = Quaternion.Euler(rotation.x, transform.eulerAngles.y, rotation.z);
        projectile.GetComponent<Rigidbody>().AddForce(projectileSpawn.forward * projectileSpeed, ForceMode.Impulse);
        bullets.Add(projectile);

        projectileTwo = Instantiate(projectilePrefab);
        projectileTwo.transform.position = projectileSpawnTwo.position;
        rotation = projectileTwo.transform.rotation.eulerAngles;
        projectileTwo.transform.rotation = Quaternion.Euler(rotation.x, transform.eulerAngles.y, rotation.z);
        projectileTwo.GetComponent<Rigidbody>().AddForce(projectileSpawnTwo.forward * projectileSpeed, ForceMode.Impulse);
        bullets.Add(projectile);
        
        StartCoroutine(DestroyBullet());
    }    

    IEnumerator ShootShip()
    {
        Fire();
        soundShoot.GetComponent<AudioSource>().Play();
        canShoot = false;
        yield return new WaitForSeconds(1f);
        canShoot = true;

        yield return ShootShip();
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(lifeTime);
        for (int i = 1; i < bullets.Count; i++)
        {
            Destroy(bullets[i]);
        }
        bullets.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Good)
        {
            if (other.gameObject.CompareTag("Bad"))
            {
                StartCoroutine(ShootShip());
            }
        }

        if (Bad)
        {
            if (other.gameObject.CompareTag("Good"))
            {
                StartCoroutine(ShootShip());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (Good)
        {
            if (other.gameObject.CompareTag("Bad"))
            {
                StopAllCoroutines();
            }
        }

        if (Bad)
        {
            if (other.gameObject.CompareTag("Good"))
            {
                StopAllCoroutines();
            }
        }
    }
}
