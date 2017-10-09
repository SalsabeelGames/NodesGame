using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultDialog : MonoBehaviour {
    public float imageXScale;
    public float imageYScale;

    // Use this for initialization
    void Start () {
        imageXScale = 0.16f;
        imageYScale = 0.82f;
        this.gameObject.transform.Find("front_image").gameObject.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
    }
	
	// Update is called once per frame
	void Update () {
        if (this.gameObject.activeSelf)
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
}
