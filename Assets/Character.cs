using UnityEngine;
using System.Collections;


/**
 * the abstract data structure of a character
 * 
*/
public class Character: MonoBehaviour{


	private static bool isMoving;
	GameObject character;
	Vector3 current_position;
	int character_type;
	int seat_number;
	int coast_number;
	int character_number;
	bool isOnBoat;

	public Character(int type, int character_number){
		this.character_type = type;
		this.coast_number = type;
		this.character_number = character_number;
		this.character = Instantiate (Resources.Load (type == 0 ? "devil" : "hero")) as GameObject;
		isMoving = false;
		isOnBoat = false;
	}
	/*-------------------------------------------------------fundamental function ------------------------------------------------------------------*/
	public void setCharacterType(int type){
		this.character_type = type;
	}

	public int getCharacterType(){
		return this.character_type;
	}

	public void setCharacterNumber(int number){
		this.character_number = number;
	}

	public int getCharacterNumber(){
		return this.character_number;
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

	public bool getOnBoat(){
		return this.isOnBoat;
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
}