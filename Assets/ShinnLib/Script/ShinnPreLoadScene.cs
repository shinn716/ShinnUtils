using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShinnPreLoadScene : MonoBehaviour {

	[Header("PreLoadScene")]
	public string LoadSceneName;
	public float time;
	public bool loadScene;

	private AsyncOperation _async;
	public AsyncOperation async{
		get { return _async;}
	}


	void Start () {
		if(loadScene)
			Invoke ("DelayLoad", time);
	}

	void DelayLoad(){
		_async = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync (LoadSceneName, LoadSceneMode.Single);
		_async.allowSceneActivation = false;
	}
}
