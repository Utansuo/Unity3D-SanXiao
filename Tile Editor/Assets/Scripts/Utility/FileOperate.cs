using UnityEngine;
using System.Collections;
using System.Text;
using System.IO;
public class FileOperate 
{

	public static string JsonPath
    {
        get{   
            string    path=null;
            if(Application.platform==RuntimePlatform.IPhonePlayer)
            {
                path= Application.dataPath.Substring (0, Application.dataPath.Length - 5);
                path = path.Substring(0, path.LastIndexOf('/'))+"/Documents/"; 
             }
			else if(Application.platform==RuntimePlatform.Android)
            {
                path= Application.persistentDataPath;
             }
            else
            {
                path=Application.dataPath;
            }
            return path;
        }  
    }
	
	public static string ReadFile(string txtName)
    {
        string path=JsonPath+txtName;
        string readResult = string.Empty;
		if(File.Exists(path))
		{
			FileStream  fs = new  FileStream(path,FileMode.Open);
			StreamReader sr = new StreamReader(fs);
			readResult = sr.ReadToEnd();
			sr.Close();
			fs.Close();
		}
		else
		{
			Debug.Log("No json ");
		}  
		
		return readResult;
    }
	
	 public static void WriteFile(string txtName,string content)
    {
        string path=JsonPath+txtName;
        FileStream fs;
        if(!File.Exists(path))
        {
            fs=new  FileStream(path,FileMode.Create);
        }
        else
        {
            fs=new  FileStream(path,FileMode.Truncate);//注意对存在文件内容替换
        }
        StreamWriter sw=new StreamWriter(fs);
        sw.Write(content);
        sw.Close();
        fs.Close();  
    }
}
