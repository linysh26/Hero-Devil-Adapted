using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ActionManager : MonoBehaviour {

	private Dictionary<int, Action> actions = new Dictionary<int, Action> ();
	List<Action> waitingAdd = new List<Action>();
	List<int> waitingDelete = new List<int>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	protected void Update () {
		foreach (Action action in waitingAdd) {
			actions [action.GetInstanceID()] = action;
		}
		waitingAdd.Clear ();

		foreach (KeyValuePair<int, Action> key in actions) {
			Action action = key.Value;
			if (action.destroy) {
				waitingDelete.Add (key.Key);
			} else if (action.enable) {
				action.Update ();
			}
		}
		foreach (int key in waitingDelete) {
			Action action = actions [key];
			waitingDelete.Remove (key);
			DestroyObject (action);
		}
		waitingDelete.Clear ();
	}

	public void RunAction(GameObject cast, Action action, ActionCallBack1 callback){
		Debug.Log ("Start!");
		action.gameobject = cast;
		action.callback = callback;
		waitingAdd.Add (action);
		action.Start ();
	}
}
