using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationTest : MonoBehaviour {

	[SerializeField]
	Collider2D myCollider;
	[SerializeField]
	float radius;
	[SerializeField]
	List<ContactInfo> contactInfo;
	[SerializeField]
	Transform target;
	[SerializeField]
	float rotationSpeed = 50;

	// Update is called once per frame
	void Update () {
		contactInfo = new List<ContactInfo> ();
		List<Vector2> vectorsD = new List<Vector2> ();
		List<Collider2D> colliderD = new List<Collider2D> ();
		Collider2D[] colliders = Physics2D.OverlapCircleAll (transform.position, radius);
		foreach (Collider2D C in colliders) {
			if (C != myCollider && !C.isTrigger) {
				float speed1 = Vector2.Dot(C.attachedRigidbody.velocity, (myCollider.attachedRigidbody.position - C.attachedRigidbody.position).normalized);
				float speed2 = Vector2.Dot (myCollider.attachedRigidbody.velocity, (C.attachedRigidbody.position - myCollider.attachedRigidbody.position).normalized);
				float relativSpeed = speed1 + speed2;
				if (relativSpeed > 0) {
					RaycastHit2D[] hits = new RaycastHit2D[50];
					C.Cast (C.attachedRigidbody.velocity - myCollider.attachedRigidbody.velocity, hits, (C.attachedRigidbody.position - myCollider.attachedRigidbody.position).magnitude);
					foreach (RaycastHit2D H in hits) {
						if (H != null && H.collider == myCollider) {
							Debug.DrawRay (myCollider.gameObject.transform.position, C.attachedRigidbody.position - myCollider.attachedRigidbody.position, Color.red);
							float time = (C.attachedRigidbody.position - myCollider.attachedRigidbody.position).magnitude / relativSpeed;
							Vector2 position = (myCollider.attachedRigidbody.velocity * time) + myCollider.attachedRigidbody.position;
							Vector2 vector = (C.attachedRigidbody.velocity - myCollider.attachedRigidbody.velocity).normalized;
							vector = new Vector2 (vector.y, -vector.x);
							vector = Vector2.Dot (myCollider.gameObject.transform.up, vector) >= 0 ? vector : -vector;
							contactInfo.Add (new ContactInfo (position, time, vector));
							break;
						}
					}
				}
			}
		}
		Vector2 deltaVector = Vector2.zero;
		foreach (ContactInfo I in contactInfo) {
			deltaVector += I.EvasionVector * Mathf.InverseLerp(7, 2, I.TimeToContact);
			Debug.DrawRay (myCollider.attachedRigidbody.position, I.ContactPosition - myCollider.attachedRigidbody.position, Color.yellow);
			Debug.DrawRay (myCollider.attachedRigidbody.position, I.EvasionVector * I.TimeToContact * 15, Color.green);
		}
		if(contactInfo.Count != 0)
			deltaVector /= contactInfo.Count;
		Debug.DrawRay (myCollider.attachedRigidbody.position, deltaVector, Color.blue);
		Vector2 targetVector = (target.position - myCollider.transform.position).normalized;
		if (deltaVector != Vector2.zero)
			targetVector = deltaVector;
//		Quaternion rotateQuaternion = Quaternion.LookRotation(targetVector);
//		float angle = Quaternion.Angle(transform.rotation, rotateQuaternion);
//		transform.rotation = Quaternion.Slerp(
//			transform.rotation,
//			rotateQuaternion,
//			Time.deltaTime * rotationSpeed / angle);

		float rotation_z = Mathf.Atan2(-targetVector.x, targetVector.y) * Mathf.Rad2Deg;
		if (rotation_z < 0)
			rotation_z += 360;
		float deltaAngteA = rotation_z - transform.eulerAngles.z;
		float deltaAngteB = Mathf.Abs(deltaAngteA) - 360;
		if (deltaAngteA < 0)
			deltaAngteB = Mathf.Abs(deltaAngteB);
		float deltaAngte = Mathf.Abs (deltaAngteA) < Mathf.Abs (deltaAngteB) ? deltaAngteA : deltaAngteB;
		float angleRotate = Mathf.Min (Mathf.Abs(deltaAngte), rotationSpeed * TimeManager.LevelFrameTime);
		transform.rotation = Quaternion.Euler(0f, 0f, transform.eulerAngles.z + (angleRotate * Mathf.Sign(deltaAngte)));    

		myCollider.attachedRigidbody.AddForce (myCollider.transform.up * 100);
//		velosity = Vector2.Dot(myCollider.attachedRigidbody.velocity, new Vector2(3, 1).normalized);
//		velosity = new Vector2 (Mathf.Max (relativVector.x, 0), Mathf.Max (relativVector.y, 0)).magnitude;
	}
}

[System.Serializable]
public class ContactInfo
{
	public Vector2 ContactPosition;
	public float TimeToContact;
	public Vector2 EvasionVector;

	public ContactInfo (Vector2 contactPosition, float timeToContact, Vector2 evasionVector)
	{
		ContactPosition = contactPosition;
		TimeToContact = timeToContact;
		EvasionVector = evasionVector;
	}
}


