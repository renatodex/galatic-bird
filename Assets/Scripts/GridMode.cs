using UnityEngine;
using System.Collections;
//using UnityEditor;

[ExecuteInEditMode]
public class GridMode : MonoBehaviour {
	public float snapValue = 1;
	//public float depth = 0;    
	
	void Update() {
		if (Application.isPlaying) return;

		float snapInverse = 1/snapValue;
		
		float x, y, z;

		x = Mathf.Round(transform.position.x * snapInverse)/snapInverse;
		y = Mathf.Round(transform.position.y * snapInverse)/snapInverse;   
		z = transform.position.z;
		
		transform.position = new Vector3(x, y, z);
	}
}