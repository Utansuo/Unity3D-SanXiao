using UnityEngine;
using System.Collections;

public class StartPanel : BaseUI {
	
	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void StartGame()
	{
		Game.uimanager.mainUI = Game.uimanager.CreateUIObj<CreateMap>(UIPANEL.MainUI);
		print("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
		Close ();
	}
	
	void EnterOptions()
	{
		Game.uimanager.setPanel = Game.uimanager.CreateUIObj<SetPanel>(UIPANEL.SetPanel);
	}
	
	public override void Close ()
	{
		base.Close ();
	}
}
