using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticTurret : Item {

	List<BaseGameObject> agroList = new List<BaseGameObject> ();
	BaseGameObject currentTarget = null;

	[SerializeField]
	BaseWeapon weapon = null;

	[SerializeField]
	Transform rotateAnchor = null;
	[SerializeField]
	float maxAngle = 3f;
	[SerializeField]
	float rotationSpeed = 45f;

	int parentID = 0;

	[SerializeField]
	Collider2D agroCollider = null;

	bool isFire = false;
	public bool IsFire
	{
		set{
			if (isFire != value) {
				if (value)
					weapon.ActivateItem ();
				else
					weapon.DeactivateItem ();
				isFire = value;
			}
		}
	}

	Fractions myFraction = Fractions.Environment;

	public enum TurretStates{
		Idle,
		Activ,
		EnemyDetected
	}

	void Awake()
	{
		InitializeStateManager (typeof(TurretStates));
		InitializeState (
			(int)TurretStates.Idle,
			IdleStartHandler,
			null,
			IdleEndHandler);
		InitializeState (
			(int)TurretStates.Activ,
			null,
			ActivUpdateHandler,
			null);
		InitializeState (
			(int)TurretStates.EnemyDetected,
			null,
			EnemyDetectedUpdateHandler,
			null);
		SetState ((int)TurretStates.Idle);
	}

	public override void Establish (Unit unit)
	{
		myFraction = unit.Fraction;
		parentID = unit.ID;
		weapon.Establish (unit);
		SetState ((int)TurretStates.Activ);
		base.Establish (unit);
	} 

	public override void Dismantle ()
	{
		SetState ((int)TurretStates.Idle);
		base.Dismantle ();
	}

	public void IdleStartHandler()
	{
		agroCollider.enabled = false;
		agroList = new List<BaseGameObject> ();
	}

	public void IdleEndHandler()
	{
		agroCollider.enabled = true;
	}

	public void ActivUpdateHandler()
	{
		if (agroList.Count != 0) {
			for (int i = 0; i < agroList.Count; i++) {
				if (agroList [i].gameObject == null || (agroList [i].gameObject && !agroList [i].gameObject.activeSelf)) {
					agroList.Remove (agroList [i]);
					i--;
				} else if (!agroList [i].IsImortal) {
					currentTarget = agroList [i];
				}
			}
			SetState ((int)TurretStates.EnemyDetected);
		}
	}

	public void EnemyDetectedUpdateHandler()
	{
		if (!currentTarget || currentTarget.IsImortal) {
			currentTarget = null;
			IsFire = false;
			SetState ((int)TurretStates.Activ);
			return;
		}
		Vector2 enemyVector = currentTarget.transform.position - weapon.SpaunPoint.position;
		float relativeSpeed = currentTarget.Rigidbodies [0].GetRelativeVector(enemyVector.normalized).magnitude;
		float timetoContact = enemyVector.magnitude / (weapon.BulletAcceleration - (relativeSpeed / 2));
		enemyVector = (currentTarget.transform.position + ((Vector3)currentTarget.Rigidbodies [0].velocity * timetoContact)) - rotateAnchor.position;
		float rotation_z = Mathf.Atan2(-enemyVector.x, enemyVector.y) * Mathf.Rad2Deg;
		if (rotation_z < 0)
			rotation_z += 360;
		float deltaAngteA = rotation_z - rotateAnchor.eulerAngles.z;
		float deltaAngteB = Mathf.Abs(deltaAngteA) - 360;
		if (deltaAngteA < 0)
			deltaAngteB = Mathf.Abs(deltaAngteB);
		float deltaAngte = Mathf.Abs (deltaAngteA) < Mathf.Abs (deltaAngteB) ? deltaAngteA : deltaAngteB;
		float angleRotate = Mathf.Min (Mathf.Abs(deltaAngte), rotationSpeed * TimeManager.LevelFrameTime);
		rotateAnchor.rotation = Quaternion.Euler(0f, 0f, rotateAnchor.eulerAngles.z + (angleRotate * Mathf.Sign(deltaAngte)));    

		Debug.DrawRay (rotateAnchor.position, rotateAnchor.up * enemyVector.magnitude);

		RaycastHit2D[] hits;
		hits = Physics2D.RaycastAll (rotateAnchor.position, enemyVector);

		if (hits.Length != 0)
		{
			foreach (RaycastHit2D hit in hits) {
				if (!hit.collider.isTrigger) {
					BaseGameObject hitObject = hit.collider.GetComponent<BaseGameObject> ();
					if (hitObject == null && hit.collider.attachedRigidbody != null)
						hitObject = hit.collider.attachedRigidbody.GetComponent<BaseGameObject> ();
					if (hitObject && hitObject.ID != parentID) {
						if (hitObject == currentTarget)
							break;
						if (hitObject.IsImortal || hitObject.Fraction == myFraction) {
							if (agroList.Count > 1)
								GetAnotherGoal ();
							else
								IsFire = false;
							return;
						}
					}
				}
			}
		}

		if (Mathf.Abs(deltaAngte) < maxAngle) {
			IsFire = true;
			return;
		}


	}

	void GetAnotherGoal()
	{
		foreach (BaseGameObject G in agroList) {
			if (G != currentTarget) {
				currentTarget = G;
				return;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		var possibleEnemy = collider.attachedRigidbody.gameObject.GetComponent<BaseGameObject> ();
		if (!possibleEnemy || possibleEnemy.IsImortal)
			possibleEnemy = collider.gameObject.GetComponent<BaseGameObject> ();
		if (!possibleEnemy)
			return;
		if (possibleEnemy.Fraction != myFraction && !possibleEnemy.IsImortal) {
			agroList.Add (possibleEnemy);
		}
	}

	void OnTriggerExit2D(Collider2D collider)
	{
		var possibleEnemy = collider.attachedRigidbody.gameObject.GetComponent<BaseGameObject> ();
		if (!possibleEnemy)
			possibleEnemy = collider.gameObject.GetComponent<BaseGameObject> ();
		if (!possibleEnemy)
			return;
		if (possibleEnemy.Fraction != myFraction) {
			agroList.Remove (possibleEnemy);
			if (currentTarget == possibleEnemy)
				currentTarget = null;
		}
	}
}
