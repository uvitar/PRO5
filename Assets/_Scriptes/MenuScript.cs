﻿using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void onStartGame() {
		GameLogic.Instance.startNewDemoGame ();
	}

	public void onTreeSawing() {
		GameLogic.Instance.startGameWithLevel ("TreeSawing");
	}

	public void onTennis() {
		GameLogic.Instance.startGameWithLevel ("Tennis");
	}

	public void onFlappyScream() {
		GameLogic.Instance.startGameWithLevel ("FlappyScream");
	}

	public void onRoad_Scene() {
		GameLogic.Instance.startGameWithLevel ("Road_Scene");
	}

	public void onPlattform_Szene() {
		GameLogic.Instance.startGameWithLevel ("Plattform_Szene");
	}

	public void onTod_Szene_Spiel() {
		GameLogic.Instance.startGameWithLevel ("Tod-Szene-Spiel");
	}

	public void onJumpAndDuck() {
		GameLogic.Instance.startGameWithLevel ("JumpAndDuck");
	}

	public void onMainMenu() {
		// load a new scene, but do not start the new game yet
		GameLogic.Instance.loadFirstLevelOnHold();
	}
}
