using UnityEngine;
using System.Collections;

public static class Util{

	public static  float map(float x, float fromLow, float fromHigh, float toLow, float toHigh){
		return ( x - fromLow) / (fromHigh - fromLow) * (toHigh - toLow) + toLow; 
	}

}