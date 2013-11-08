using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {
	
	public static  UIManager uimanager;
	public static  AndroidPlugin androidPlugin;
	public static  DataManager dataManager;
	public Transform uiParent;
	// Use this for initialization
	void Start () {
		dataManager = CreateObj<DataManager>("DataManager");
		uimanager = CreateObj<UIManager>("UIManager");
		androidPlugin = CreateObj<AndroidPlugin>("AndroidPlugin");
		
	    uimanager.SetUIParent = uiParent;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public T CreateObj<T> (string prefabName)where T:Component 
	{
		 string path="Prefabs/LoadPrefabs/Obj/"+prefabName;
		 GameObject go= (GameObject)Instantiate( Resources.Load(path));
		 go.transform.parent = gameObject.transform;
		 go.name = prefabName;
		 T t= go.GetComponent<T>();
		 return t;
	}
}

