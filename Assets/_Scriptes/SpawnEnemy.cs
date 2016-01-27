﻿using UnityEngine;
using System.Collections;

public class SpawnEnemy : MonoBehaviour {

    public bool randomSpawn = false;
    Vector3 playerPosition;
    Vector3 startPosition;
    Vector3 position;
    float speed = 1;
	float distance = 0;
	float count;
	bool visible = true;
	GameObject enem;
	public Collider objColl;
	private Camera cam;
	private Plane[] planes;
	private Vector3[] spawnPoint;
	public int limit = 300;
    bool levelDidStart = false;
    bool stop = false;

    // Use this for initialization
    void Start () {

		this.levelDidStart = GameLogic.Instance.getLevelIsReadyToStart ();
		this.speed = this.speed * GameLogic.Instance.getLevelSpeed ();

		count = 0;
		spawnPoint = new Vector3[5];
		spawnPoint [0] = new Vector3 (21.9f,3f,-18f);
		spawnPoint [1] = new Vector3 (21.9f,3f,-24f);
		spawnPoint [2] = new Vector3 (-15f,3f,-24f);
		spawnPoint [3] = new Vector3 (26f,3f,-7f);
		spawnPoint [4] = new Vector3 (-14f,3f,-29f);

		position = spawnPoint [Random.Range(0,spawnPoint.Length)];
        startPosition = this.transform.position;
		playerPosition = GameObject.Find("Player").transform.position;
        enem = GameObject.Find ("Enemy");

		cam = Camera.main;
		planes = GeometryUtility.CalculateFrustumPlanes(cam);
		objColl = this.GetComponent<Collider>();

        if (randomSpawn)
        {
            this.transform.position = position;
        }

        setSpeed();
	}

    void OnEnable()
    {
        GameLogic.Instance.OnLevelReadyToStart += levelReadyToStart;
    }

    // Unregister Broadcast "OnLevelReadyToStart" event
    void OnDisable()
    {
        GameLogic.Instance.OnLevelReadyToStart -= levelReadyToStart;
    }

    // receives OnLevelReadyToStart events
    private void levelReadyToStart()
    {
        this.levelDidStart = true;
    }

    // Update is called once per frame
    void Update () {

        if (levelDidStart)
        {
            float tempDB = AudioAnalyzer.Instance.getMicLoudness();
            distance = Vector3.Distance(playerPosition, transform.position);
            rotateChar();

            if (GeometryUtility.TestPlanesAABB(planes, objColl.bounds))
                visible = true;
            else {
                visible = false;
                count++;
            }

            //Debug.Log (tempDB);
            isWon();

            if (!stop)
            {
                if (count > limit)
                {
                    attack();
                    count = 0;
                }
                else {
                    if (tempDB > -5)
                    {
                        moveAway();
                    }
                    else {
                        this.speed += 0.1f;
                        this.transform.position = Vector3.MoveTowards(this.transform.position, playerPosition, speed * Time.deltaTime);
                    }
                }
            }
        }
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Player")
        {
            stop = true;
		    endGame();
            GetComponent<Rigidbody>().mass = 100;
        }
    }

    void moveAway()
    {
		Vector3 player = GameObject.Find ("Player").transform.position - transform.position; 
		player = player.normalized;
			
		Vector3 temp = +1 * player;
		Vector3 goal = position;
		goal += temp;
		goal.y = 3;

		transform.position = Vector3.MoveTowards(transform.position, position+goal , 10 * Time.deltaTime);
    }

	void attack(){

        this.transform.position = spawnPoint [Random.Range(0,spawnPoint.Length)];
		GameObject clone = Instantiate(Resources.Load("Enemy" + Random.Range(1, 3)), new Vector3(21.9f, 3f, -18f), transform.rotation) as GameObject;
        Destroy (GameObject.Find ("Enemy"));
		clone.name = "Enemy";

	}

	void endGame(){
        GameLogic.Instance.didFailLevel();
	}

	void Awake(){
		AudioAnalyzer.Instance.Init ();
	}

    void isWon()
    {
        stop = !GameLogic.Instance.getIsLevelActive();
    }

    void rotateChar()
    {
        var targetPos = playerPosition;
        targetPos.y = this.transform.position.y; //set targetPos y equal to mine, so I only look at my own plane
        var targetDir = Quaternion.LookRotation(targetPos-this.transform.position);
        this.transform.rotation = Quaternion.Slerp(transform.rotation, targetDir, 6 * Time.deltaTime);
    }

    void setSpeed()
    {
        float temp = GameLogic.Instance.getLevelSpeed();

        this.speed = temp;
    }
}


