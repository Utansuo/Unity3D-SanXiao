using UnityEngine;
using System.Collections;

public class BaseUI : MonoBehaviour {
	

	public UIPANEL uiPanel;
	public bool uiDestory;
	

 
	public virtual void Close()
	{
		if(uiDestory)
		{
			gameObject.SetActive(false);
			Destroy(this.gameObject,0.05f);
			Game.uimanager.uiDictory.Remove(uiPanel);
		}
		else
		{
			this.gameObject.SetActive(false);
		}
	}
}
