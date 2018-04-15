using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : StateManager {



	public enum TurretStages // доступные состояния турели
	{
		Idle,
		Active
	}

	public void SearchTarget () // здесь ищем обьекты в зоне действия турели
	{
		
	}

	public void TurretFire () // стреляем в найденные обьекты
	{
		
	}


	void OnTriggerEnter2D(Collider2D other) // реагируем на тригерры в зоне коллайдера
	{
		
		SearchTarget ();
	}

}

[System.Serializable]
public class TargetData
{
	public float AgressionKoef = 0f;
	public BaseGameObject Object;
}
