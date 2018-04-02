using UnityEngine;
using System.Collections;

public class boat_action_controller : MonoBehaviour {

	GameObject boat;
	bool isMoving;
	float speed;
	int rest;
	Transform[] seats;
	int[] seats_status;
	Vector3 destination;
	Vector3 origin;

	int current_coast;
	coast_action_controller []coasts;

	public boat_action_controller (coast_action_controller[] coasts) {
		boat = Instantiate (Resources.Load ("boat"), new Vector3 (-2, 0, 0), Quaternion.identity) as GameObject;
		boat.GetComponent<boat_action> ().setBoatActionController (this);
		isMoving = false;
		rest = 2;
		speed = 2;
		seats = boat.GetComponentsInChildren<Transform> ();
		this.coasts = coasts;
		current_coast = 0;
		seats_status = new int[2];
		seats_status [0] = 0;
		seats_status [1] = 0;
		destination = new Vector3 (2, 0, 0);
		origin = boat.transform.position;
	}

	public GameObject getBoat(){
		return this.boat;
	}

	public bool getStatus(){
		return isMoving;
	}

	public int getCurrentCoast(){
		return current_coast;
	}

	public bool isBoatAvailable(){
		return isMoving;
	}

	public void Move (){
		if (isMoving) {
			float step = Mathf.Min(speed * Time.deltaTime, Mathf.Abs(destination.x - boat.transform.position.x));
			boat.transform.position = Vector3.MoveTowards (boat.transform.position, destination, step);
			if (boat.transform.position == destination) {
				isMoving = false;
				character_action_controller.setIsClickAvailable (true);
				destination = origin;
				origin = boat.transform.position;
				current_coast = 1 - current_coast;
				Debug.Log ("current coast: " + current_coast);
			}
		}
	}

	public void clickActionHandler(){
		Debug.Log (2);
		if (isMoving)
			return;
		Debug.Log (3);
		isMoving = true;
		character_action_controller.setIsClickAvailable (false);
	}

	public void putCharacter(character_action_controller character){
		if (rest == 2)
			return;
		else {
			Debug.Log (character.getSeatNumber ());
			this.seats_status [character.getSeatNumber ()] = 0;
			coasts[current_coast].getOnCoast (character);
			character.setOnBoat (false);
			rest++;
		}
	}

	public void takeCharacter(character_action_controller character){
		seats = boat.GetComponentsInChildren<Transform> ();
		if (rest == 0)
			return;
		else {
			coasts[current_coast].getOffCoast (character);
			int seat_number = seats_status [0] == 0 ? 0 : 1;
			character.setSeatNumber (seat_number);
			character.setPosition (seats[seat_number + 1].position);
			character.getCharacter().transform.parent = boat.transform;
			character.setOnBoat (true);
			seats_status [seat_number] = 1;
			rest--;
		}
	}

	public void Restart(){
		boat.transform.position = new Vector3 (-2, 0, 0);
		rest = 2;
		seats_status [0] = 0;
		seats_status [1] = 0;
		destination = new Vector3 (2, 0, 0);
		origin = boat.transform.position;
		current_coast = 0;
		isMoving = false;
	}
}
