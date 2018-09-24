using System;
using UnityEngine;

public class EncodeSample : MonoBehaviour
{
	public string content;
	public string password;
	public string result;

	private void Start()
	{
		var encoder = new Clib.Security();
		encoder.Key = password;
		result = encoder.EncryptBase64(content);
	}
}
