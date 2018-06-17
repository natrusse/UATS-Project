using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Manager : MonoBehaviour 
{
	
	public int lives = 3;
	public GameObject playerPrefab;
	[HideInInspector ()] public GameObject currentShip;
	private float timer = 0;
	public float asteroidSpawnTime = 1;
	public GameObject asteroidPrefab;
	public float asteroidForce = 2;
	public float asteroidDespawn = 20;


	public bool isEnabled = true;

	void Start() 
	{
		if (lives >= 0) {
			currentShip = Instantiate (playerPrefab);
			currentShip.transform.position = new Vector3 (0, 0, 0);
			currentShip.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 0));
		}
	}

	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyUp (KeyCode.P)) 
		{ // pause button location
			isEnabled = !isEnabled; 
		}
		if (Input.GetKeyUp (KeyCode.Escape)) {
			Application.Quit();
		}
		// Asteroid spawn timer
		timer += Time.deltaTime;
			if (timer > asteroidSpawnTime) {
			{
				timer = 0;
				timer += Time.deltaTime;
				if (timer >= asteroidDespawn) 
				{
					Destroy (gameObject);
				}

			}

			//spawns the asteroid

			Vector2 spawnposition = RandomCirclePoint (100);
			Vector2 targetposition = RandomCirclePoint (20);
			GameObject newAsteroid = Instantiate (asteroidPrefab);
			asteroidPrefab.transform.position = spawnposition;

			//Rotates asteroid toward center circle
			Vector3 rel = newAsteroid.transform.InverseTransformPoint (targetposition);
			float angle = Mathf.Atan2 (rel.x, rel.y) * Mathf.Rad2Deg;
			newAsteroid.transform.Rotate (0, 0, -angle);

			//makes asteroid move
			newAsteroid.GetComponent <Rigidbody2D> ().AddForce (newAsteroid.transform.up * asteroidForce, ForceMode2D.Impulse);
		}
	}

	Vector2 RandomCirclePoint (float diameter)
	{
		//Generate Random X within Radius
		float x = Random.Range (diameter / 2, diameter);
		//Solve for Y on Semi-Circle
		float y = Mathf.Sqrt ((Mathf.Pow (diameter, 2)) - (Mathf.Pow (x, 2)));
		//Invert Coordinates
		return randomlyInvertVector (x, y);
	}


	Vector2 randomlyInvertVector (float x, float y)
	{

		int signA = Random.Range (0, 2);
		if (signA != 0) {
			x = -x;

		} 
		int signB = Random.Range (0, 2);
		if (signB != 0) {
			y = -y;
		}
		return new Vector2 (x, y);
	}

	public void Death ()
	{

		lives -= 1;
		if (lives > 0) {

			currentShip.transform.position = new Vector3 (0, 0, 0);
			currentShip.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 0));
		} 
		if (lives == 0) {
			Destroy (currentShip.gameObject);
		}
	}
}
