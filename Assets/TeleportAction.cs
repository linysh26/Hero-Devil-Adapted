using UnityEngine;
using System.Collections;

public class TeleportAction : Action {

	public Vector3 target;

	public static TeleportAction GetAction(Vector3 target){
		TeleportAction action = ScriptableObject.CreateInstance<TeleportAction> ();
		action.target = target;
		return action;
	}

	// Use this for initialization
	public override void Start () {
		Debug.Log ("Teleport!");
		this.gameobject.transform.position = target;
		destroy = true;
	}

	public override void Update(){
		throw new System.NotImplementedException ();
	}
}
