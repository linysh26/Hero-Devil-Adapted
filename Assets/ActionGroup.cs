using UnityEngine;
using System.Collections;

public class ActionGroup : ActionManager, ActionCallBack1{

	public FirstController scene_controller;
	public MoveToAction moveToLeft, moveToRight;
	public TeleportAction teleportTo;
	// Use this for initialization
	void Start () {
		scene_controller = (FirstController)Director.getInstance ().currentSceneController;
		moveToLeft = MoveToAction.GetAction (new Vector3 (-1, 0, 0), 1);
		moveToRight = MoveToAction.GetAction (new Vector3 ((float)1.5, 0, 0), 1);
		teleportTo = TeleportAction.GetAction (new Vector3 (0, 0, 0));
	}
	
	// Update is called once per frame
	protected new void Update(){
		if (Input.GetMouseButtonDown (0) && !scene_controller.game) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {
				GameObject action_taker = hit.collider.gameObject;
				Debug.Log (action_taker.name [0]);
				switch (action_taker.name [0]) {
				case 'd':
				case 'h':
					if (scene_controller.boat.getStatus ())
						break;
					int ch_num = scene_controller.findCharacterNumber (action_taker);
					int c_num = scene_controller.getCurrentCoast ();
					if (scene_controller.characters[ch_num].getOnBoat ()) {
						scene_controller.boat.putCharacter (scene_controller.characters [ch_num]);
						teleportTo.target = scene_controller.coasts [c_num].getEmptySeat ();
						teleportTo.enable = true;
						teleportTo.destroy = false;
						scene_controller.coasts [c_num].getOnCoast (scene_controller.characters [ch_num]);
						RunAction (scene_controller.characters [ch_num].getCharacter (), teleportTo, this);
					} else {
						if (scene_controller.characters [ch_num].getCoastNumber () != scene_controller.getCurrentCoast ()
							|| !scene_controller.boat.isBoatAvailable())
							break;
						else {
							scene_controller.coasts [c_num].getOffCoast (scene_controller.characters[ch_num]);
							teleportTo.target = scene_controller.boat.getAvailableSeat ();
							teleportTo.enable = true;
							teleportTo.destroy = false;
							scene_controller.boat.takeCharacter (scene_controller.characters [ch_num]);
							RunAction (scene_controller.characters [ch_num].getCharacter (), teleportTo, this);
						}
					}
					break;
				case 'C':
					int current_coast = scene_controller.getCurrentCoast ();
					if (!scene_controller.boat.getStatus()) {
						Debug.Log ("出航！大海贼时代！");
						if (current_coast == 0) {
							moveToRight.speed = scene_controller.boat.speed;
							moveToRight.enable = true;
							moveToRight.destroy = false;
							RunAction (scene_controller.boat.getBoat (), moveToRight, this);
						} 
						else {
							moveToLeft.speed = scene_controller.boat.speed;
							moveToLeft.enable = true;
							moveToLeft.destroy = false;
							RunAction (scene_controller.boat.getBoat (), moveToLeft, this);
						}
					}
					break;
				default:
					break;
				}
			}
		}
		base.Update ();
	}

	public void ActionEvent(Action source, ActionEventType events = ActionEventType.Completed, int intParam = 0, string strParam = null, Object objectParam = null){
		
	}
}
