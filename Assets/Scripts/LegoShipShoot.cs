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
    public GameObject target;
    public float projectileSpeed = 20f;
    public float lifeTime = 3f;
    public float spawnDelay = 1f;
    private bool canShoot = true;

    public List<GameObject> bullets;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if(Vector3.Distance(gameObject.transform.position, target.transform.position) < 40.0f && canShoot)
        {
            StartCoroutine(ShootShip());
        }
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
        canShoot = false;
        yield return new WaitForSeconds(1f);
        canShoot = true;
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
}
