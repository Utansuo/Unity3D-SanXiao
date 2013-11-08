using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class UIManager : MonoBehaviour {
	
	public  Dictionary<UIPANEL,Component> uiDictory;
 	private Transform uiParent;
	// Use this for initialization
	void Start () {
		uiDictory = new Dictionary<UIPANEL,Component>(); 
	
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
		}
		else
		{
			string path=Game.dataManager.uiDic[uiPanel];
			GameObject go=(GameObject)Instantiate( Resources.Load(path));
			go.transform.parent = uiParent;
		    go.transform.localScale = new Vector3(1,1,1);
			go.name = path.Substring(path.LastIndexOf('/'));
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


