using UnityEngine;
using System.Collections;
using System.IO;
using System;
using UnityEngine.UI;

public class convexo_script : MonoBehaviour
{
	private const float translationSpeed = 2;
	private const float virtualTranslationSpeed = 1.5f;
	//private const float focoMinPosition = 1.03f;
	//private const float focoMaxPosition = 1.15f;
	//private const float centroPosition = -1.23f; 
	//private const float realMultiplier = 0.99f;
	private const float virtualMultiplier = 0.992f;

	private const float offsetParalelo = 0.00048f;

	private const float multiplierAngle = 58f;
	private float objHeight = 2.7f;
	private const float multiplierScale = 0.19f;
	private const float offsetOrigem = 2.7f;
	private const float offset = 1.3f;

	private float distanceToMirror;
	private float distanceOrigem;

	bool repeatPositionLeft = false;
	bool repeatPositionRight = false;

	public GameObject objReal;
	public GameObject objImagemVirtual;
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
		const float boundaryRightLimit = 1.93f;


		float realPosition = objReal.transform.localPosition.x;

		float imgVirtualScaleX = objImagemVirtual.transform.localScale.x;
		float imgVirtualScaleY = objImagemVirtual.transform.localScale.y;
		float imgVirtualScaleZ = objImagemVirtual.transform.localScale.z;

		//Debug.Log (objReal.transform.localPosition.x);
		if (realPosition < boundaryRightLimit) {
			
			raioIncidenteParalelo.transform.localScale -= new Vector3 (offsetParalelo,0,0);

			raioIncidenteOrigem.transform.localRotation = Quaternion.Euler (new Vector3 (0, 0, Mathf.Asin(distanceToMirror/distanceOrigem) * multiplierAngle)); 
			raioIncidenteOrigem.transform.localScale = new Vector3 (1,distanceOrigem * multiplierScale,1);

			raioRefletido.transform.localRotation = Quaternion.Euler (new Vector3 (0, 0, -(Mathf.Asin(distanceToMirror/distanceOrigem) * multiplierAngle)+offset));

			tracejado.transform.localRotation = Quaternion.Euler (new Vector3 (0, 0, - (Mathf.Asin(distanceToMirror/distanceOrigem) * multiplierAngle) +offset + 180));

			objReal.transform.Translate (translationSpeed * Time.deltaTime, 0, 0);
			objImagemVirtual.transform.Translate (-virtualTranslationSpeed * Time.deltaTime, 0, 0);
			objImagemVirtual.transform.localScale = new Vector3 (imgVirtualScaleX, imgVirtualScaleY * ( 1 / virtualMultiplier), imgVirtualScaleZ);

		}
	}

	public void PositionLeftButton ()
	{
		const float boundaryLeftLimit = -2.6f; 

		float realPosition = objReal.transform.localPosition.x;

		float imgVirtualScaleX = objImagemVirtual.transform.localScale.x;
		float imgVirtualScaleY = objImagemVirtual.transform.localScale.y;
		float imgVirtualScaleZ = objImagemVirtual.transform.localScale.z;

		//Debug.Log (objReal.transform.localPosition.x);
		if (realPosition > boundaryLeftLimit) {

			raioIncidenteParalelo.transform.localScale += new Vector3 (offsetParalelo,0,0);

			raioIncidenteOrigem.transform.localRotation = Quaternion.Euler (new Vector3 (0, 0, Mathf.Asin(distanceToMirror/distanceOrigem) * multiplierAngle)); 
			raioIncidenteOrigem.transform.localScale = new Vector3 (1,distanceOrigem * multiplierScale,1);

			raioRefletido.transform.localRotation = Quaternion.Euler (new Vector3 (0, 0, -(Mathf.Asin(distanceToMirror/distanceOrigem) * multiplierAngle)+offset));

			tracejado.transform.localRotation = Quaternion.Euler (new Vector3 (0, 0, - (Mathf.Asin(distanceToMirror/distanceOrigem) * multiplierAngle) +offset + 180));

			objReal.transform.Translate (-translationSpeed * Time.deltaTime, 0, 0);

			objImagemVirtual.transform.Translate (virtualTranslationSpeed * Time.deltaTime, 0, 0);
			objImagemVirtual.transform.localScale = new Vector3 (imgVirtualScaleX, imgVirtualScaleY * virtualMultiplier, imgVirtualScaleZ);

			}

	}


}