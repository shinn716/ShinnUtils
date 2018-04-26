using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShinnUtil{

public class SkyChangeColor : MonoBehaviour {

		public GameObject sky;

		public Color color1 = Color.black;
		public Color color2 = Color.blue;
		public Color color3 = Color.cyan;
		public Color color4 = Color.clear;
		public Color color5 = Color.white;

	
		bool color1st = false;
		bool color2st = false;
		bool color3st = false;
		bool color4st = false;
		bool color5st = false;
	
		[Space]
		public KeyCode key1 = KeyCode.Q;
		public KeyCode key2 = KeyCode.W;
		public KeyCode key3 = KeyCode.E;
		public KeyCode key4 = KeyCode.R;
		public KeyCode key5 = KeyCode.T;
	
		Renderer render;
		Color mycolor;


		void Start(){
			render = sky.GetComponent<Renderer> ();
			mycolor = render.material.color;
		}

		void Update () {

			if (color1st) {
				mycolor = Color.Lerp (mycolor, color1, Time.fixedDeltaTime * 4);
				render.material.color = mycolor;
					
				color2st = false;
				color3st = false;
				color4st = false;
				color5st = false;
					
				Invoke ("color1delay", 1);
			}
				
			if (color2st) {
				mycolor = Color.Lerp (mycolor, color2, Time.fixedDeltaTime * 4);
				render.material.color = mycolor;
					
				color1st = false;
				color3st = false;
				color4st = false;
				color5st = false;
	
				Invoke ("color2delay", 1);
			}

			if (color3st) {
				mycolor = Color.Lerp (mycolor, color3, Time.fixedDeltaTime * 4);
				render.material.color = mycolor;
	
				color1st = false;
				color2st = false;
				color4st = false;
				color5st = false;
	
				Invoke ("color3delay", 1);
			}


			if (color4st) {
				mycolor = Color.Lerp (mycolor, color4, Time.fixedDeltaTime * 4);
				render.material.color = mycolor;
	
				color1st = false;
				color2st = false;
				color3st = false;
				color5st = false;
	
				Invoke ("color4delay", 1);
			}

			if (color5st) {
				mycolor = Color.Lerp (mycolor, color5, Time.fixedDeltaTime * 4);
				render.material.color = mycolor;
	
				color1st = false;
				color2st = false;
				color3st = false;
				color4st = false;
	
				Invoke ("color5delay", 1);
			}



			if (Input.GetKeyDown (key1))
				color1st = true;
		
			if (Input.GetKeyDown (key2))
				color2st = true;
		
			if (Input.GetKeyDown (key3))
				color3st = true;
		
			if (Input.GetKeyDown (key4))
				color4st = true;
		
			if (Input.GetKeyDown (key5))
				color5st = true;
		



		}


		void color1delay(){
			color1st = false;
		}

		void color2delay(){
			color2st = false;
		}

		void color3delay(){
			color3st = false;
		}

		void color4delay(){
			color4st = false;
		}

		void color5delay(){
			color5st = false;
		}

	}


}
