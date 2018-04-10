using UnityEngine;
using System.Collections;

/**
 * the abstract data structure of a boat
 * 
*/

public class Boat: MonoBehaviour {

	GameObject boat;

	bool isMoving;
	public float speed;
	int rest;
	Transform[] seats;
	int[] seats_status;

	public Boat () {
		boat = Instantiate (Resources.Load ("FreeBarrowsWagons/prefabs/wagon2"), new Vector3 ((float)-0.5, 0, 0), Quaternion.identity) as GameObject;
		isMoving = false;
		rest = 2;
		speed = 2;
		seats = boat.GetComponentsInChildren<Transform> ();
		seats_status = new int[2];
		seats_status [0] = 0;
		seats_status [1] = 0;
	}

	public GameObject getBoat(){
		return this.boat;
	}

	public bool getStatus(){
		return isMoving;
	}

	public void setStatus(bool status){
		this.isMoving = status;
	}

	public Vector3 getAvailableSeat(){
		int seat_number = seats_status [0] == 0 ? 0 : 1;
		return seats[seat_number + 1].position;
	}

	public bool isBoatAvailable(){
		return rest != 0;
	}

	public void clickActionHandler(){
		if (isMoving)
			return;
		isMoving = true;
		Character.setIsClickAvailable (false);
	}

	public void putCharacter(Character character){
		if (rest == 2)
			return;
		else {
			this.seats_status [character.getSeatNumber ()] = 0;
			character.setOnBoat (false);
			rest++;
		}
	}

	public void takeCharacter(Character character){
		seats = boat.GetComponentsInChildren<Transform> ();
		if (rest == 0)
			return;
		else {
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
		isMoving = false;
	}
}
