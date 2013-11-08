using UnityEngine;
using System.Collections;

public class BaseUI : MonoBehaviour {
	
	[HideInInspector]
	private UIPANEL uiPanel;
	[HideInInspector]
	private bool uiDestory;
	
	public virtual void Init(UIPANEL panel,bool flag)
	{
		uiPanel = panel;
		uiDestory = flag;
	}
 
	public virtual void Close()
	{
		
		if(uiDestory)
		{
			Destroy(this.gameObject);
			Game.uimanager.uiDictory.Remove(uiPanel);
		}
		else
		{
			this.gameObject.SetActive(false);
		}
	}
}
