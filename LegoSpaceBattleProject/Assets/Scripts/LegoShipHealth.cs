using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegoShipHealth : MonoBehaviour
{
    public int health = 3;
    public List<GameObject> particlesExplosion;
    public GameObject soundBreaking;

    public bool good, bad;

    void Start()
    {
        
    }
    void Update()
    {
        if (health <= 0)
        {
            // Explode ship with particles and sound at ship position
            for (int i = 0; i < particlesExplosion.Count; i++)
            {
                Instantiate(particlesExplosion[i], gameObject.transform.position, gameObject.transform.rotation);
            }
            soundBreaking.GetComponent<AudioSource>().Play();
            
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (good)
        {
            if (collision.gameObject.name == "BulletBad(Clone)")
            {
                health--;
            }
        }

        if (bad)
        {
            if (collision.gameObject.name == "Bullet(Clone)")
            {
                health--;
            }
        }
    }
}
