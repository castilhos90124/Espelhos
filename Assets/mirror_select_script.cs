using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mirror_select_script : MonoBehaviour {

	public GameObject plano;
	public GameObject concavo;
	public GameObject convexo;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ChooseMirror(int espelho){

		switch (espelho) {
			
		case 1:
			plano.SetActive (true);
			concavo.SetActive (false);
			convexo.SetActive (false);
			break;
		case 2:
			plano.SetActive (false);
			concavo.SetActive (true);
			convexo.SetActive (false);
			break;
		case 3:
			plano.SetActive (false);
			concavo.SetActive (false);
			convexo.SetActive (true);
			break;
		default:
			plano.SetActive (false);
			concavo.SetActive (false);
			convexo.SetActive (false);
			break;
		}
	}
}
