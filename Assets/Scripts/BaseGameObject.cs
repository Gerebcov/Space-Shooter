using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGameObject : MonoBehaviour {
	

	[SerializeField]
	protected string unitName;
	public virtual string UnitName{
		get{
			if (unitName == "")
				unitName = "Object №" + gameObject.GetInstanceID ();
			return unitName;
		}
	}

	[SerializeField]
	Fractions fraction = Fractions.Environment;
	public Fractions Fraction{get {return fraction;} protected set{fraction = value;}}

	[SerializeField]
	bool imortal = false;
	public bool IsImortal {get {return imortal;} set {imortal = value;}}
	[SerializeField]
	float maxHealsPoint = 0;
	public float MaxHealsPoint{get{return maxHealsPoint;}}
	[SerializeField]
	float healsPoint;
	public float HealsPoint{
		get{ return healsPoint;} 
		set{ if (!imortal) {
				healsPoint = Mathf.Clamp(value, 0, maxHealsPoint);
				if (healsPoint <= 0)
					Dead ();
			}
		}
	}
	[SerializeField]
	GameObject DeadFX = null;
	[SerializeField]
	Defenses[] defenses = null;



	public Rigidbody2D[] Rigidbodies;

	public float Mass{
		get{
			float mass = 0;
			foreach (Rigidbody2D R in Rigidbodies)
				mass += R.mass;
			return mass;
		}
		set{
			foreach (Rigidbody2D R in Rigidbodies)
				R.mass += value / Rigidbodies.Length;
		}
	}

	public virtual void Dead()
	{
		if(DeadFX)
			Instantiate (DeadFX, transform.position, transform.rotation, transform.parent);
		Destroy (gameObject);
	}

	// Use this for initialization
	void Awake () {
		Inicialization ();
	}

	public void Inicialization()
	{
		if(Rigidbodies.Length == 0)
			Rigidbodies = GetComponentsInChildren<Rigidbody2D> ();
		Rigidbodies [0].centerOfMass = Vector2.zero;
		if (IsImortal)
			return;
		HealsPoint = MaxHealsPoint;
	}

	public void AddedDamage(float Damage, DamageTypes type)
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

	public void Healing(float healsPoint)
	{
		HealsPoint += healsPoint;
	}


	
	void OnCollisionEnter2D(Collision2D collision) {
		if (IsImortal || collision.collider.isTrigger)
			return;
		float power = collision.relativeVelocity.magnitude;
		AddedDamage((power / 10) * Mass, DamageTypes.Kinetic);
	}
}
[System.Serializable]
public class Defenses
{
	public DamageTypes DefenseType;
	public float DefensePersent;
}
