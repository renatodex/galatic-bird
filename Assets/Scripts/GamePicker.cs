using UnityEngine;
using System.Collections;

public static class GamePicker
{
	public static GameObject[] getObstacles() {
		return GameObject.FindGameObjectsWithTag("Obstacle");
	}
}

