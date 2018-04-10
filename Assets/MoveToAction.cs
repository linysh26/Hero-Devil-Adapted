using UnityEngine;
using System.Collections;

public class MoveToAction : Action{

	FirstController scene_controller;
	public float speed;
	public Vector3 target;

	public static MoveToAction GetAction(Vector3 target, float speed){
		MoveToAction action = ScriptableObject.CreateInstance<MoveToAction> ();
		action.target = target;
		action.speed = speed;
		return action;
	}

	// Use this for initialization
	public override void Start () {
		scene_controller = (FirstController)Director.getInstance ().currentSceneController;
		scene_controller.boat.setStatus (true);
		Animator anima = gameobject.GetComponent<Animator> ();
		anima.SetInteger ("AnimState", 0);
	}
	
	// Update is called once per frame
	public override void Update () {Debug.Log (this.gameobject.name);
		if (enable) {
			Debug.Log ("Moving...");
			float step = Mathf.Min (speed * Time.deltaTime, Mathf.Abs (target.x - this.gameobject.transform.position.x));
			this.gameobject.transform.position = Vector3.MoveTowards (this.gameobject.transform.position, target, step);
			if (this.gameobject.transform.position == target) {
				destroy = true;
				scene_controller.boat.setStatus (false);
				scene_controller.setCurrentCoast (1 - scene_controller.getCurrentCoast ());
			}
		}
	}
}
