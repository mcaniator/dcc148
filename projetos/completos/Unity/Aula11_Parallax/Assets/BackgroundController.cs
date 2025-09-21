using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    [SerializeField] private bool useQuad;
    [SerializeField] private float skySpeed;
    [SerializeField] private Transform leftEdge;
    [SerializeField] private GameObject skyQuad;
    [SerializeField] private GameObject skySprite;

    private Transform skyObject;
    private Material skyMaterial;
    private float offset = 0;
    private float xBegin;

    private float quadSpeedFactor = 0.01f;

    void Start()
    {
        if(useQuad)
        {
            skyQuad.SetActive(true);
            skyObject = transform.GetChild(0);
            skyMaterial = skyObject.GetComponent<MeshRenderer>().material;
        }
        else
        {
            skySprite.SetActive(true);
            skyObject = transform.GetChild(1);
            xBegin = skyObject.transform.position.x;
        }
        Debug.Log(xBegin);
    }

    void Update()
    {
        if(useQuad)
        {
            skyObject.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0);
            offset += skySpeed * Time.deltaTime * quadSpeedFactor;
            skyMaterial.SetTextureOffset("_MainTex", new Vector2(offset, 0));
        }
        else
        {
            float xNext = skyObject.transform.position.x - skySpeed * Time.deltaTime;
            float xDiff = xNext - leftEdge.transform.position.x;
            // Debug.Log(xDiff);
            if(xDiff <= 0)
                xNext = xBegin + xDiff;

            Debug.Log(skyObject);
            skyObject.transform.position = new Vector3(xNext, skyObject.transform.position.y, 0);
        }
    }
}
