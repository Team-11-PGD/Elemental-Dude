using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Chris Huider
public class ShowPickupText : MonoBehaviour
{
    public GameObject uiObject;
    private Text UIText;

    [HideInInspector]
    public string powerupText;

    [SerializeField]
    private float showTextDuration = 1.5f;

    private void Start()
    {
        if (uiObject == null) uiObject = GameObject.Find("PickupText");
        uiObject.SetActive(false);
        UIText = uiObject.GetComponent<Text>();
    }

    public void StartText(bool useTimer = true)
    {
        uiObject.SetActive(true);
        UIText.text = powerupText;
        if (useTimer) StartCoroutine(TextDisableTimer());
    }

    private IEnumerator TextDisableTimer()
    {
        yield return new WaitForSeconds(showTextDuration);
        uiObject.SetActive(false);
    }
}
