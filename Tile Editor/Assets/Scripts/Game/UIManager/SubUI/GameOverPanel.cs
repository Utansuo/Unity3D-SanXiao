using UnityEngine;
using System.Collections;

public class GameOverPanel : BaseUI {
	
	public UILabel totalPoint;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnEnable()
	{
		totalPoint.text = ""+Game.dataManager.playerData.GetTotalPoints;
	}
	
	void Reset()
	{
		base.Close();
		Game.uimanager.startPanel.gameObject.SetActive(true);
		
	}
}
