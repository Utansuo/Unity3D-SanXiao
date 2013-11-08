using UnityEngine;
using System.Collections;

public class RequestCallback : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void OnRequestSuccess(string funName, string result)
	{
		switch(funName)
		{
				case " funName":GameInitCallBack(result); break;
		}
	}
	
	public void OnRequestFailed(string funName)
	{
		Debug.Log(funName +"Faile");
	}
	
	
	
	void GameInitCallBack(string result)
	{
		
	}
}
