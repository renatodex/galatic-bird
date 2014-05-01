using UnityEngine;
using System.Collections;

public class SpawnerController : MonoBehaviour {

	public GameObject[] obstacles;

	public void startSpawner() {
		StartCoroutine(waitStart());
	}

	IEnumerator waitStart() {
		while(true) {
			if(FlagStartGame.Instance.isFlag()) {
				StartCoroutine(startSpawning());
				yield break;
			}
			yield return null;
		}
	}

	IEnumerator startSpawning() {
		while(true) {
			if(!FlagStartGame.Instance.isFlag()) {
				yield break;
			}
			spawnBlock();
			yield return new WaitForSeconds(1.2f);
			yield return null;
		}
	}

	void spawnBlock() {
		GameObject obstacle = (GameObject)Instantiate(obstacles[Random.Range(0, this.obstacles.Length)]);
		obstacle.transform.position = transform.position;
	}
}
