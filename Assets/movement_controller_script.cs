using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement_controller_script : MonoBehaviour {

	public GameObject objReal;
	public GameObject objImagem;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Movement(int direction){
		const float offset = 0.5f;
		const int esquerda = -1;

		if (direction == esquerda)    
			objReal.transform.localPosition = new Vector3(objReal.transform.localPosition.x - offset,objReal.transform.localPosition.y,objReal.transform.localPosition.z);
		else 					//DIREITA
			objReal.transform.localPosition =  new Vector3(objReal.transform.localPosition.x + offset,objReal.transform.localPosition.y,objReal.transform.localPosition.z);



	}

}
