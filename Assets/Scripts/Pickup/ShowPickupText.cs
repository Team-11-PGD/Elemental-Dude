using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowPickupText : MonoBehaviour
{
    public GameObject uiObject;
    [HideInInspector]
    public string powerupText;

    [SerializeField]
    private float showTextDuration = 4;

    private Text UIText;

    private void Start()
    {
        if (uiObject == null) uiObject = GameObject.Find("PickupText"); // O(n)
        uiObject.SetActive(false);
        UIText = uiObject.GetComponent<Text>();
    }

    public void StartText(bool useTimer = true)
    {
        uiObject.SetActive(true);
        UIText.text = powerupText;
        if (useTimer)
        {
            StartCoroutine(TextDisableTimer());
        }
    }

    private IEnumerator TextDisableTimer()
    {
        yield return new WaitForSeconds(showTextDuration);
        uiObject.SetActive(false);
    }
}
