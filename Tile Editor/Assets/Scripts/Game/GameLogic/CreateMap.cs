using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
	
public class CreateMap : BaseUI {
	
		public UILabel totalPointLab;
		public UISprite gameOver;
		public Transform tileParent;			
		GameData gameData;
	
		int row;
		int column;
		int offX;
		int[,] mapInfo;
		int totalPoint;
		int curType;
		int f ;
		int moveTileNum;
	
		float mapWidthMax;
		float mapHeightMax;
	
		bool intervalFlag;
		bool dropFlag;
		bool gameOverCheckFlag;
		
		List<List<Vector2>> indexList;
		List<Tile> tile;
		List<Tile> horizontalTile;
	
		Queue<Vector2> tempQueue;
		Queue<Vector2> saveQueue;
	
		Dictionary<string,TileObj> objDic;
	
	   List<GameObject> dataTile;

		Vector2 constVar = new Vector2(-1,-1);

	    private float originalY;
		// Use this for initialization
		void Awake ()
		{
			gameData = Game.dataManager.gameData;
		
			column = gameData.Column;
			row = gameData.Row;
			offX = gameData.OffSetX;
			mapWidthMax = gameData.MapWidthMax;
			mapHeightMax = gameData.MapHeightMax;
		
			
		
			indexList = new List<List<Vector2>>();
			horizontalTile = new List<Tile>();
			tile = new List<Tile>();
		
			tempQueue = new Queue<Vector2>();
			saveQueue = new Queue<Vector2>();
			dataTile = new List<GameObject>();
		    originalY = gameOver.transform.localPosition.y;
		  
		}
	
		void OnEnable()
		{
			gameOver.transform.localPosition =new Vector3(gameOver.transform.localPosition.x,originalY,gameOver.transform.localPosition.z);
			curType = -1;
			intervalFlag = false;
			dropFlag = false;
			gameOverCheckFlag = false;
		    gameData.InitGameData();
			mapInfo = gameData.mapInfo;
		    objDic = gameData.objDic;
			SetTotalPoint();
			StartCoroutine(InitMap());
		}
		
		// Update is called once per frame
		void Update ()
		{
				if(intervalFlag) return;
				if(Input.GetMouseButtonUp(0) )
				{
						if(offX<=Input.mousePosition.x&&Input.mousePosition.x<mapWidthMax&&Input.mousePosition.y>=0&&Input.mousePosition.y<mapHeightMax)
						{
								tempQueue.Clear();
								saveQueue.Clear();
								indexList.Clear();
								tile.Clear();
								horizontalTile.Clear();
								
								Vector2 v2= CalculateCurIndex(Input.mousePosition);
								if(mapInfo[(int)v2.y,(int)v2.x]>0)
								{
										intervalFlag = true;
										CalculateRemoveTile(v2);
								}
								else{
										print("null");
								}
						}
						else{
								print("AAAAAAAAAA");
						}
				}
		}
	
	
		IEnumerator  InitMap()
		{
		       print("dataTile.Count"+ row + "  "+column);
				for(int i=0;i<row;i++)
				{
						for(int j=0;j<column;j++)
						{
							
				                //TileObj tileObj = objDic[i+","+j];
								GameObject go = null;
								if(dataTile.Count<=0)
								{
									go = (GameObject)Instantiate(Resources.Load("Prefabs/Obj/Tile"));
									go.transform.parent = tileParent;
									
									go.transform.localScale =new Vector3(Game.dataManager.gameData.SpriteWidth,Game.dataManager.gameData.SpriteWidth,1);

								}
								else
								{
											go = dataTile[i*column+j];
											go.SetActive(true);
					
								}
								go.GetComponent<UISprite>().spriteName = go.GetComponent<UISprite>().atlas.spriteList[ mapInfo[i,j] - 1 ].name;
								go.transform.localPosition = new Vector3(objDic[i+","+j].x,objDic[i+","+j].y,0);
	
								objDic[i+","+j].obj = go;
						}
						yield return new  WaitForSeconds(0.2f);
				}
				dataTile.Clear();
		}
    Vector2 CalculateCurIndex(Vector2 pos)
	{
		Vector2 vec2;
		vec2.x = Mathf.FloorToInt((pos.x-1-offX)/gameData.SpriteWidth);
		vec2.y = Mathf.FloorToInt((pos.y-1)/gameData.SpriteHeight);
		return vec2;
	} 
	
	void CalculateRemoveTile(Vector2 v2)
	{
		bool flag =false;
		Vector2 up,down,left,right;
		curType = GetTileType(v2);
		tempQueue.Enqueue(v2);
		while(tempQueue.Count>0)
		{
			if(!flag)
			{
				Vector2 v = tempQueue.Dequeue();
				if(!saveQueue.Contains(v))
				{
					saveQueue.Enqueue(v);
				}
				flag = true;
			}
			else
			{
				v2 = tempQueue.Dequeue();
				if(!saveQueue.Contains(v2))
				{
					saveQueue.Enqueue(v2);
				}
			}
			CheckTileInfo(v2,out up,out down,out left,out right);
			
			AddSameTile(up,"Up");
			AddSameTile(down,"Down");
			AddSameTile(left,"Left");
			AddSameTile(right,"Right");
		}
		
		//print("saveQueue: " + saveQueue.Count);
		int count = saveQueue.Count;
		if(count<2) 
		{
			intervalFlag = false;
			return;
		}
		Vector2[] vIndex = saveQueue.ToArray();
		List<Vector2> vv = new List<Vector2>();
		vv.AddRange(vIndex);
		
		//the same colum add a list
		while(vv.Count>0)
		{
			List<Vector2> vv2 = new List<Vector2>();
			vv2.Add(vv[0]);
			if(vv.Count>1)
			{
				for(int j=1;j<vv.Count;j++)
				{
					if(vv[0].x == vv[j].x)
					{
						vv2.Add(vv[j]);
						vv.Remove(vv[j]);
						j -= 1;
					}
				}
			}
			vv.Remove(vv[0]);
			indexList.Add(vv2);
		}
		
		//hide remove element
		for(int i=0;i<vIndex.Length;i++)
		{
			Vector2 v = vIndex[i];
			mapInfo[(int)v.y,(int)v.x] = 0;
			objDic[v.y+","+v.x].obj.SetActive(false);
			if(!dataTile.Contains(objDic[v.y+","+v.x].obj))
			{
				dataTile.Add(objDic[v.y+","+v.x].obj);
			}
			objDic[v.y+","+v.x].obj = null;
			
			UILabel  lable =  ((GameObject)Instantiate( Resources.Load("Prefabs/UI/Score"))).GetComponent<UILabel>();
			lable.transform.parent = transform;
			lable.transform.localPosition = new Vector3(objDic[v.y+","+v.x].x,objDic[v.y+","+v.x].y,0);
			lable.transform.localScale = new Vector3(20,20,1);
			int curPoint = 10+i*5;
			lable.text =""+curPoint;
			iTween.MoveTo(lable.gameObject,iTween.Hash("position",new Vector3(totalPointLab.transform.localPosition.x,totalPointLab.transform.localPosition.y,totalPointLab.transform.localPosition.z),"time",0.5f+i*0.3f,"islocal",true,"oncomplete","OnCompleteMoveOver","oncompletetarget",lable.gameObject,"oncompleteparams",curPoint));
		    if(!gameOverCheckFlag)
			{
				gameOverCheckFlag = true;
			}
		}
		Game.soundManager.PlayCollectPoint();
		//According to the growing up of column sorting
		for(int i=0;i<indexList.Count;i++)
		{
			if(indexList[i].Count==1) continue;
			for(int j=0;j<indexList[i].Count;j++)
			{
					for(int m=0;m<indexList[i].Count-1;m++)
					{
					   Vector2 temp;
						if(indexList[i][m].y>indexList[i][m+1].y)
						{
							temp = indexList[i][m];
						    indexList[i][m] = indexList[i][m+1];
						    indexList[i][m+1] = temp;
						}
					}
			}
		}
		for(int i=0;i<indexList.Count;i++)
		{
			Vector2 temp = indexList[i][0];
			int j = (int)temp.y;
			int c = (int)temp.x;
			for(;j<row;j++)
			{
				for(int m=j+1;m<row;m++)
				{
					if(mapInfo[m,c]>0)
					{
						mapInfo[j,c] = mapInfo[m,c];
						mapInfo[m,c] = 0;
						Tile t = new Tile();
						t.y = objDic[j+","+c].y;
						objDic[j+","+c].obj = objDic[m+","+c].obj;
						t.obj = objDic[j+","+c].obj;
						tile.Add(t);
						break;
					}
				}
			}
		}
		
		f = 1000;
		moveTileNum = 0;
		for(int s=0;s<vIndex.Length;s++)
		{
			Vector2 temp = vIndex[s];
			if(mapInfo[(int)temp.y,(int)temp.x]==0 && temp.y==0)
			{ 
				if(f>temp.x)
				{
					f = (int)temp.x;
				}
				moveTileNum++;
			}
		}
		if(f==1000)
		{
			for(int i =0;i<tile.Count;i++)
			{
				dropFlag = true;
				if(gameOverCheckFlag)
				{
					gameOverCheckFlag = false;
				}
				iTween.MoveTo(tile[i].obj,iTween.Hash("y",tile[i].y,"time",0.3f,"islocal",true,"easetype",iTween.EaseType.easeInOutBack,"oncomplete","MoveOnComplete","oncompletetarget",gameObject,"oncompleteparams",i));
			}
		}
		else
		{
			dropFlag = true;
			if(gameOverCheckFlag)
			{
				gameOverCheckFlag = false;
			}
			if(tile.Count>0)
			{
					for(int i =0;i<tile.Count;i++)
					{
						iTween.MoveTo(tile[i].obj,iTween.Hash("y",tile[i].y,"time",0.3f,"islocal",true,"easetype",iTween.EaseType.easeInOutBack,"oncomplete","MoveOnComplete1","oncompletetarget",gameObject,"oncompleteparams",i));
					}
				//print("&&&&&&&&&&&&&&&&");
			}
			else
			{
				MoveOnComplete1(-1);
				//print("@@@@@@@@@@@@@@@&");
			}
		}
	
		if(!dropFlag)
		{
			intervalFlag = false;
		}
		if(gameOverCheckFlag)
		{
			gameOverCheckFlag = false;
			CheckGameOver();
		}
		//print(" indexList.count:"+indexList.Count);
	}
	
	void MoveOnComplete(int i)
	{
		if(i == tile.Count-1)
		{
			intervalFlag = false;
			dropFlag = false;
			CheckGameOver();
		}
	}
	void MoveOnComplete1(int s)
	{
		if(s == tile.Count-1)
		{
			for(int j =0;j<row;j++)
			{
				for(int i=f;i<column;i++)
				{
					for(int m =i+moveTileNum;m<column;m++)
					{
						if(mapInfo[j,m]>0)
						{
							mapInfo[j,i] = mapInfo[j,m];
							mapInfo[j,m] = 0;
							Tile t = new Tile();
							t.y = objDic[j+","+i].x;
							objDic[j+","+i].obj = objDic[j+","+m].obj;
							t.obj = objDic[j+","+i].obj;
							horizontalTile.Add(t);
						}
						break;
					}
				}
			}
			if(horizontalTile.Count>0)
			{
				for(int i=0;i<horizontalTile.Count;i++)
				{
					horizontalTile[i].obj.SetActive(true);
					iTween.MoveTo(horizontalTile[i].obj,iTween.Hash("x",horizontalTile[i].y,"time",0.3f,"islocal",true,"easetype",iTween.EaseType.easeInOutBack,"oncomplete","MoveOnComplete2","oncompletetarget",gameObject,"oncompleteparams",i));
				}
			}
			else
			{
				intervalFlag = false;
			    dropFlag = false;
			}
			
		}
	}
	void MoveOnComplete2(int i)
	{
		if(i == horizontalTile.Count-1)
		{
			//print("***************");
			intervalFlag = false;
			dropFlag = false;
			CheckGameOver();
		}
	}
	
	void ShowTile()
	{
		StringBuilder str = new StringBuilder();
		for(int i=row-1;i>=0;i--)
		{
			for(int j=0;j<column;j++)
			{
				str.Append(mapInfo[i,j]);
				str.Append(",");
			}
			str.Append("\n");
		}
		print(str);
	}
	
	
	void AddSameTile(Vector2 v2,string str)
	{
		int tempTileType = -100;
		if(!constVar.Equals(v2))
		{
			tempTileType = GetTileType(v2);
		}
		if(curType == tempTileType)
		{
			if(!tempQueue.Contains(v2) && !saveQueue.Contains(v2))
			{
				//print(" dic:" + str);
				tempQueue.Enqueue(v2);
			}
		}
	}
	
	void CheckGameOver()
	{
		print("WWWWWWWWWWWWW");
		int counter = 0;
		Vector2 up,down,left,right;
		Vector2 v2;
		for(int i=0;i<row;i++)
		{
			for(int j=0;j<column;j++)
			{
				if(mapInfo[i,j]>0)
				{
					v2.x =j;
					v2.y = i;
					
				    CheckTileInfo(v2,out up,out down,out left,out right);
					if(!constVar.Equals(up)&& mapInfo[i,j] == mapInfo[(int)up.y,(int)up.x])
					{
						print("WWWWWWWWWWWWW UP");
						return;
					}
					if(!constVar.Equals(down)&& mapInfo[i,j] == mapInfo[(int)down.y,(int)down.x])
					{
						print("WWWWWWWWWWWWW Down");
						return;
					}
					if(!constVar.Equals(left)&& mapInfo[i,j] == mapInfo[(int)left.y,(int)left.x])
					{
						print("WWWWWWWWWWWWW Left");
						return;
					}
					if(!constVar.Equals(right)&& mapInfo[i,j] == mapInfo[(int)right.y,(int)right.x])
					{
						print("WWWWWWWWWWWWW Right");
						return;
					}
				}
				
			}
		}
		
		for(int i=0;i<row;i++)
		{
			for(int j=0;j<column;j++)
			{
				if(mapInfo[i,j]>0)
				{
					
					if(!dataTile.Contains(objDic[i+","+j].obj))
					{
						dataTile.Add(objDic[i+","+j].obj);
					}
				}
			}
		}
		iTween.MoveTo(gameOver.gameObject,iTween.Hash("y",0,"time",1,"isLocal",true,"easyType",iTween.EaseType.linear,"oncomplete","GameOverComplete","oncompletetarget",gameObject));
		print("GameOver$$$$$$$$$$$$$$!"+ dataTile.Count);
		//print("DDDDDDDDDDDDDDDDDDDDD");
	}
	void GameOverComplete()
	{
		for(int i=0;i<dataTile.Count;i++)
		{
			dataTile[i].SetActive(false);
			
		}
		base.Close();
		Game.uimanager.gameOverPanel = Game.uimanager.CreateUIObj<GameOverPanel>(UIPANEL.GameOverPanel);
		
	}
	
	int GetTileType(Vector2 v2)
	{
     	return mapInfo[(int)v2.y,(int)v2.x];
	}
	
	public void SetTotalPoint()
	{
	    totalPointLab.text = "Total Points:"+ Game.dataManager.playerData.GetTotalPoints;
	}
	
	void CheckTileInfo(Vector2 v2,out Vector2 up,out Vector2 down,out Vector2 left,out Vector2 right)
	{
			up.x = v2.x;
			up.y = v2.y+1;
			
			down.x = v2.x;
			down.y = v2.y-1;
			
			left.x = v2.x-1;
			left.y = v2.y;
			
			right.x = v2.x+1;
			right.y = v2.y;
			
			if(v2.x == 0)
			{
				left.x = -1;
				left.y = -1;
			}
			else if(v2.x == column-1)
			{
				right.x = -1;
				right.y = -1;
			}
			
			if(v2.y == 0)
			{
				down.x = -1;
				down.y = -1;
			}
			else if(v2.y == row-1)
			{
				up.x = -1;
				up.y = -1;
			}
	}
}

class Tile
{
	public float y;
	public GameObject obj;
}

