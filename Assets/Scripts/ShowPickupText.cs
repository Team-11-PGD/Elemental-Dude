using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPickupText : MonoBehaviour
{
    public GameObject uiObject;
    public bool active = false;

    // Start is called before the first frame update
    void Start()
    {
        uiObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            StartCoroutine("ShowText");
        }
    }

    IEnumerator ShowText()
    {
        yield return new WaitForSeconds(4);
        uiObject.SetActive(false);
        active = false;
    }
}
