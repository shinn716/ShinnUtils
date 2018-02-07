using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShinnFadeFuct : MonoBehaviour {

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

	void OnEnable(){
		_fadeinend = false;
		_fadeoutend = false;
	}

	void Update () {

		if(FadeIn){
			if (lerpvalue > 0.98f) {
				lerpvalue = 1f;
				FadeIn = false;

				_fadeinend = true;

			} else
				lerpvalue = Mathf.Lerp (lerpvalue, 1, Time.deltaTime * FadeSpeed);

			this.GetComponent<Image> ().color = new Color (r, g, b, lerpvalue);

		}


		if(FadeOut){
			if (lerpvalue < 0.02f) {
				lerpvalue = 0f;
				FadeOut = false;

				_fadeoutend = true;

				if (CompleteDisableObject)
					this.gameObject.SetActive (false);
			} else
				lerpvalue = Mathf.Lerp (lerpvalue, 0, Time.deltaTime * FadeSpeed);

			this.GetComponent<Image> ().color = new Color (r, g, b, lerpvalue);

		}
	}
		
}
