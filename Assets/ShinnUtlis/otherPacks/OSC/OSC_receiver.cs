using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShinnUtil{

	[RequireComponent(typeof(OSC))]
	public class OSC_receiver : MonoBehaviour {

		OSC osc;
		[Header("OSC Setting")]
		public bool oscenable = true;
		public string address = "/test";
	
		[SerializeField] 
		int state = 0;
		public int State {
			get{ return state; }
			set{ state = value; }
		}
	
	
		void Start () {
			if (oscenable) {
				osc = GetComponent<OSC> ();
				osc.SetAddressHandler (address, OnReceive);
				//OscMessageHandler (msgRceive);
			}
		}
		/*
		void msgRceive(OscMessage message){
			switch(message.address){
				
			default:
			break;

			case "/address1":
				print ("trigger1");
				state = 1;
			break;

			case "/address2":
				print ("trigger2");
				state = 2;
				break;

			case "/address3":
				print ("trigger3");
				state = 3;
				break;
			}

		}*/

		void OnReceive(OscMessage message){
			print ("Receive message " + message + " "+ message.values[0]);
			string receive = message.values[0].ToString();
			
			if(receive=="s7"){
				print ("trigger7");
				state = 7;
			}
			if(receive=="s5"){
				print ("trigger5");
				state = 5;
			}
			if(receive=="s4"){
				print ("trigger4");
				state = 4;
			}
		}

	}


}
