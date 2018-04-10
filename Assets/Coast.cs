using UnityEngine;
using System.Collections;

/**
 * the abstract data structure of a coast
 * 
*/

public class Coast : MonoBehaviour {
	GameObject coast;
	int coast_number;
	Vector3[] seats;
	int[] seats_status;
	int devil_counter;
	int hero_counter;


	// Use this for initialization
	public Coast (Vector3 position, int coast_number) {
		this.devil_counter = 0;
		this.hero_counter = 0;
		this.coast_number = coast_number;
		coast = Instantiate (Resources.Load ("coast"), position, Quaternion.identity) as GameObject;
		seats_status = new int[6];
		seats = new Vector3[6];
		for (int i = 0; i < 6; i++) {
			seats [i] = new Vector3 (position.x + (float)(i - 2.5), (float)1.25, 0);
			seats_status [i] = 0;
		}
	}

	public int getCoastNumber(){
		return this.coast_number;
	}

	public int getHeroes(){
		return hero_counter;
	}

	public int getDevils(){
		return devil_counter;
	}

	public bool heroKillDevil(){
		return hero_counter > devil_counter && devil_counter != 0;
	}

	public void getOffCoast(Character character){
		seats_status [character.getSeatNumber()] = 0;
		if (character.getCharacterType () == 0)
			devil_counter--;
		else
			hero_counter--;
	}


	public void getOnCoast(Character character){
		character.setSeatNumber (getEmptySeatNumber ());
		character.setCoastNumber (this.coast_number);
		int seat_number = character.getSeatNumber ();
		character.setPosition (seats [seat_number]);
		character.getCharacter ().transform.parent = coast.transform;
		Debug.Log ("successfully");
		this.seats_status [seat_number] = 1;
		if (character.getCharacterType () == 0)
			devil_counter++;
		else
			hero_counter++;
	}

	public Vector3 getEmptySeat(){
		return seats[getEmptySeatNumber()];
	}

	public int getEmptySeatNumber(){
		int seat_number = -1;
		for(int i = 0;i < 6;i++){
			if (seats_status [i] == 0)
				seat_number = i;
		}
		return seat_number;
	}

	public void Restart(){
		for (int i = 0; i < 6; i++) {
			seats_status [i] = 0;
		}
		hero_counter = 0;
		devil_counter = 0;
	}
}
