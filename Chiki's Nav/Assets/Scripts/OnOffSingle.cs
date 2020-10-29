using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffSingle : MonoBehaviour
{
    public GameObject target;

    public void OnOffObject()
    {
        target.SetActive(!target.activeInHierarchy);
    }
}
