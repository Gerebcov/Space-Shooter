using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class Sector : MonoBehaviour {

	[SerializeField]
	float sectorRange;
	[SerializeField]
	float sectorWallRange = 100;

	[SerializeField]
	CircleCollider2D sectorZone, sectorWall;

	[SerializeField]
	GameObject asteroidSample;
	[SerializeField]
	int maxAsteroid = 10;


	GameObject[] asteroids;


	// Use this for initialization
	void Start () {
		if (!Application.isPlaying)
			return;
		asteroids = new GameObject[maxAsteroid];
		for(int i = 0; i < maxAsteroid / 2; i++)
		{
			Vector3 spaynPoint = new Vector3 (Random.Range (-sectorRange, sectorRange), Random.Range (-sectorRange, sectorRange), 0);
			float angle = Random.Range (0, 360);
			GameObject g = (GameObject)Instantiate (asteroidSample, spaynPoint, Quaternion.Euler(0, 0, angle));
			asteroids[i] = g;
		}
		StartCoroutine (Spayner ());
	}

	IEnumerator Spayner()
	{
		while (true) {
			for (int i = 0; i < maxAsteroid; i++) {
				if (asteroids [i] == null) {
					Vector3 spaynVector = new Vector3 (Random.Range (-1, 1), Random.Range (-1, 1), 0);
					if (spaynVector.magnitude == 0)
						spaynVector = Vector3.up;
					float angle = Random.Range (0, 360);
					GameObject newAsteroid = (GameObject)Instantiate (asteroidSample, transform.position + (spaynVector * sectorRange), Quaternion.Euler (0, 0, angle));
					asteroids[i] = newAsteroid;
					newAsteroid.GetComponent<Rigidbody2D> ().AddForce (newAsteroid.transform.up * Random.Range (15, 40), ForceMode2D.Impulse);
				}
			}
			yield return null;
		}
	}


	#if UNITY_EDITOR
	// Update is called once per frame
	void Update () {
		sectorZone.radius = sectorRange;
		sectorWall.radius = sectorWallRange + sectorRange;
	}
	#endif
}
