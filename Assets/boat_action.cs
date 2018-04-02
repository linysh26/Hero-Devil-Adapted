using UnityEngine;
using System.Collections;

public class boat_action : MonoBehaviour {

	boat_action_controller bac;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		bac.Move ();
	}

	void OnMouseUp(){
		Debug.Log (1);
		bac.clickActionHandler ();
	}

	public void setBoatActionController(boat_action_controller bac){
		this.bac = bac;
	}
}
