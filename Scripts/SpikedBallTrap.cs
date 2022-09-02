using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikedBallTrap : MonoBehaviour
{
	public float speed;
	public float distance;
	private bool isFacingRight = true;

	public Transform groundDetection;
	public GameObject hurtSound;

	void Start()
	{

	}

	void Update()
	{
		transform.Translate(Vector2.right * speed * Time.deltaTime);

		RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
		if (groundInfo.collider == false)
		{
			if (isFacingRight == true)
			{
				transform.eulerAngles = new Vector3(0, -180, 0);
				isFacingRight = false;
			}
			else if (isFacingRight == false)
			{
				transform.eulerAngles = new Vector3(0, 0, 0);
				isFacingRight = true;
			}
		}
	}

	void OnCollisionStay2D(Collision2D collision)
	{
        if (collision.gameObject.tag == "Player")
		{
			Player_Movement controller = collision.collider.GetComponent<Player_Movement>();

			if (controller != null)
				controller.ChangeHealth(-1);
			Debug.Log("I Lost a Life!");

		}
	}
}

