using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsTools : MonoBehaviour {

	[SerializeField] float[] fvalue;
	[SerializeField] int[] ivalue;
	[SerializeField] string[] svalue;

	public float[] RW_fvalue {
		set { fvalue = value; }
		get { return fvalue; }
	}

	public int[] RW_ivalue{
		set { ivalue = value; }
		get { return ivalue; }
	}

	public string[] RW_svalue{
		set { svalue = value; }
		get { return svalue; }
	}

	public KeyCode saveKey = KeyCode.F7;
	public KeyCode defaultKey = KeyCode.F8;

	private void Update(){
		
		if (Input.GetKeyDown(saveKey))
			Save ();
		
		if (Input.GetKeyDown(defaultKey))
			Default ();		
	}

	public void Save () {

		for (int i = 0; i < fvalue.Length; i++)
			PlayerPrefs.SetFloat (("fvalue" + i), fvalue[i]);

		for (int i = 0; i < ivalue.Length; i++)
			PlayerPrefs.SetInt (("ivalue" + i), ivalue[i]);

		for (int i = 0; i < svalue.Length; i++)
			PlayerPrefs.SetString (("svalue" + i), svalue[i]);
	}

	public void Default () {
		
		for (int i = 0; i < fvalue.Length; i++)
			PlayerPrefs.SetFloat (("fvalue" + i), 0);

		for (int i = 0; i < ivalue.Length; i++)
			PlayerPrefs.SetInt (("ivalue" + i), 0);

		for (int i = 0; i < svalue.Length; i++)
			PlayerPrefs.SetString (("svalue" + i), null);
	}

	public void Load () {

		for (int i = 0; i < fvalue.Length; i++) {
			fvalue [i] = PlayerPrefs.GetFloat (("fvalue" + i));
			Debug.Log("f-value: " + fvalue[i]);
		}

		for (int i = 0; i < ivalue.Length; i++) {
			ivalue [i] = PlayerPrefs.GetInt (("ivalue" + i));
			Debug.Log("i-value: " + ivalue[i]);
		}

		for (int i = 0; i < svalue.Length; i++) {
			svalue [i] = PlayerPrefs.GetString (("svalue" + i));
			Debug.Log("s-value: " + svalue[i]);
		}




	}

}
