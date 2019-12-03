using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyManager : MonoBehaviour {

	public List<EnemyController> enemies { get; private set; }
	public List<EnemyController> switchables { get; private set; }
	public int numberOfEnemiesTotal { get; private set; }
	public int numberOfEnemiesRemaining => enemies.Count;

	public UnityAction<EnemyController, int> onRemoveEnemy;

	private void Awake() {
		switchables = new List<EnemyController>();
		enemies = new List<EnemyController>();
	}

	public void RegisterEnemy(EnemyController enemy) {
		if (enemy.gameObject.tag == "Switchable") {
			switchables.Add(enemy);
		}
		enemies.Add(enemy);

		numberOfEnemiesTotal++;
	}

	public void UnregisterEnemy(EnemyController enemyKilled) {
		int enemiesRemainingNotification = numberOfEnemiesRemaining - 1;

		onRemoveEnemy.Invoke(enemyKilled, enemiesRemainingNotification);

		// removes the enemy from the list, so that we can keep track of how many are left on the map
		switchables.Remove(enemyKilled);
		enemies.Remove(enemyKilled);
	}
}
