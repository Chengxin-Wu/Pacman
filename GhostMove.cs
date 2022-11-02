using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GhostMove : MonoBehaviour 
{
	public GameObject[] WayPointsGos;
	public float speed = 0.2f;
	private List<Vector3> wayPoints = new List<Vector3> ();
	private int index = 0;
	private Vector3 startPos;

	private void Start()
	{
		startPos = transform.position + new Vector3 (0, 3, 0);
		LoadPath(WayPointsGos[gameManager.Instance.usingIndex[GetComponent<SpriteRenderer>().sortingOrder - 2]]);
	}

	private void LoadPath(GameObject go)
	{
		wayPoints.Clear ();
		foreach (Transform t in go.transform) {
			wayPoints.Add (t.position);
		}
		wayPoints.Insert (0, startPos);
		wayPoints.Add (startPos);
	}

	private void FixedUpdate()
	{
		if (transform.position != wayPoints [index]) 
		{
			Vector2 temp = Vector2.MoveTowards (transform.position, wayPoints [index], speed);
			GetComponent<Rigidbody2D> ().MovePosition (temp);
		} 
		else 
		{
			index++;
			if (index >= wayPoints.Count) 
			{
				index = 0;
				LoadPath (WayPointsGos [Random.Range (0, WayPointsGos.Length)]);
			}
		}

		Vector2 dir = wayPoints [index] - transform.position;
		GetComponent<Animator> ().SetFloat ("DirX", dir.x);
		GetComponent<Animator> ().SetFloat ("DirY", dir.y);
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.name == "Pacman") 
		{
			if (gameManager.Instance.isSuperPacman) 
			{
				transform.position = startPos - new Vector3(0,3,0);
				index = 0;
				gameManager.Instance.Score += 500;
			} 
			else 
			{
				collision.gameObject.SetActive (false);
				gameManager.Instance.GamePanel.SetActive (false);
				Instantiate (gameManager.Instance.GameOverPrefab);
				Invoke ("ReStart", 3f);
			}
		}
	}

	private void ReStart()
	{
		SceneManager.LoadScene (0);
	}
}