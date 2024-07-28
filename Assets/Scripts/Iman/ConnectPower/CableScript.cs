using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableScript : MonoBehaviour
{
    int[] rotations = { 0, 90, 180, 270 };

    public int[] correctRotation;
    [SerializeField]
    private bool isPlaced = false;

    private int possibleRots = 1;
    private PowerManager powerManager;

    private void Awake()
    {
        powerManager = GameObject.Find("GameManager").GetComponent<PowerManager>();
    }

    private void Start()
    {
        possibleRots = correctRotation.Length;
        int rand = Random.Range(0, rotations.Length);
        transform.eulerAngles = new Vector3(0, 0, rotations[rand]);

        CheckPlacement();
    }

    private void OnMouseDown()
    {
        transform.Rotate(new Vector3(0, 0, 90));
        CheckPlacement();
    }

    private void CheckPlacement()
    {
        bool wasPlaced = isPlaced;

        int currentRotation = Mathf.RoundToInt(transform.eulerAngles.z) % 360;

        isPlaced = false;
        foreach (int correctRot in correctRotation)
        {
            if (currentRotation == correctRot)
            {
                isPlaced = true;
                break;
            }
        }

        if (isPlaced && !wasPlaced)
        {
            powerManager.CorrectMove();
        }
        else if (!isPlaced && wasPlaced)
        {
            powerManager.WrongMove();
        }
    }
}

