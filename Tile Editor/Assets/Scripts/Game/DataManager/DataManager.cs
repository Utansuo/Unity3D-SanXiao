using UnityEngine;
using System.Collections;
using JsonFx.Json;
using System;
using System.Collections.Generic;
public class DataManager : MonoBehaviour {

	public Friends friends;
	public Dictionary<UIPANEL,string> uiDic;
	public GameData gameData;
	// Use this for initialization
	
	void Awake()
	{
		InitData();
	}
	
	void Start () {

	}
	
	void InitData()
	{
		
		gameData = new GameData(Screen.width,Screen.height);
		
		uiDic = new Dictionary<UIPANEL, string>();
		TextAsset uiText = Resources.Load("Txt/UIPrefabPath") as TextAsset;
		string[] uiPath = uiText.text.Split('\n');
		Type week=typeof(UIPANEL);
		Array Arrays = Enum.GetValues(week);
		for(int i=0;i<Arrays.LongLength;i++)
		{
			uiDic.Add((UIPANEL)Arrays.GetValue(i),uiPath[i]);
		}
		print(uiDic[UIPANEL.UIPANEL_FriendsList]);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void  GetFriendsCallBack(string friendsInfo)
	{
			friends = JsonReader.Deserialize<Friends>(friendsInfo);
		    print(friends.users[0].name);
	}
	
}
