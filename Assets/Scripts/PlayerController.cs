using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private bool _died = false;
	public GameObject _messageGameOver;


	void Start() {
		StartCoroutine(waitForNewGame());
	}

	IEnumerator waitForNewGame() {
		while(true) {
			if(Input.GetKey(KeyCode.Space)) {
				this._died = false;
				this.renderer.enabled = true;
				Debug.Log ("NOW!");
				FlagStartGame.Instance.setFlag(true);

				GameObject.Destroy(GameObject.FindGameObjectWithTag("Message"));
		
				this.rigidbody2D.isKinematic = false;

				GameObject.FindGameObjectWithTag("Spawner").GetComponent<SpawnerController>().startSpawner();

				yield break;
			}

			yield return null;
		}
	}


	void FixedUpdate() {
		if(FlagStartGame.Instance.isFlag()) {

			if(!this._died) {
				if(Input.GetKey(KeyCode.Space)) {
					rigidbody2D.velocity = new Vector2(0f, 0f);
					rigidbody2D.AddForce(new Vector2(0f, 500f));
				}
		
				float safeDistance = 0.1f;

				Debug.DrawRay(transform.FindChild("RayFront").transform.position, Vector3.right*safeDistance, Color.yellow, 1);
				if(Physics2D.Raycast(transform.FindChild("RayFront").transform.position, Vector3.right, safeDistance)) {
					this.gameOver();
				}

				Debug.DrawRay(transform.FindChild("RayUp").transform.position, Vector3.up*safeDistance, Color.yellow, 1);
				if(Physics2D.Raycast(transform.FindChild("RayUp").transform.position, Vector3.up, safeDistance)) {
					this.gameOver();
				}

				Debug.DrawRay(transform.FindChild("RayDown").transform.position, Vector3.down*safeDistance, Color.yellow, 1);
				if(Physics2D.Raycast(transform.FindChild("RayDown").transform.position, Vector3.down, safeDistance)) {
					this.gameOver();
				}

			}
		}
	}

	void gameOver() {
		StartCoroutine(gameOverEvent());
	}

	IEnumerator gameOverEvent() {
		FlagStartGame.Instance.setFlag(false);
		this._died = true;
		rigidbody2D.isKinematic = true;
		collider2D.enabled = false;
		
		GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
		foreach(GameObject obstacle in obstacles) {
			GameObject.Destroy(obstacle);
		}

		Animator anim = GetComponent<Animator>();
		anim.SetTrigger("die");
		Instantiate(this._messageGameOver);


		yield return new WaitForSeconds(1f);

	
		StartCoroutine(waitForNewGame());

		yield return null;
	}

	public void killPlayer() {
		this.transform.position = GameObject.Find("StartPosition").transform.position;
		this.renderer.enabled = false;
	}


	void OnTriggerEnter2D(Collider2D col) {
		gameOver();
	}

}
