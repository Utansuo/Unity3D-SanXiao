
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameData  {
	
	public int[,] mapInfo;
	//public Dictionary<string,TileObj> objDic = null;
    public Dictionary<string,TileObj> objDic;
	
	private int width;
	private int height;
	private int spriteWidth = 50;
	private int spriteHeight = 50;
	private int row;
	private int column;
	private int offSetX;
	private int mapWidthMax;
	private int mapHeightMax;
	public GameData(int screenWidth,int screenHeight)
	{
		width = screenWidth;
		height = screenHeight;
		
		row = height/spriteHeight/3*2;
		column = width/spriteWidth;
		offSetX = Mathf.CeilToInt(width%spriteWidth/2);
		mapWidthMax = column*spriteWidth - offSetX;
		mapHeightMax = row*spriteHeight;
		objDic = new Dictionary<string,TileObj>(); 
		mapInfo = new int[row,column];
		InitGameData();
	}
	
	public int Row
	{
		get{return row;}
	}
	
	public int Column
	{
		get{return column;}
	}
	
	public int OffSetX
	{
		get{return offSetX;}
	}
	
	public int MapWidthMax
	{
		get{return mapWidthMax;}
	}
	
	public int MapHeightMax
	{
		get{return mapHeightMax;}
	}
	
	public int SpriteWidth
	{
		get{return spriteWidth;}
	}
	
	public int SpriteHeight
	{
		get{return spriteHeight;}
	}
	
	public void InitGameData()
	{
		for(int i = 0;i<row;i++)
		{
			for(int j = 0;j<column;j++)
			{
				int random = UnityEngine.Random.Range(1,7);
				mapInfo[i,j] = random;
				TileObj tileObj = new TileObj();
				tileObj.x = (j+1/2f)*spriteWidth-width/2+offSetX; //(j+1/2f)*tileWidth-Screen.width/2  
				tileObj.y = (i+1/2f)*spriteHeight-height/2f;
				objDic.Add(i+","+j,tileObj);
			}
		}
	}
	
}

public class TileObj
{
	public float x;
	public float y;
	public GameObject obj;
}


