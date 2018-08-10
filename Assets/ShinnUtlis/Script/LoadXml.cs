﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;

namespace ShinnUtil{

	public class LoadXml : MonoBehaviour {
		
		public bool absoluteLoc = true;
		public string filepath = "C:/Users/Shinn/Desktop/EmailSetting.xml";

		public static string temp_client;
		public static string temp_port;
		public static string temp_user;
		public static string temp_pass;

		public static string temp_to;
		public static string temp_subject;
		public static string temp_body;
		public static string temp_fileloc;

		void Awake(){
			
			if(absoluteLoc)
				LoadFromXml (filepath);
			else{
				string assets = (Application.streamingAssetsPath + "/EmailSetting.xml").ToString();
				LoadFromXml(assets);
			}
				
		}

		public void LoadFromXml(string path)
		{
			XmlDocument xmlDoc = new XmlDocument (); 
			xmlDoc.Load (path);


			if (File.Exists (path)) {
				print ("Load XML Success..");

				xmlDoc.Load (path); 

				XmlNodeList _client = xmlDoc.GetElementsByTagName ("SMTP_Client");
				XmlNodeList _port = xmlDoc.GetElementsByTagName ("SMTP_Port");
				XmlNodeList _user = xmlDoc.GetElementsByTagName ("USER");
				XmlNodeList _pass = xmlDoc.GetElementsByTagName ("USER_Pass");

				XmlNodeList _to = xmlDoc.GetElementsByTagName ("To");
				XmlNodeList _subject = xmlDoc.GetElementsByTagName ("Subject");
				XmlNodeList _body = xmlDoc.GetElementsByTagName ("Body");
				XmlNodeList _file = xmlDoc.GetElementsByTagName ("AttachFile");

				temp_client = _client.Item (0).InnerText;	
				temp_port = _port.Item (0).InnerText;	
				temp_user = _user.Item (0).InnerText;	
				temp_pass = _pass.Item (0).InnerText;

				temp_to = _to.Item (0).InnerText;	
				temp_subject = _subject.Item (0).InnerText;	
				temp_body = _body.Item (0).InnerText;	
				temp_fileloc = _file.Item (0).InnerText;	

			}
		}


	}

}
