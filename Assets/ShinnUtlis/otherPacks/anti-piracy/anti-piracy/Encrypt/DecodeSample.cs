using System;
using UnityEngine;

public class DecodeSample : MonoBehaviour
{
	public string password;
	public string encodedContent;
	public string correctContent;
	public string result;

	private void Start()
	{
		try
		{
			var encoder = new Clib.Security();
			encoder.Key = password;

			var decrypt = encoder.DecryptBase64(encodedContent);

			if (decrypt == correctContent)
				result = "Pass";
			else
				result = "Not Pass";
		}
		catch (Exception)
		{
			result = "Not Pass";
		}
	}
}
