using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class scr_DeleteAllLines : MonoBehaviour
{
    // Start is called before the first frame update

    private GameObject[] leftTagObjects;
    private GameObject[] rightTagObjects;
    public GameObject actionUpdateTextHolder;
    public TextMeshProUGUI actionUpdateText;

    private void Start()
    {
        funcHideDeleteAllText();
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.Touch) > 0.0f)
        {
            rightTagObjects = GameObject.FindGameObjectsWithTag("right");
            leftTagObjects = GameObject.FindGameObjectsWithTag("left");
            if (rightTagObjects.Length > 0 || leftTagObjects.Length > 0)
            {
                Invoke("funcDeleteAllLines", 0.1f);
            }
        }
    }

    private void funcDeleteAllLines() {

        if (leftTagObjects.Length > 0)
        {
            int leftQty = leftTagObjects.Length;
            while (leftQty >= 0)
            {
                Destroy(leftTagObjects[leftQty - 1]);
                leftQty--;
            }
        }

        if (rightTagObjects.Length > 0)
        {
            int rightQty = rightTagObjects.Length;
            while (rightQty >= 0)
            {
                Destroy(rightTagObjects[rightQty - 1]);
                rightQty--;
            }
        }

        StartCoroutine(funcShowDeleteAllText());
    }

    IEnumerator funcShowDeleteAllText()
    {
        actionUpdateTextHolder.SetActive(true);
        actionUpdateText.text = "Deleted All Lines";
        Invoke("funcHideDeleteAllText", 2.5f);
        yield return null;
    }

    private void funcHideDeleteAllText()
    {
        actionUpdateTextHolder.SetActive(false);
        actionUpdateText.text = "...";
    }

}
