using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

public class ReadXmlData : MonoBehaviour
{
	string filepath;
	int xml_startstep;
	int xml_endstep;

	public void init()
	{
		filepath ="C:/TAO_iWall/UrgData.xml";			
		LoadFromXml();

		Urg._startstep = xml_startstep;
		Urg._endstep   = xml_endstep;
	}
		
	private void LoadFromXml()
	{
		
		XmlDocument xmlDoc = new XmlDocument(); 
		
		if (File.Exists (filepath)) 
		{
			xmlDoc.Load (filepath); 
			XmlNodeList transformList_urg = xmlDoc.GetElementsByTagName ("URGStep");
			foreach (XmlNode transformInfo in transformList_urg) 
			{
				
				XmlNodeList transformcontent = transformInfo.ChildNodes;
				foreach (XmlNode transformItens in transformcontent) 
				{
					
					if (transformItens.Name == "StartStep") 
						xml_startstep = int.Parse (transformItens.InnerText);
					
					if (transformItens.Name == "EndStep") 
						xml_endstep = int.Parse (transformItens.InnerText);

				}
			}


		}

	}


}