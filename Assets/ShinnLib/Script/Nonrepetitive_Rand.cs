using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nonrepetitive_Rand : MonoBehaviour {

	[Header("Array Length (Get 'show')")]
	public int ArrayLength;

	public int[] show
	{
		get { return _show;}
	}

	[SerializeField]
	int[] _show;


	void Start () {
		init ();
	}
		

	public void init(){
		//-----Show In Editor
		_show = GetRandomSequence2(ArrayLength, ArrayLength);
	}

	public int[] GetRandomSequence2(int total, int n)
	{
		int[] sequence = new int[total];
		int[] output = new int[n]; 

		for (int i = 0; i < total; i++)
		{
			sequence[i] = i;
		}

		int end = total - 1;
		for (int i = 0; i < n; i++)
		{
			int num = Random.Range(0, end + 1);
			output[i] = sequence[num];
			sequence[num] = sequence[end];
			end--;
		}
		return output;
	}


}
