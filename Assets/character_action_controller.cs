using UnityEngine;
using System.Collections;

public class character_action_controller : MonoBehaviour {


	private static bool isMoving;
	GameObject character;
	Vector3 current_position;
	int character_type;
	int seat_number;
	int coast_number;
	bool isOnBoat;

	boat_action_controller boat;

	public character_action_controller(boat_action_controller boat, int type){
		this.character_type = type;
		this.coast_number = type;
		this.boat = boat;
		this.character = Instantiate (Resources.Load (type == 0 ? "devil" : "hero")) as GameObject;
		Transform[] children = this.character.GetComponentsInChildren<Transform> ();
		children [1].GetComponent<character_action> ().setCharacterActionController (this);
		children [2].GetComponent<character_action> ().setCharacterActionController (this);
		isMoving = false;
		isOnBoat = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
/*-------------------------------------------------------fundamental function ------------------------------------------------------------------*/
	public void setCharacterType(int type){
		this.character_type = type;
	}

	public int getCharacterType(){
		return this.character_type;
	}

	public void setCoastNumber(int coast_number){
		this.coast_number = coast_number;
	}

	public int getCoastNumber(){
		return this.coast_number;
	}

	public void setCharacter(GameObject character){
		this.character = character;
	}

	public GameObject getCharacter(){
		return this.character;
	}

	public void setOnBoat(bool isOnBoat){
		this.isOnBoat = isOnBoat;
	}

	public void setPosition(Vector3 new_position){
		character.transform.position = new_position;
	}

	public Vector3 getPosition(){
		return character.transform.position;
	}

	public void setSeatNumber(int seat_number){
		this.seat_number = seat_number;
	}
	public int getSeatNumber(){
		return this.seat_number;
	}
	public static void setIsClickAvailable(bool is_moving){
		isMoving = !is_moving;
	}
	public static bool isClickAvailable(){
		return isMoving;
	}

	public void setBoatController(boat_action_controller bc){
		this.boat = bc;
	}

/*--------------------------------------------------------character behavior--------------------------------------------------------------------*/

	public void updateStatus(){
		this.isOnBoat = !isOnBoat;
	}

	public void clickActionHandler(){
		Transform parent = character.transform.parent;
		if (character_action_controller.isMoving) {
			Debug.Log ("is Moving");
			return;
		} 
		else if (parent == boat.getBoat ().transform) {
			Debug.Log ("Get back coast");
			boat.putCharacter (this);
		}
		else {
			if (isOnBoat) {
				Debug.Log ("get on coast");
				boat.putCharacter (this);
			}
			else {
				if(boat.getCurrentCoast() != this.coast_number){
					return;
				}
				Debug.Log (character.transform.parent.GetComponent<coast_action_controller> ().getCoastNumber());
				Debug.Log ("get on boat");
				boat.takeCharacter (this);
			}
		}
	}
}