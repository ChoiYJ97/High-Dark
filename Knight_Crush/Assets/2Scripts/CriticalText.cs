using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriticalText : MonoBehaviour
{
    public TextMesh Textmesh;
    SpriteRenderer Renderer;
    // Start is called before the first frame update
    void Start()
    {
        this.Renderer = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float alpha = this.Textmesh.color.a;

        alpha -= Time.deltaTime;

        Textmesh.color = new Color(Textmesh.color.r, Textmesh.color.g, Textmesh.color.b, alpha);
        Renderer.color = new Color(Renderer.color.r, Renderer.color.g, Renderer.color.b, alpha);

        if (alpha <= 0)
            Destroy(this.gameObject);
    }
}
