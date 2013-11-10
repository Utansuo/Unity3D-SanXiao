using UnityEngine;
using System.Collections;

public class PlayerData  {

	private int totalPoints;
	public void Add(int point)
	{
		totalPoints +=point;
	}
	
	public int GetTotalPoints
	{
		get{return totalPoints;}
	}
}
