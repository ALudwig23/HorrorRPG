using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiedMonster : MonoBehaviour
{
    public string[] BodyParts = { "Head" , "LeftLeg" , "RightLeg" };

    private int _maxBodyParts = 3;

    public int MaxBodyParts { get { return _maxBodyParts; } }



}
