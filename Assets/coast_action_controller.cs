using UnityEngine;
using System.Collections;

public class coast_action_controller : MonoBehaviour {
	
	GameObject coast;
	int coast_number;
	Vector3[] seats;
	int[] seats_status;
	int devil_counter;
	int hero_counter;


	// Use this for initialization
	public coast_action_controller (Vector3 position, int coast_number) {
		this.devil_counter = 0;
		this.hero_counter = 0;
		this.coast_number = coast_number;
		coast = Instantiate (Resources.Load ("coast"), position, Quaternion.identity) as GameObject;
		seats_status = new int[6];
		seats = new Vector3[6];
		for (int i = 0; i < 6; i++) {
			seats [i] = new Vector3 (position.x + (float)(i - 2.5), 1, 0);
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
		return hero_counter >= devil_counter && devil_counter != 0;
	}

	public void getOffCoast(character_action_controller character){
		seats_status [character.getSeatNumber()] = 0;
		if (character.getCharacterType () == 0)
			devil_counter--;
		else
			hero_counter--;
	}

	public void getOnCoast(character_action_controller character){
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

	int getEmptySeatNumber(){
		for(int i = 0;i < 6;i++){
			if(seats_status[i] == 0)
				return i;
		}
		return -1;
	}

	public void Restart(){
		for (int i = 0; i < 6; i++) {
			seats_status [i] = 0;
		}
		hero_counter = 0;
		devil_counter = 0;
	}
}
