using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Shinn{

	public class SceneFadeFuct : MonoBehaviour {

        [Header("FadeFuct (FadeInEnd, FadeOutEnd)")]
        public bool AutoStartFadeout = false;
        public bool AutoStartFadein = false;

        [SerializeField, Range(0, 1)]
		public float r, g, b;
        public float time;
        public float delay;
        public iTween.EaseType ease;
			

		[Header("Unity Events")]
		[SerializeField] UnityEvent _event;

		Color RenderColor;
		Renderer Render;
		Image RenderImage;

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
		}

		void OnEnable(){

            if (AutoStartFadeout)
                Fadeout();


            if (AutoStartFadein)
                Fadein();
		}

        public void Fadeout() {
            iTween.ValueTo(gameObject, iTween.Hash("from", 1, "to", 0, "time", time, "delay", delay, "onupdate", "processed1", "oncomplete", "complete", "oncompletetarget", gameObject, "easetype", ease));
        }

        public void Fadein()
        {
            iTween.ValueTo(gameObject, iTween.Hash("from", 0, "to", 1, "time", time, "delay", delay, "onupdate", "processed2", "oncomplete", "complete", "oncompletetarget", gameObject, "easetype", ease));
        }

        void processed1( float newvalue)
        {
            RenderColor = new Color(r, g, b, newvalue);
            if (GetComponent<Renderer>())
                Render.material.color = RenderColor;
            else if (GetComponent<Image>())
                RenderImage.color = RenderColor;
        }

        void processed2(float newvalue)
        {
            RenderColor = new Color(r, g, b, newvalue);
            if (GetComponent<Renderer>())
                Render.material.color = RenderColor;
            else if (GetComponent<Image>())
                RenderImage.color = RenderColor;
        }

        void complete()
        {
            _event.Invoke();
        }

        public void ReLoadLevel(int level){
			Application.LoadLevel (level);
		}

        public void Destroy(GameObject temp)
        {
            Destroy(temp);
        }
        
	}
}
