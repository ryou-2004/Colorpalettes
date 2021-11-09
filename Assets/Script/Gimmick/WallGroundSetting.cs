using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGroundSetting : MonoBehaviour
{
    private GameObject[] wallObj;
    private void Start()
    {
        wallObj = GameObject.FindGameObjectsWithTag("WallGround");
        foreach (GameObject obj in wallObj)
        {
            obj.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
        StartCoroutine(BodyTypeChange());
    }
    IEnumerator BodyTypeChange()
    {
        yield return new WaitForSeconds(1);
        foreach (GameObject obj in wallObj)
        {
            obj.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        }
    }
}
