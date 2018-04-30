using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ShinnUtil{

	public enum ClickState{
		Open, OpenAndClose
	}

	public class UI_tool1 : MonoBehaviour {

		public GameObject content;
		public ClickState _ClickState;
		bool clickst = false;

		[Header("Delay Show Obj")]
		public float DelayTime;
	
		[Header("Position")]
		public bool PosSt = false;
		public Vector3 PosEnd;
		public float PosTime;
		public iTween.EaseType PosEase;
		public iTween.LoopType PosLoop;
		Vector3 orgPos;
	
	
		[Header("Rotation")]
		public bool RotSt = false;
		public Vector3 RotEnd;
		public float RotTime;
		public iTween.EaseType RotEase;
		public iTween.LoopType RotLoop;
		Vector3 orgRot;
	
	
		[Header("Scale")]
		public bool SclSt = false;
		public Vector3 SclEnd;
		public float SclTime;
		public iTween.EaseType SclEase;
		public iTween.LoopType SclLoop;
		Vector3 orgScl;
	
	
		[Header("FadeFuct")]
		public bool Fadeinst = false;
		public Color ColorValue;
		public float FadeTime = 1;
		Color OrgColorValue;
	
		void Awake(){
		
			orgPos = content.transform.localPosition;
			orgRot = content.transform.localEulerAngles;
			orgScl = content.transform.localScale;


			if (content.GetComponent<GUITexture> ()) {
				OrgColorValue = content.GetComponent<GUITexture> ().color;	
			} else if (content.GetComponent<GUIText> ()) {
				OrgColorValue = content.GetComponent<GUIText> ().material.color;
			} else if (content.GetComponent<Renderer> ()) {
				OrgColorValue = content.GetComponent<Renderer> ().material.color;
			} else if (content.GetComponent<Light> ()) {
				OrgColorValue = content.GetComponent<Light> ().color;
			} else if (content.GetComponent<Image> ()) {
				OrgColorValue = content.GetComponent<Image> ().color;
			} else if (content.GetComponent<RawImage> ()) {
				OrgColorValue = content.GetComponent<RawImage> ().color;
			}

		}


		public void State(){

			switch(_ClickState){
			default:
				break;
			case ClickState.Open:
				Open ();
				break;

			case ClickState.OpenAndClose:
				OpenAndClose ();
				break;
			}
	
		}



		void Open(){
			Invoke ("AnimationStart", DelayTime);
		}

		void OpenAndClose(){
			clickst = !clickst;

			if (clickst) {
				Invoke ("AnimationStart", DelayTime);
			}
			if (!clickst) {
				Invoke ("AnimationEnd", DelayTime);
			}

		}

		void AnimationStart(){
		
			content.SetActive (true);

			if (PosSt)
				iTween.MoveTo (content, iTween.Hash ("position", PosEnd, "time", PosTime, "easetype", PosEase, "looptype", PosLoop, "islocal", true));


			if (RotSt)
				iTween.RotateTo (content, iTween.Hash ("rotation", RotEnd, "time", RotTime, "easetype", RotEase, "looptype", RotLoop, "islocal", true));


			if (SclSt)
				iTween.ScaleTo (content, iTween.Hash ("scale", SclEnd, "time", SclTime, "easetype", SclEase, "looptype", SclLoop, "islocal", true));


			if (Fadeinst)
				iTween.ColorTo (content, iTween.Hash ("r", ColorValue.r, "g", ColorValue.g, "b", ColorValue.b, "a", ColorValue.a, "time", FadeTime));
		}

		void AnimationEnd(){

			if (PosSt)
				iTween.MoveTo (content, iTween.Hash ("position", orgPos, "time", PosTime, "easetype", PosEase, "islocal", true));


			if (RotSt)
				iTween.RotateTo (content, iTween.Hash ("rotation", orgRot, "time", RotTime, "easetype", RotEase, "islocal", true));


			if (SclSt)
				iTween.ScaleTo (content, iTween.Hash ("scale", orgScl, "time", SclTime, "easetype", SclEase, "islocal", true));

			if (Fadeinst)
				iTween.ColorTo (content, iTween.Hash ("r", OrgColorValue.r, "g", OrgColorValue.g, "b", OrgColorValue.b, "a", 0, "time", FadeTime, "oncomplete", "endFuct", "oncompletetarget", content));
		}

		void endFuct(){
			content.SetActive (false);
		}


		public void FadeoutAndEnd(){
			iTween.ColorTo (content, iTween.Hash ("r", ColorValue.r, "g", ColorValue.g, "b", ColorValue.b, "a", 0, "time", FadeTime, "oncomplete", "endFuct", "oncompletetarget", content));
		}

		
	}




}