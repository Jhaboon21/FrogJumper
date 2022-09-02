using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage;
    public Rigidbody2D rigidbody2d;
    public float speed = 10f;
    public GameObject explosion;
    public GameObject throwSound;
    public GameObject hitSound;

    //called by the player controller after it instantiate a new projectile to launch it.
    void Start()
    {

        rigidbody2d.velocity = transform.right * speed;

        GameObject throwSoundInstance = Instantiate(throwSound, transform.position, transform.rotation);

        throwSoundInstance.GetComponent<AudioSource>().Play();

        Destroy(throwSoundInstance, 2f);
    }

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Destroy(gameObject, 1f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
            Debug.Log("I killed an Enemy!");

            GameObject hitInstance = Instantiate(explosion, transform.position, transform.rotation);

            GameObject hitSoundInstance = Instantiate(hitSound, transform.position, transform.rotation);

            hitSoundInstance.GetComponent<AudioSource>().Play();

            Destroy(collision.gameObject);

            Destroy(hitInstance, 0.4f);
            Destroy(hitSoundInstance, 1f);
        }
        if (collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
            GameObject hitSoundInstance = Instantiate(hitSound, transform.position, transform.rotation);

            GameObject hitInstance = Instantiate(explosion, transform.position, transform.rotation);
            Destroy(hitInstance, 0.4f);

            hitSoundInstance.GetComponent<AudioSource>().Play();

            Destroy(hitSoundInstance, 1f);
        }
        if (collision.gameObject.tag == "Boss")
        {
            collision.collider.GetComponent<Boss>().health -= damage;

            GameObject hitInstance = Instantiate(explosion, transform.position, transform.rotation);

            GameObject hitSoundInstance = Instantiate(hitSound, transform.position, transform.rotation);

            hitSoundInstance.GetComponent<AudioSource>().Play();

            Destroy(hitInstance, 0.4f);
            Destroy(hitSoundInstance, 1f);

            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
