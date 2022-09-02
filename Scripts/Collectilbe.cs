using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectilbe : MonoBehaviour
{
    public GameObject collectSound;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("I collected a Strawberry!");
            ScoreControlScript.scoreValue += 1;

            GameObject collectSoundInstance = Instantiate(collectSound, transform.position, transform.rotation);

            collectSoundInstance.GetComponent<AudioSource>().Play();
            Destroy(collectSoundInstance, .5f);

            Destroy(gameObject);

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
