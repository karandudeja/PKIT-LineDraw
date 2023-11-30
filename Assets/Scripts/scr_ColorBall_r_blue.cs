using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_ColorBall_r_blue : MonoBehaviour
{
    [Header("Pen Properties")]
    public Transform tip;
    public Material drawingMaterial;
    public Material tipMaterial;
    [Range(0.01f, 0.1f)]
    public float penWidth = 0.01f;
    public Color[] penColors;

    [Header("Hands and Grabbable")]
    public OVRGrabber rightHand;
    public OVRGrabber leftHand;
    public OVRGrabbable grabbable;

    private LineRenderer currentDrawing;
    private List<Vector3> positions = new();
    private int index;
    private int currentColorIndex;

    private bool isRightHandDrawing;

    private GameObject[] rightTagObjects;
    public GameObject rightHandTextObjectContainer;

    private void Start()
    {
        currentColorIndex = 0;
        tipMaterial.color = penColors[currentColorIndex];
        isRightHandDrawing = false;
    }

    private void Update()
    {
        
        // While user holds the left hand trigger
        if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > 0.0f)
        {
            // Assign left controller's position and rotation to cube
            transform.position = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
            transform.rotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch);
            isRightHandDrawing = true;

            rightHandTextObjectContainer.SetActive(true);
        }
        else
        {
            isRightHandDrawing = false;

            rightHandTextObjectContainer.SetActive(false);
        }

        transform.position = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch) + new Vector3(-0.02f, 0, 0.05f);

        if (isRightHandDrawing)
        {
            DrawLinesFunc();
        }
        else if (currentDrawing != null)
        {
            currentDrawing = null;
        }
        else if (OVRInput.GetUp(OVRInput.RawButton.A) || OVRInput.GetUp(OVRInput.RawButton.B))
        {
            //SwitchColorsFunc();

            Invoke("DeleteLastRightFunc", 0.33f);
        }

    }

    private void DrawLinesFunc()
    {
        if (currentDrawing == null)
        {
            index = 0;
            currentDrawing = new GameObject().AddComponent<LineRenderer>();
            currentDrawing.material = drawingMaterial;
            currentDrawing.startColor = currentDrawing.endColor = penColors[currentColorIndex];
            currentDrawing.startWidth = currentDrawing.endWidth = penWidth;
            currentDrawing.positionCount = 1;
            currentDrawing.SetPosition(0, tip.transform.position);

            currentDrawing.name = "right-line";
            currentDrawing.tag = "right";
        }
        else
        {
            var currentPosition = currentDrawing.GetPosition(index);
            if (Vector3.Distance(currentPosition, tip.transform.position) > 0.01f)
            {
                index++;
                currentDrawing.positionCount = index + 1;
                currentDrawing.SetPosition(index, tip.transform.position);
            }
        }
    }

    private void SwitchColorsFunc()
    {
        if (currentColorIndex == penColors.Length - 1)
        {
            currentColorIndex = 0;
        }
        else
        {
            currentColorIndex++;
        }
        tipMaterial.color = penColors[currentColorIndex];
    }

    private void DeleteLastRightFunc()
    {
        rightTagObjects = GameObject.FindGameObjectsWithTag("right");
        if (rightTagObjects.Length > 0)
        {
            Destroy(rightTagObjects[rightTagObjects.Length - 1]);
        }
    }
}
