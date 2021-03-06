using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerTurn : MonoBehaviour
{

	bool oldState;
	bool isActive;
	private enermiesTurn turn;
    public KeyCode keyStartMove = KeyCode.Q;
    public KeyCode keyStartAttack = KeyCode.F;
    private static DieLauncher diceGun;
    int action;//0:move,1:fight
	movePlayer mover;
	playerFight fighter;
	public float actionsLeft;
	bool waitingForRoll;
	public Transform hand;
	//public Vector3 handPos;
	//public float handRot;
	//public Quaternion handRot;
	//public Vector3 handRotVec;
	public GameObject holdingWepn;


	//player
	void Start()
	{

		//handPos = new Vector3(0.3f, 0, 0);
		//handRot = 45;
		//handRot = Quaternion.Euler(0, 0, 45);
		//handRotVec = new Vector3(1, 1, 0);

		hand = transform.Find("hand");//GameObject.Find("Dice Gun");

		waitingForRoll = false;
		mover = GetComponent<movePlayer>();
		fighter = GetComponent<playerFight>();
		diceGun = GameObject.Find("Dice Gun").GetComponent<DieLauncher>();
		turn = GameObject.Find("enermies1").GetComponent<enermiesTurn>();

	}
	void FixedUpdate(){
		bool newState=turn.startPLayerTurn;
		if(newState&&newState!=oldState){
			generateActions();
		}
		if(isActive){
			turn.numOfActiveObjs++;
			if(!(action==0?mover.isActive:fighter.isFighting)){
				if(actionsLeft>0)nextAction();
				else isActive=false;
			}
		}
		oldState=newState;
	}
	void generateActions(){
		waitingForRoll=true;

		diceGun.rollDice();
	}
	void nextAction(){
		action=0;
		if(action==0)mover.StartMovement();
		else fighter.startFight();
		actionsLeft--;
		//isActive=true;
	}
	void Update(){

        if (transform.position.y < -25)
        {
			transform.position = new Vector3(-2.6f, 2.5f, 0);
			GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);

		}

		if(waitingForRoll && diceGun.diceOutput.Count > 0)
		{
			waitingForRoll=false;
			isActive = true;
			actionsLeft = diceGun.diceOutput[0];
			diceGun.diceOutput.RemoveAt(0);
		}
		/*else{
			if(isActive){
		        if (Input.GetKeyDown(keyStartMove))
		        {
		        	action=0;
		            mover.StartMovement();
		        }
		        else if(Input.GetKeyDown(keyStartAttack)){
		            mover.StopMovement();
		            mover.isMoving=false;
		        }
		    }
		}*/

		if (Input.GetKeyDown(KeyCode.Q))
		{
			holdingWepn.GetComponent<WeaponHandler>().dropWeapon();
		}

	}

}
