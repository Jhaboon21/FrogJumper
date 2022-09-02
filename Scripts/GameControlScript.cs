using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControlScript : MonoBehaviour
{
    public GameObject heart1, heart2, heart3, gameOver;
    public static int health;
    float timerDisplay;
    public float displayTime = 2.0f;
    public Transform respawnPosition;
    public Transform Player;

    // Start is called before the first frame update
    void Start()
    {
        health = 3;
        heart1.gameObject.SetActive(true);
        heart2.gameObject.SetActive(true);
        heart3.gameObject.SetActive(true);
        gameOver.gameObject.SetActive(false);
        timerDisplay = -1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (health > 3)
        {
            health = 3;
        }

        if (timerDisplay >= 0)
        {
            timerDisplay -= Time.deltaTime;
            if (timerDisplay < 0)
            {
                gameOver.SetActive(false);
            }
        }

        switch (health)
        {
            case 3:
                heart1.gameObject.SetActive(true);
                heart2.gameObject.SetActive(true);
                heart3.gameObject.SetActive(true);
                break;
            case 2:
                heart1.gameObject.SetActive(true);
                heart2.gameObject.SetActive(true);
                heart3.gameObject.SetActive(false);
                break;
            case 1:
                heart1.gameObject.SetActive(true);
                heart2.gameObject.SetActive(false);
                heart3.gameObject.SetActive(false);
                break;
            case 0:
                heart1.gameObject.SetActive(false);
                heart2.gameObject.SetActive(false);
                heart3.gameObject.SetActive(false);
                DisplayDialog();
                Respawn();
                Destroy(Player.gameObject);

                break;
        }
    }
    void Respawn()
    {
        health = 3;
        Instantiate(Player, respawnPosition.position, respawnPosition.rotation);
    }
    public void DisplayDialog()
    {
        timerDisplay = displayTime;
        gameOver.SetActive(true);
    }
}