using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace ShinnUtil{

	public class SceneFadeFuct : MonoBehaviour {

		[Header("FadeFuct (FadeInEnd, FadeOutEnd)")]
		public bool FadeIn;
		public bool FadeOut;

		[Space]
		public float FadeSpeed = 1;
		float lerpvalue=0;

		[SerializeField, Range(0, 1)]
		public float r, g, b;

		bool _fadeinend = false;
		public bool FadeInEnd{
			get { return _fadeinend; }
			set { _fadeinend = value; }
		}

		bool _fadeoutend = false;
		public bool FadeOutEnd{
			get { return _fadeoutend; }
			set { _fadeoutend = value; }
		}
			

		[Header("Unity Events")]
		[SerializeField] UnityEvent _event;

		Color RenderColor;
		Renderer Render;
		Image RenderImage;

		bool fadeinorg;
		bool fadeoutorg;

		void Start(){

			if (GetComponent<Renderer> ()) {
				RenderColor = GetComponent<Renderer> ().material.color;
				Render = GetComponent<Renderer> ();
			} else if (GetComponent<Image> ()) {
				RenderColor = GetComponent<Image> ().color;
				RenderImage = GetComponent<Image> ();
			} else {
				Debug.LogWarning ("You need add component, like Image or Render.");
			}
				

			fadeinorg = FadeIn;
			fadeoutorg = FadeOut;

			if (FadeIn)
				lerpvalue = 0;

			if (FadeOut)
				lerpvalue = RenderColor.a;
		}

		void OnEnable(){



			if (fadeinorg) {

				RenderColor = new Color (r, g, b, 0);
				lerpvalue = 0;
				FadeIn = true;
			}
			if (fadeoutorg) {

				RenderColor = new Color (r, g, b, 1);
				lerpvalue = 1;
				FadeOut = true;

			}


		}

		void Update () {

			if (FadeIn) {
				if (lerpvalue > 0.99f) {
					lerpvalue = 1f;
					FadeIn = false;

					RenderColor = new Color (r, g, b, lerpvalue);
					if (GetComponent<Renderer> ())
						Render.material.color = RenderColor;
					else if (GetComponent<Image> ()) 
						RenderImage.color = RenderColor;

					_fadeinend = true;
					_event.Invoke ();


					//if (loadScene)
					//	Application.LoadLevel (level);

				} else {
					lerpvalue = Mathf.Lerp (lerpvalue, 1, Time.deltaTime * FadeSpeed);

					RenderColor = new Color (r, g, b, lerpvalue);

					if (GetComponent<Renderer> ())
						Render.material.color = RenderColor;
					else if (GetComponent<Image> ())
						RenderImage.color = RenderColor;
					

				}
			}


			if (FadeOut) {
				if (lerpvalue < 0.01f) {
					lerpvalue = 0f;
					FadeOut = false;

					RenderColor = new Color (r, g, b, lerpvalue);
					if (GetComponent<Renderer> ())
						Render.material.color = RenderColor;
					else if (GetComponent<Image> ())
						RenderImage.color = RenderColor;
					

					_fadeoutend = true;
					_event.Invoke ();

					//if (CompleteDisableObject)
					//	gameObject.SetActive (false);
					
				} else {
					
					lerpvalue = Mathf.Lerp (lerpvalue, 0, Time.deltaTime * FadeSpeed);
					RenderColor = new Color (r, g, b, lerpvalue);

					if (GetComponent<Renderer> ())
						Render.material.color = RenderColor;
					else if (GetComponent<Image> ())
						RenderImage.color = RenderColor;
					

				}
			}


		}


		public void ReLoadLevel(int level){
			Application.LoadLevel (level);
		}


	}
}