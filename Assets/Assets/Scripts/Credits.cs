using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Credits : MonoBehaviour, IPointerClickHandler
{

    float imageXScale;
    float imageYScale;
    bool isCreditVisiable = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isCreditVisiable)
        {
            if (imageXScale <= 0.37f)
            {
                imageXScale += Time.deltaTime * 0.90f;
            }

            if (imageYScale <= 0.82f)
            {
                imageYScale += Time.deltaTime * 0.90f;
            }
            this.gameObject.transform.Find("front_image").gameObject.transform.localScale = new Vector3(imageXScale, imageYScale, 0);
        }
    }

    public void showCredits()
    {
        imageXScale = 0.16f;
        imageYScale = 0.82f;
        this.gameObject.transform.Find("front_image").gameObject.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
        isCreditVisiable = true;
        this.gameObject.SetActive(true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        isCreditVisiable = false;
        this.gameObject.SetActive(false);
    }
}
