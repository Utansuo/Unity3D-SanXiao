using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class UIManager : MonoBehaviour {
	
	public  Dictionary<UIPANEL,Component> uiDictory;
 	private Transform uiParent;
	public StartPanel startPanel;
	public CreateMap mainUI;
	public SetPanel setPanel;
	public GameOverPanel gameOverPanel;
	// Use this for initialization
	void Start () {
		uiDictory = new Dictionary<UIPANEL,Component>(); 
	    InitStartPanel();

	}
	
	void InitStartPanel()
	{
		startPanel = CreateUIObj<StartPanel>(UIPANEL.StartPanel);
	}
	

	
	// Update is called once per frame
	void Update () {
	
	}
	
	public T CreateUIObj<T> (UIPANEL uiPanel)where T:Component 
	{
		T t = null;
		if(uiDictory.ContainsKey(uiPanel))
		{
			t = (T)uiDictory[uiPanel];
			if(!t.gameObject.activeSelf)
			{
				t.gameObject.SetActive(true);
			}
		}
		else
		{
			string path = Game.dataManager.uiDic[uiPanel];
			print("AA:"+ path);
			GameObject go = Instantiate(Resources.Load(path)) as GameObject;
			go.transform.parent = uiParent;
		    go.transform.localScale = new Vector3(1,1,1);
			go.name = path.Substring(path.LastIndexOf('/')+1);
			t= go.GetComponent<T>();
		    uiDictory.Add(uiPanel,t);
		}
		 
	
		 return t;
	}
	
	
	
	public Transform SetUIParent
	{
		set{uiParent = value ;}
	}
	
	
		
	
}


