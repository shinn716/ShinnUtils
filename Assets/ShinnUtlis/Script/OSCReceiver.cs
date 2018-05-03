using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShinnUtil{

	[RequireComponent(typeof(OSC))]
	public class OSCReceiver : MonoBehaviour {

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
			}
		}
		
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
