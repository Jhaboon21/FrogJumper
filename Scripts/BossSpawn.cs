using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawn : MonoBehaviour
{
    public GameObject bossPrefab;
    public Transform bSpawnPoint;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // deal the boss some damage + spawn particle effects + screen shake
        if (other.CompareTag("Player"))
        {
            GameObject bossSpawnIn = Instantiate(bossPrefab, bSpawnPoint.position, transform.rotation);
        }

    }
}
