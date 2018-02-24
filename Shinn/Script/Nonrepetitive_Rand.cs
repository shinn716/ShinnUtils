using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nonrepetitive_Rand : MonoBehaviour {

	[Header("Array Length (Get 'value')")]
	public int ArrayLength;
	private int[] _value;
	public int[] value
	{
		get { return _value;}
		set	{ _value = value;}	
	}

	[SerializeField]
	int[] show;


	void Start () {
		init ();
	}
		

	public void init(){
		_value = GetRandomSequence2(ArrayLength, ArrayLength);
		//-----Show In Editor
		show = value;
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
