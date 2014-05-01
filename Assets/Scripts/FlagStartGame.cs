using UnityEngine;
using System.Collections;

public class FlagStartGame : Singleton<FlagStartGame> {
	protected FlagStartGame () {}
	
	private bool _state = false;
	
	public void setFlag(bool state) {
		this._state = state;
	}
	
	public bool isFlag() {
		return this._state;
	}
}