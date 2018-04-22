using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class Sector : MonoBehaviour {

	[SerializeField]
	float sectorRange = 1000;
	[SerializeField]
	float sectorWallRange = 100;

	[SerializeField]
	GameObject asteroidSample = null;
	[SerializeField]
	int maxAsteroid = 10;
	[SerializeField]
	float minForse = 0, maxForse = 20;
	[SerializeField]
	PolygonCollider2D polygonCollider = null;

	GameObject[] asteroids;

	void OnEnable()
	{
		if (!Application.isPlaying)
			return;
		if (!polygonCollider)
			return;
		Vector2[] points = new Vector2[720];
		for (int i = 0; i < 360; i++) 
		{
			transform.Rotate (0, 0, 1);
			points[i] = transform.up * sectorRange;
			points[719 - i] = transform.up * (sectorRange + sectorWallRange);
		}
		polygonCollider.SetPath (0, points);
	}

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
			g.transform.parent = transform;
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
						spaynVector = Vector3.up * Random.Range(0.1f, 1);
					float angle = Random.Range (0, 360);
					GameObject newAsteroid = (GameObject)Instantiate (asteroidSample, transform.position + (spaynVector.normalized * sectorRange), Quaternion.Euler (0, 0, angle));
					asteroids[i] = newAsteroid;
					newAsteroid.transform.parent = transform;
					newAsteroid.GetComponent<Rigidbody2D> ().AddForce (newAsteroid.transform.up * Random.Range (minForse, maxForse), ForceMode2D.Impulse);
				}
			}
			yield return null;
		}
	}

}
