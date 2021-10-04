using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowPickupText : MonoBehaviour
{
    public GameObject uiObject;
    public bool active = false;

    public string powerupText;
    private Text UIText;

    void Start()
    {
        uiObject.SetActive(false);
        UIText = uiObject.GetComponent<Text>();
    }

    void Update()
    {
        UIText.text = powerupText;
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
