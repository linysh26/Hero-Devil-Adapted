using UnityEngine;
using System.Collections;

public class Action : ScriptableObject {

	public bool enable;
	public bool destroy;

	public GameObject gameobject{ get; set; }
	public Transform transform { get; set; }
	public ActionCallBack1 callback{ get; set; }

	protected Action() {}

	// Use this for initialization
	public virtual void Start () {
		throw new System.NotImplementedException();
	}
	
	// Update is called once per frame
	public virtual void Update () {
		throw new System.NotImplementedException();
	}
}
