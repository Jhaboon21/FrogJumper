using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public GameObject pickupSound;


    // Start is called before the first frame update
    void Start()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("I collected an Apple!");
            GameControlScript.health += 1;

            GameObject healSoundInstance = Instantiate(pickupSound, transform.position, transform.rotation);

            healSoundInstance.GetComponent<AudioSource>().Play();
            Destroy(healSoundInstance, .5f);

            Destroy(gameObject);

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
