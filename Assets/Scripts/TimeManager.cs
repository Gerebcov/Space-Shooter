using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour {

	public static float LevelTimeScale = 1;
	public static float LevelTime { get; private set;}
	public static float LevelFrameTime{ get; private set;}
	public static float LevelFixedDeltaTime{ get; private set;}


	// Use this for initialization
	void FixedUpdate () {
		LevelFixedDeltaTime = Time.fixedDeltaTime * LevelTimeScale;
		LevelTime += LevelFixedDeltaTime;
	}
	
	// Update is called once per frame
	void Update () {
		LevelFrameTime = Time.deltaTime * LevelTimeScale;
	}
}
