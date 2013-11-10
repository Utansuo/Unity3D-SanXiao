using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {
	
	public static  UIManager uimanager;
	public static  AndroidPlugin androidPlugin;
	public static  DataManager dataManager;
	public static  MusicManager musicManager;
	public static  SoundManager soundManager;
	public Transform uiParent;
	
	void Awake()
	{
		InitManager();
	}
	// Use this for initialization
	void Start () {
	
	}
	void InitManager()
	{
			dataManager = CreateObj<DataManager>("DataManager");
			uimanager = CreateObj<UIManager>("UIManager");
			musicManager = CreateObj<MusicManager>("MusicManager");
			soundManager = CreateObj<SoundManager>("SoundManager");
			//androidPlugin = CreateObj<AndroidPlugin>("AndroidPlugin");
			
			uimanager.SetUIParent = uiParent;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public T CreateObj<T> (string prefabName)where T:Component 
	{
		 string path="Prefabs/Obj/"+prefabName;
		 GameObject go= (GameObject)Instantiate( Resources.Load(path));
		 go.transform.parent = gameObject.transform;
		 go.name = prefabName;
		 T t= go.GetComponent<T>();
		 return t;
	}
}

