using UnityEngine;
using System.Collections;

public class FirstController : MonoBehaviour, SceneController, user_action {

	Director director;

	coast_action_controller[] coasts;
	character_action_controller[] characters;
	boat_action_controller boat;
	GameObject water_surface;

	bool game;
	bool result;

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
		water_surface = Instantiate (Resources.Load ("water_surface"), new Vector3(-1,0, -1), Quaternion.identity) as GameObject;
		coasts = new coast_action_controller[2];
		coasts [0] = new coast_action_controller (new Vector3 (-6, 0, 0), 0);
		coasts [1] = new coast_action_controller (new Vector3 (6, 0, 0), 1);
		boat = new boat_action_controller (coasts);
		characters = new character_action_controller[6];
		for (int i = 0; i < 6; i++) {
			characters [i] = new character_action_controller (boat, i % 2);
			coasts [i%2].getOnCoast (characters [i]);
		}
	}
	public void Restart(){
		game = false;
		coasts [0].Restart ();
		coasts [1].Restart ();
		boat.Restart ();
		for (int i = 0; i < 6; i++) {
			coasts [i%2].getOnCoast (characters [i]);
		}
	}

}
