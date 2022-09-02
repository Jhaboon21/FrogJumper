using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public int health;
    public int damage;
    private float timeBtwDamage = 1.5f;

    public Slider healthBar;
    private Animator anim;
    public bool isDead;

    private void Start()
    {
        anim = GetComponent<Animator>();
        healthBar = GameObject.Find("healthBar").GetComponent<Slider>();
        healthBar.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (health <= 0)
        {
            anim.SetTrigger("death");
        }

        if (timeBtwDamage > 0)
        {
            timeBtwDamage -= Time.deltaTime;
        }
        healthBar.value = health;
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player" && isDead == false && timeBtwDamage <= 0)
        {
            Player_Movement controller = other.collider.GetComponent<Player_Movement>();

            if (controller != null)
                controller.ChangeHealth(-1);
            Debug.Log("I Lost a Life!");
        }
    }

}