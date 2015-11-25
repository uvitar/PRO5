﻿using UnityEngine;
using System.Collections;

public class PlattformBehavior : MonoBehaviour {

    Vector3 startPosition;
    public float speed = 3;
    bool onObject;
    double DB;
    double frequ;
    Vector3 moveTo;
    GameObject next;
    int counter = 0;
    int first = 1;
    bool choosen = false;
    bool visited = false;
	GameObject Player;

    // Use this for initialization
    void Start() {

        startPosition = transform.position;
		Player = GameObject.Find ("Player");

    }

    // Update is called once per frame
    void Update() {

		DB = GameObject.Find ("Audio Source").GetComponent<InputAnalyser> ().MicLoudness;
		frequ = GameObject.Find ("Audio Source").GetComponent<InputAnalyser> ().getPitch ();
        Debug.Log(frequ);
        isVisited();

        if (first == 1 && !GameObject.Find("Plattform1").GetComponent<PlattformBehavior>().onPlayer())
        {
            startGame();
        }
        else
        {
            first++;
            if (onPlayer())
            {
                moveTower();
            }
            else if (!onPlayer() && visited)
            {
                Stop(8f);
            }
            else
            {
                if (!onPlayer())
                {
                    //moveRandomTower();
                }
            }
        }
        
    }

    void OnCollisionStay(Collision col)
    {
        if (col.gameObject.name == "Player")
        {
            onObject = true;
        }
        else
        {
            onObject = false;
        }
    }

    bool onPlayer()
    {
        Vector3 tower = transform.position;

		if(Player.transform.position.x == tower.x)//Player.transform.position.x >= tower.x-1 && Player.transform.position.x <= tower.x + 1)
        {
            //Debug.Log("On");
            return true;
        }
        else
        {
            //Debug.Log("Off");
            return false;
        }
    }

    void moveTower()
    {
        if ((int)transform.position.y == (int)GameObject.Find(getNext()).transform.position.y)
        {
            //Debug.Log("Hallo");
            this.choosen = true;
            moveCharakter();
        }
        else if(transform.position.y > 3.10 && transform.position.y < 3.15 && name == "Plattform10")
        {
            endGame();
        }
            else
            {
                if (frequ >= 500 )
                {
                    //Debug.Log("UP");
                    moveUp();
                }
                else
                {
                    //Debug.Log("DOWN");
                    moveDown();
                }
            }
    }

    void moveRandomTower()
    {
        if (counter == 0 || counter == 50) {
            counter = 0;
            moveTo = new Vector3(transform.position.x, Random.Range(-6F, 8F), transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, moveTo, 5 * Time.deltaTime);
            counter++;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, moveTo, 5 * Time.deltaTime);
            counter++;
        }
    }

    //moves the charakter
    void moveCharakter()
    {
        int temp = 0;
        Vector3 tempPlayer = Player.transform.position;
        Vector3 tempNewPos = new Vector3(GameObject.Find(getNext()).transform.position.x, tempPlayer.y, GameObject.Find(getNext()).transform.position.z);

        freeze(GameObject.Find(getNext()));
        GameObject.Find("Player").transform.position = Vector3.MoveTowards(tempPlayer, tempNewPos, 1 * Time.deltaTime);
        /*while (temp < 5000)
        {
            temp++;
        }    */    
    }

    //Moves tower up
    void moveUp()
    {
        if (startPosition.y < 8)
        {
            startPosition.y += 1;
            transform.position = Vector3.MoveTowards(transform.position, startPosition, speed * Time.deltaTime);
            updatePlayer(1);
        }
    }

    //moves tower down
    void moveDown()
    {
        if (startPosition.y > -6)
        {
            startPosition.y -= 1;
            transform.position = Vector3.MoveTowards(transform.position, startPosition, speed * Time.deltaTime);
            updatePlayer(-1);
        }
    }

    //get the next tower name
    string getNext()
    {
        string sillyMeme = name;

        char temp;

        temp = sillyMeme[9];
        int bar = temp - '0';
        bar++;

        if (bar == 11)
        {
            return "Plattform10";
        }


        return "Plattform" + bar;
    }

    private void freeze(GameObject tower)
    {
        //Debug.Log("Fahren");
        if (choosen) {
            float y = transform.position.y;
            tower.transform.position = Vector3.MoveTowards(tower.transform.position, tower.transform.position, speed * Time.deltaTime);
        }
  
    }

    private void Stop(float x)
    {
        if (transform.position.y != 8) {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, x, transform.position.z), speed * Time.deltaTime);
        } 
    }

    private void isVisited()
    {
        Vector3 tower = transform.position;
        Vector3 player = GameObject.Find("Player").transform.position;

        if (player.x > tower.x + 1.5)
        {
            //Debug.Log(name +" Visited");
            visited = true;
        }
        else
        {
            visited = false;
        }
    }

	private void  startGame()
    {
        Vector3 tempPlayer = Player.transform.position;
		Vector3 tempNewPos = new Vector3(GameObject.Find("Plattform1").transform.position.x, tempPlayer.y, GameObject.Find("Plattform1").transform.position.z);

        freeze(GameObject.Find("Plattform1"));
		Player.transform.position = Vector3.MoveTowards(tempPlayer, tempNewPos, 1 * Time.deltaTime);
		//yield return new WaitForSeconds(5);
    }

    private void endGame()
    {
        Stop(transform.position.y);
        Vector3 tempPlayer = GameObject.Find("Player").transform.position;
        Vector3 tempNewPos = new Vector3(GameObject.Find("End").transform.position.x, tempPlayer.y, GameObject.Find("End").transform.position.z);
        GameObject.Find("Player").transform.position = Vector3.MoveTowards(tempPlayer, tempNewPos, 3 * Time.deltaTime);
        isVisited();
    }

    private void updatePlayer(int x)
    {
        Vector3 tower = transform.position;
        GameObject player = GameObject.Find("Player");
        player.transform.position = Vector3.MoveTowards(player.transform.position, new Vector3(tower.x, player.transform.position.y+x, tower.z), 10 *Time.deltaTime);
    }
}



