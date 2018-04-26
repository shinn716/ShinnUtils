using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ShinnUtil{

public class PreLoadScene : MonoBehaviour {

	[Header("PreLoadScene")]
	[SerializeField] string LoadSceneName;
	[SerializeField] float time;
	[SerializeField] bool loadScene;

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

}
