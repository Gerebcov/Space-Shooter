using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairArea : Item {

	List<Unit> unitInZone = new List<Unit>();
	[SerializeField]
	Fractions myFraction = Fractions.Environment;

	[SerializeField]
	float repairSpeed = 50f;

	[SerializeField]
	ParticleSystem repairFX = null;

	void Update()
	{
		if (unitInZone.Count > 0) {
			foreach (Unit U in unitInZone)
				U.Healing (repairSpeed * TimeManager.LevelFrameTime / unitInZone.Count);
		}
	}

	public override void Establish (Unit unit)
	{
		myFraction = unit.Fraction;
		base.Establish (unit);
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		var unit = collider.attachedRigidbody.gameObject.GetComponent<Unit> ();
		if (unit != null && unit.Fraction == myFraction) {
			unitInZone.Add (unit);
			if(repairFX && unitInZone.Count == 1)
				repairFX.Play (true);
		}
	}

	void OnTriggerExit2D(Collider2D collider)
	{
		var unit = collider.attachedRigidbody.gameObject.GetComponent<Unit> ();
		if (unit != null && unit.Fraction == myFraction) {
			unitInZone.Remove (unit);
			if (repairFX && unitInZone.Count == 0)
				repairFX.Stop (true, ParticleSystemStopBehavior.StopEmitting);
		}
	}

}
