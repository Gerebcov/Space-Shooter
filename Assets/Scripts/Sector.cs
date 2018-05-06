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
			float angl = i / Mathf.Rad2Deg;
			Vector2 v = new Vector2(Mathf.Sin(angl), Mathf.Cos(angl));
			points[i] = v * sectorRange;
			points[719 - i] = v * (sectorRange + sectorWallRange);
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
			float angleSpaun = Random.Range (0, 360) / Mathf.Rad2Deg;
			Vector3 spaynPoint = new Vector3 (Mathf.Sin(angleSpaun), Mathf.Cos(angleSpaun), 0) * Random.Range(0, sectorRange);
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
					float angleSpaun = Random.Range (0, 360) / Mathf.Rad2Deg;
					Vector3 spaynPoint = new Vector3 (Mathf.Sin(angleSpaun), Mathf.Cos(angleSpaun), 0) * sectorRange;
					float angle = Random.Range (0, 360);
					GameObject newAsteroid = (GameObject)Instantiate (asteroidSample, spaynPoint, Quaternion.Euler (0, 0, angle));
					asteroids[i] = newAsteroid;
					newAsteroid.transform.parent = transform;
					newAsteroid.GetComponent<Rigidbody2D> ().AddForce (newAsteroid.transform.up * Random.Range (minForse, maxForse), ForceMode2D.Impulse);
				}
			}
			yield return null;
		}
	}

}
