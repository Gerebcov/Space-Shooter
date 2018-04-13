﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

	[SerializeField]
	Rigidbody2D player;
	[SerializeField]
	float speed;
	[SerializeField]
	float maxSpeed;
	[SerializeField]
	ParticleSystem moveParticle;
	[SerializeField]
	float forse;

	[SerializeField]
	float atackSpeed;
	[SerializeField]
	GameObject buletSample;
	float lastFireTime;
	[SerializeField]
	Transform[] weapon;

	// Use this for initialization
	void Start () {
		player.centerOfMass = Vector2.zero;

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			moveParticle.Play (true);
			StartCoroutine(Move ());
		}
		if (Input.GetMouseButtonUp (0)) {
			moveParticle.Stop (true, ParticleSystemStopBehavior.StopEmitting);
			StopCoroutine(Move ());
		}

		if (Input.GetMouseButtonDown (1)) {
			StartCoroutine(Fire());
		}
		if (Input.GetMouseButtonUp (1)) {
			StopCoroutine(Fire());
		}


		Vector3 mousePosition = (Input.mousePosition - new Vector3(Screen.width / 2, Screen.height / 2, 0));
		float rotation_z = Mathf.Atan2(-mousePosition.normalized.x, mousePosition.normalized.y) * Mathf.Rad2Deg;
		player.transform.rotation = Quaternion.Euler(0f, 0f, rotation_z);    

		Camera.main.transform.position = player.transform.position - (Vector3.forward * 1000);

		forse = player.velocity.magnitude;
//		player.drag = (player.velocity.sqrMagnitude / (maxSpeed * maxSpeed)) / (1 - ((player.velocity.sqrMagnitude) / (maxSpeed * maxSpeed)));
	}

	public IEnumerator Move()
	{
		while (Input.GetMouseButton(0)) {
			player.AddForce(player.transform.up * speed * Time.deltaTime, ForceMode2D.Impulse); 
//			float speedPrsent = player.velocity.magnitude / maxSpeed;
//			if (speedPrsent > 1)
//				player.velocity = player.velocity / speedPrsent;
			yield return null;
		}

	}

	public IEnumerator Fire()
	{
		while (Input.GetMouseButton (1)) {
			if (lastFireTime < TimeManager.LevelTime) {
				foreach (Transform T in weapon) {
					GameObject g = (GameObject) Instantiate (buletSample, T.position, T.rotation);
					g.GetComponent<Rigidbody2D>().AddForce (player.velocity, ForceMode2D.Impulse);
				}
				lastFireTime = TimeManager.LevelTime + atackSpeed;
			}
			yield return null;
		}
	}
}