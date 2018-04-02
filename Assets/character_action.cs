using UnityEngine;
using System.Collections;

public class character_action : MonoBehaviour {

	character_action_controller cac;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseUp(){
		Debug.Log ("click!");
		cac.clickActionHandler ();
	}

	public void setCharacterActionController(character_action_controller cac){
		this.cac = cac;
	}
}
