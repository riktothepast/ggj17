using UnityEngine;

public class BatOldMan : MonoBehaviour {

    public float forcePush;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player")) {
            col.GetComponent<CharacterLogic>().Push((transform.position - col.transform.position).normalized, forcePush);
        }
    }
}
