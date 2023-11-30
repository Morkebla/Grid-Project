using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Selection : MonoBehaviour
{
   RaycastHit hit;

    Transform highlight;
    [SerializeField] Material selectedMat;
    [SerializeField] Material highlightMaterial;
    [SerializeField] private Material _originalMat;

    private void Update()
    {
        MouseOverHighlight();

    }

    private void MouseOverHighlight()
    {
        if (highlight != null)
        {
            highlight.GetComponent<MeshRenderer>().material = _originalMat;
            highlight = null;
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            highlight = hit.transform;
            if (highlight.GetComponent<MeshRenderer>().material != highlightMaterial)
            {
                _originalMat = hit.collider.GetComponent<MeshRenderer>().material;// saves the original color into variable
                highlight.GetComponent<MeshRenderer>().material = highlightMaterial;// changes the color of the object to the default color of highlightMaterial.
            }
            else
            {
                highlight = null;
            }
        }
    }
   
}
