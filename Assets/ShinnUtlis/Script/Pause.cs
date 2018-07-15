using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour 
{
	public KeyCode pausekey;


    /// <summary>
    /// Has Avpro Packages
    /// </summary>
    //private RenderHeads.Media.AVProVideo.MediaPlayer[] mediaplayer;

    private bool pausest = false;

	void Start(){
        /// <summary>
        /// Has Avpro Packages
        /// </summary>
        
        //var  allmedia = Resources.FindObjectsOfTypeAll<RenderHeads.Media.AVProVideo.MediaPlayer> ();
        //mediaplayer = new RenderHeads.Media.AVProVideo.MediaPlayer[allmedia.Length];
        //mediaplayer = allmedia;
    }

    void Update()
	{
		if (Input.GetKeyDown (pausekey)) {

			pausest = !pausest;

			if (pausest)
				PauseGame ();
			else
				ContinueGame ();   

		}
	}

	private void PauseGame()
	{
		Time.timeScale = 0;


        /// <summary>
        /// Has Avpro Packages
        /// </summary>
        //if (mediaplayer == null)
        //	return;

        //for (int i = 0; i < mediaplayer.Length; i++)
        //	mediaplayer [i].Pause ();
    }

    private void ContinueGame()
	{
		Time.timeScale = 1;


        /// <summary>
        /// Has Avpro Packages
        /// </summary>
  //      if (mediaplayer == null)
		//	return;

		//for (int i = 0; i < mediaplayer.Length; i++)
		//	mediaplayer [i].Play ();
	}
}