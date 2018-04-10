using UnityEngine;
using System.Collections;



/**
 * Since I was going to let the very object to care about its own deal, so I let the object's controller communicate with the 
 * 
 * action manager instead dealing cases in the SceneController
 * */





public class FirstController : MonoBehaviour, SceneController{

	Director director;

	public Coast[] coasts;
	public Character[] characters;
	public Boat boat;
	GameObject water_surface;

	public bool game;
	bool result;

	int current_coast;

	public int getCurrentCoast(){
		return current_coast;
	}

	public void setCurrentCoast(int current_coast){
		this.current_coast = current_coast;
	}

	public int findCharacterNumber(GameObject character){
		for (int i = 0; i < 6; i++) {
			if (characters [i].getCharacter () == character) {
				return i;
			}
		}
		return -1;
	}


	void Awake(){
		director = Director.getInstance ();
		director.currentSceneController = this;
		director.currentSceneController.LoadResources ();
		game = false;
	}

	void OnGUI(){
		if (coasts [1].getHeroes () == 0 && coasts [1].getDevils () == 3) {
			game = true;
			result = true;
		}
		if (coasts [0].heroKillDevil () || coasts [1].heroKillDevil ()) {
			game = true;
			result = false;
		}
		if (game) {
			GUI.Box (new Rect(Screen.width/2 - 50, Screen.height/2 - 40, 100, 80), result?"yeah!":"shit!");

			if(GUI.Button(new Rect(Screen.width/2 - 30, Screen.height/2 - 20, 60, 40), "Again"))
				Restart();
		}
	}

	public void LoadResources(){
		Debug.Log ("load resources");
		current_coast = 0;
		water_surface = Instantiate (Resources.Load ("water_surface"), new Vector3(-100, 0, -100), Quaternion.identity) as GameObject;
		coasts = new Coast[2];
		coasts [0] = new Coast(new Vector3 (-6, 0, 0), 0);
		coasts [1] = new Coast(new Vector3 (6, 0, 0), 1);
		boat = new Boat ();
		characters = new Character[6];
		for (int i = 0; i < 6; i++) {
			characters [i] = new Character(i % 2, i);
			coasts [i%2].getOnCoast (characters [i]);
		}
	}
	public void Restart(){
		current_coast = 0;
		game = false;
		coasts [0].Restart ();
		coasts [1].Restart ();
		boat.Restart ();
		for (int i = 0; i < 6; i++) {
			coasts [i%2].getOnCoast (characters [i]);
			characters [i].setOnBoat (false);
		}
	}
}
