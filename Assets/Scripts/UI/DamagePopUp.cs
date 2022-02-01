using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DamagePopUp : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    private float timerDelta;
    public float timer = 1;//time for the damage text to be removed
    public float damageSpeed = 0.2f;//speed for the thext
    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshProUGUI>();
    }
    public void Setup(float damageAmount)//is being called at start
    {
        textMesh.SetText("-" + damageAmount.ToString());
    }
    private void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + damageSpeed, transform.position.z);//makes the damage text go up
        timerDelta += Time.deltaTime;
        if (timerDelta >= timer) GameObject.Destroy(gameObject);//if the timer is done, destroys the object
    }
}
