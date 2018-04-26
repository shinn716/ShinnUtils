using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ShinnUtil{
	
	[RequireComponent(typeof(Image))]
	public class SceneFadeFuct : MonoBehaviour {

		[Header("FadeFuct (FadeInEnd, FadeOutEnd)")]
		public bool FadeIn;
		public bool FadeOut;

		[Space]
		public float FadeSpeed = 1;
		float lerpvalue=0;
	
		[SerializeField, Range(0, 1)]
		public float r, g, b;
	
		[Space]
		public bool CompleteDisableObject;
	
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

	
		[Header("ReLoad this Scene")]
		public bool loadScene = false;
		public int level = 0;

		void Start(){
			if (FadeIn)
				lerpvalue = 0;
			
			if (FadeOut)
				lerpvalue = GetComponent<Image>().color.a;
		}

		void OnEnable(){
			_fadeinend = false;
			_fadeoutend = false;
		}

		void Update () {

			if (FadeIn) {
				if (lerpvalue > 0.99f) {
					lerpvalue = 1f;
					FadeIn = false;
					GetComponent<Image> ().color = new Color (r, g, b, lerpvalue);
					_fadeinend = true;

					if (loadScene)
						Application.LoadLevel (level);

				} else {
					lerpvalue = Mathf.Lerp (lerpvalue, 1, Time.deltaTime * FadeSpeed);
					GetComponent<Image> ().color = new Color (r, g, b, lerpvalue);
				}
			}


			if (FadeOut) {
				if (lerpvalue < 0.01f) {
					lerpvalue = 0f;
					FadeOut = false;
					GetComponent<Image> ().color = new Color (r, g, b, lerpvalue);
					_fadeoutend = true;

					if (CompleteDisableObject)
						gameObject.SetActive (false);
				} else {
					lerpvalue = Mathf.Lerp (lerpvalue, 0, Time.deltaTime * FadeSpeed);
					GetComponent<Image> ().color = new Color (r, g, b, lerpvalue);
				}
			}


		}


	}
}
