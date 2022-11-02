using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanMove : MonoBehaviour 
{
	public float speed = 0.35F;
	private Vector2 dest = Vector2.zero;

	private void Start()
	{
		dest = transform.position;
	}

	private void FixedUpdate()
	{
		Vector2 temp = Vector2.MoveTowards (transform.position, dest, speed);
		GetComponent<Rigidbody2D>().MovePosition (temp);
		if ((Vector2)transform.position == dest) 
		{
			if ((Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.W) ) && Vaild(Vector2.up)) 
			{
				dest = (Vector2)transform.position + Vector2.up;
			}
			if ((Input.GetKey (KeyCode.DownArrow) || Input.GetKey (KeyCode.S) ) && Vaild(Vector2.down)) 
			{
				dest = (Vector2)transform.position + Vector2.down;
			}
			if ((Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.A) ) && Vaild(Vector2.left)) 
			{
				dest = (Vector2)transform.position + Vector2.left;
			}
			if ((Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.D) ) && Vaild(Vector2.right)) 
			{
				dest = (Vector2)transform.position + Vector2.right;
			}
		}
	}

	private bool Vaild(Vector2 dir)
	{
		Vector2 pos = transform.position;
		RaycastHit2D hit = Physics2D.Linecast (pos + dir, pos);
		return (hit.collider == GetComponent<Collider2D> ());
	}
}
