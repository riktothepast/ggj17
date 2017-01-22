using UnityEngine;

public class SimpleScroll : MonoBehaviour {

    public Vector2 speed = Vector2.zero;

    private Vector2 offset = Vector2.zero;
    private Material material;
    public bool autoMovement = true;

    // Use this for initialization
    void Start()
    {
        material = GetComponent<Renderer>().material;

        offset = material.GetTextureOffset("_MainTex");
    }

    public void MoveCamera(bool value)
    {
        autoMovement = value;
    }

    // Update is called once per frame
    void Update()
    {
        if (!autoMovement)
        {
            return;
        }
        offset += speed * Time.deltaTime;
        material.SetTextureOffset("_MainTex", offset);
    }
}
