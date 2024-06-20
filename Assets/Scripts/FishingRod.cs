using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using InputDevice = UnityEngine.InputSystem.InputDevice;
using PDollarGestureRecognizer;
using System.IO;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class FishingRod : MonoBehaviour
{
	public XRNode inputSource;
	public InputHelpers.Button inputButton;
	public float inputThreshold = 0.1f;
	public Transform movementSource;
	public GameObject debugPrefab;
	public bool creationMode = true;
	public string newGestureName;

	private List<Gesture> trainingSet = new List<Gesture>();
	private bool isMoving = false;
	private List<Vector3> positionList = new List<Vector3>();
	public float newPositionThresholdDistance = 0.05f;

	public float recognitionThreshold = 0.6f;

	[Serializable]
	public class UnityStringEvent : UnityEvent<string> { }

	public UnityStringEvent OnRecognized;
	
	private bool isEquipped;
    private bool isFishingAvailable = true;
	private bool isCasted = false;
	private bool isPulling;
	private bool isRipplesAvailable;
	private float minTimeRipple = 5f;
	private float maxTimeRipple = 10f;
	private float timeToGetFishBeforeGone = 5f;

	public GameObject ripplesPrefab;
	public GameObject baitPrefab;
	private GameObject bait;
	private GameObject ripples;
	public GameObject endOfRope;
	public GameObject startOfRope;
	public GameObject StartOfRod;
	public LineRenderer ropeLR;
	
	private Transform baitPosition;
	public XRGrabInteractable interactable;
	public GameObject player;

	private Coroutine _waitForFishToAppear;
	
	private void Start()
	{
		string[] gesturFiles = Directory.GetFiles(Application.persistentDataPath, "*.xml");
		foreach (var item in gesturFiles)
		{
			trainingSet.Add(GestureIO.ReadGestureFromFile(item));
		}
	}

	private void Update()
	{
		InputHelpers.IsPressed(InputDevices.GetDeviceAtXRNode(inputSource), inputButton, out bool isPressed,
			inputThreshold);

		if (!isMoving && isPressed && isEquipped)
		{
			StartMovement();
		}
		else if (isMoving && !isPressed && isEquipped)
		{
			EndMovement();
		}
		else if (isMoving && isPressed && isEquipped)
		{
			UpdateMovement();
		}

		if (bait)
		{
			ropeLR.positionCount = 2;
			ropeLR.SetPosition(0, StartOfRod.transform.position);
			ropeLR.SetPosition(1, bait.transform.position);
		}
	}

	void StartMovement()
	{
		isMoving = true;
		positionList.Clear();
		positionList.Add(movementSource.position);

		if (debugPrefab)
		{
			Destroy(Instantiate(debugPrefab, movementSource.position, quaternion.identity), 3);
		}
	}

	void EndMovement()
	{
		isMoving = false;
		
		//create the gesture from the position list
		Point[] pointArray = new Point[positionList.Count];

		for (int i = 0; i < positionList.Count; i++)
		{
			Vector2 screenPoint = Camera.main.WorldToScreenPoint(positionList[i]);
			pointArray[i] = new Point(screenPoint.x, screenPoint.y, 0);
		}

		Gesture newGesture = new Gesture(pointArray);

		if (creationMode)
		{
			newGesture.Name = newGestureName;
			trainingSet.Add(newGesture);

			string fileName = Application.persistentDataPath + "/" + newGestureName + ".xml";
			GestureIO.WriteGesture(pointArray, newGestureName, fileName);
		}
		else
		{
			Result result = PointCloudRecognizer.Classify(newGesture, trainingSet.ToArray());
			Debug.Log(result.GestureClass + result.Score);
			if (result.Score > recognitionThreshold)
			{
				OnRecognized.Invoke(result.GestureClass);
				if (result.GestureClass == "fish")
				{
					ThrowBait();
				}

				if (result.GestureClass == "getfish")
				{
					GetBait();
				}
			}
		}
	}

	void UpdateMovement()
	{
		Vector3 lastPosition = positionList[positionList.Count - 1];
		if (Vector3.Distance(movementSource.position, lastPosition) > newPositionThresholdDistance)
		{
			positionList.Add(movementSource.position);
			
			if (debugPrefab)
			{
				Destroy(Instantiate(debugPrefab, movementSource.position, quaternion.identity), 3);
			}
		}
	}
	
	public void EventEquipRod()
	{
		if (interactable.interactorsSelecting.Count == 1 && interactable.interactorsSelecting[0] is XRDirectInteractor)
		{
			isEquipped = true;
		}
		else if(interactable.interactorsSelecting.Count == 0 || interactable.interactorsSelecting.Count == 1 && interactable.interactorsSelecting[0] is not XRDirectInteractor)
		{
			isEquipped = false;
		}
	}

	public void ThrowBait()
	{
		if (isFishingAvailable)
		{
			isCasted = true;
			bait = Instantiate(baitPrefab, StartOfRod.transform.position, quaternion.identity);
			bait.GetComponent<Rigidbody>().AddRelativeForce(StartOfRod.transform.forward * 5f, ForceMode.Impulse);
			isFishingAvailable = false;
			_waitForFishToAppear = StartCoroutine(WaitForFishToAppear());
		}
	}

	public void GetBait()
	{
		if (isCasted)
		{
			bait.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
			bait.GetComponent<Rigidbody>().AddForce(Vector3.up * 5f, ForceMode.Impulse);
			StartCoroutine(DestroyBaitAfterTime());
		}
	}

	IEnumerator DestroyBaitAfterTime()
	{
		yield return new WaitForSeconds(1f);
		StopCoroutine(_waitForFishToAppear);
		ropeLR.positionCount = 0;
		Destroy(bait);
		if (ripples)
		{
			Destroy(ripples);
			Debug.Log("YOU OBTAINED A FISH"); //Add fish to stock here
		}
		isCasted = false;
		isFishingAvailable = true;
		isRipplesAvailable = false;
	}

	IEnumerator WaitForFishToAppear()
	{
		while (isCasted)
		{
			yield return new WaitForSeconds(Random.Range(minTimeRipple,maxTimeRipple));
			ripples = Instantiate(ripplesPrefab, bait.transform.position, quaternion.identity);
			isRipplesAvailable = true;
			yield return new WaitForSeconds(timeToGetFishBeforeGone);
			isRipplesAvailable = false;
			Destroy(ripples);
		}
	}
}
