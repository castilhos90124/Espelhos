using UnityEngine;
using System.Collections;
using System.IO;
using System;
using UnityEngine.UI;

public class plano_script : MonoBehaviour
{
	private const float translationSpeed = 2;
	private const float offsetParalelo = 0.0033f;
	private const float offsetOrigem = 4.15f;
	private const float multiplierAngle = 49f;
	private const float objHeight = 2.9f;
	private const float multiplierScale = 0.115f;
	private const float offset = 6.3f;
	private float distanceToMirror;
	private float distanceOrigem;
	 

	bool repeatPositionLeft = false;
	bool repeatPositionRight = false;

	public GameObject objReal;
	public GameObject objImagem;
	public GameObject raioIncidenteParalelo;
	public GameObject raioIncidenteOrigem;
	public GameObject raioRefletido;
	public GameObject tracejado;



	void Start()
	{
		
	}
		
	void Update ()
	{
		distanceToMirror = -objReal.transform.localPosition.x + offsetOrigem;
		distanceOrigem = Mathf.Sqrt (Mathf.Pow (distanceToMirror, 2) + Mathf.Pow (objHeight, 2));
		if (repeatPositionLeft) {
			PositionLeftButton();
		}

		if (repeatPositionRight) {
			PositionRightButton();
		}


		//Debug.Log (distanceToMirror);

	}

	public void PositionLeftButtonRepeat ()
	{
		repeatPositionLeft = true;
	}

	public void PositionRightButtonRepeat ()
	{
		repeatPositionRight = true;
	}


	public void PositionLeftButtonOff ()
	{
		repeatPositionLeft = false;
	}

	public void PositionRightButtonOff ()
	{
		repeatPositionRight = false;
	}


	public void PositionRightButton ()
	{
		const float boundaryRightLimit = 3.82f; 
		float realPosition = objReal.transform.localPosition.x;

		if (realPosition < boundaryRightLimit) {
			
			objReal.transform.Translate (translationSpeed * Time.deltaTime, 0, 0);
			objImagem.transform.Translate (-translationSpeed * Time.deltaTime, 0, 0);

			raioIncidenteParalelo.transform.localScale -= new Vector3 (offsetParalelo,0,0);

			raioIncidenteOrigem.transform.localRotation = Quaternion.Euler (new Vector3 (0, 0, Mathf.Asin(distanceToMirror/distanceOrigem) * multiplierAngle)); 
			raioIncidenteOrigem.transform.localScale = new Vector3 (1,distanceOrigem * multiplierScale,1);

			tracejado.transform.localRotation = Quaternion.Euler (new Vector3 (0, 0, - (Mathf.Asin(distanceToMirror/distanceOrigem) * multiplierAngle) +offset));

			raioRefletido.transform.localRotation = Quaternion.Euler (new Vector3 (0, 0, -(Mathf.Asin(distanceToMirror/distanceOrigem) * multiplierAngle)+offset));
		}
	}

	public void PositionLeftButton ()
	{
		const float boundaryLeftLimit = 0.42f;

		float realPosition = objReal.transform.localPosition.x;

		if (objReal.transform.localPosition.x > boundaryLeftLimit) {
			objReal.transform.Translate (-translationSpeed * Time.deltaTime, 0, 0);
			objImagem.transform.Translate (translationSpeed * Time.deltaTime, 0, 0);

			raioIncidenteParalelo.transform.localScale += new Vector3 (offsetParalelo,0,0);
			raioIncidenteOrigem.transform.localRotation = Quaternion.Euler (new Vector3 (0, 0, Mathf.Asin(distanceToMirror/distanceOrigem) * multiplierAngle));
			//Debug.Log (Mathf.Asin (distanceToMirror / distanceOrigem));
			raioIncidenteOrigem.transform.localScale = new Vector3 (1,distanceOrigem * multiplierScale,1);

			tracejado.transform.localRotation = Quaternion.Euler (new Vector3 (0, 0, - (Mathf.Asin(distanceToMirror/distanceOrigem) * multiplierAngle) +offset));

			raioRefletido.transform.localRotation = Quaternion.Euler (new Vector3 (0, 0, -(Mathf.Asin(distanceToMirror/distanceOrigem) * multiplierAngle)+offset));

		}
	}

}