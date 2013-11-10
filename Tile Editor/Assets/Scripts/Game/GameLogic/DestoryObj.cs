using UnityEngine;
using System.Collections;

public class DestoryObj : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCompleteMoveOver(int s)
	{
		//print("&&&&&&&&&&&&&&&&!!!!!!!!!!!!!!!!!!" +s);
		Destroy(gameObject);
		Game.dataManager.playerData.Add(s);
		Game.uimanager.mainUI.SetTotalPoint();
	}
}
