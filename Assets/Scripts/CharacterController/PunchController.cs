using UnityEngine;

public class PunchController : MonoBehaviour {

    Transform parent;
    float force;
    Vector2 direction;

    public void SetValues(Transform armParent)
    {
        parent = armParent;
    }

    public void SetCurrentValues(Vector2 armDirection, float armForce)
    {
        direction = armDirection;
        force = armForce;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform != parent)
        {
            CharacterLogic foe = col.GetComponent<CharacterLogic>();
            if (foe)
            {
                foe.Push(direction, force);
            }
        }
    }
}
