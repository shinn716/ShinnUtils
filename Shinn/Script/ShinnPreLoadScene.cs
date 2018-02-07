using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShinnPreLoadScene : MonoBehaviour {

	[Header("預載場景")]
	public AsyncOperation async;
	public string LoadSceneName;
	public float time;
	public bool loadScene;

	void Start () {
		if(loadScene)
			Invoke ("DelayLoad", time);
	}

	void DelayLoad(){
		async = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync (LoadSceneName, LoadSceneMode.Single);
		async.allowSceneActivation = false;
	}
}
