using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGameObject : MonoBehaviour {

	[SerializeField]
	bool imortal;
	public bool IsImortal {get {return imortal;}}
	[SerializeField]
	float maxHealsPoint;
	float healsPoint;
	public float HealsPoint{
		get{ return healsPoint;} 
		set{ if (!imortal) {
				healsPoint = value;
				if (healsPoint <= 0)
					Dead ();
			}
		}
	}
	[SerializeField]
	Defenses[] defenses;


	public Rigidbody2D Rigidbody;

	public void Dead()
	{
		Destroy (gameObject);
	}

	// Use this for initialization
	void Awake () {
		Inicialization ();
		if(!Rigidbody)
			Rigidbody = GetComponent<Rigidbody2D> ();
	}

	public void Inicialization()
	{
		if (IsImortal)
			return;
		HealsPoint = maxHealsPoint;
	}

	public void AddedDamage(float Damage, Constants.DamageTypes type)
	{
		if (IsImortal)
			return;
		float defensePersent = 0;
		foreach (Defenses D in defenses) {
			if (D.DefenseType == type) {
				defensePersent = D.DefensePersent;
				break;
			}
		}
		HealsPoint -= Damage * (1 - defensePersent);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnCollisionEnter2D(Collision2D collision) {
		if (IsImortal)
			return;
		float power = collision.relativeVelocity.magnitude;
		AddedDamage(power / 10 / Rigidbody.mass, Constants.DamageTypes.Kinetic);
	}
}
[System.Serializable]
public class Defenses
{
	public Constants.DamageTypes DefenseType;
	[Range(0, 1)]
	public float DefensePersent;
}
