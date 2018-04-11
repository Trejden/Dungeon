using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float Radius = 1f;

    Transform Player;

    bool HasInteracted = false;

    private void Start()
    {
        Player = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        float Distance = Vector3.Distance(Player.position, transform.position);

        if (!HasInteracted && Distance < Radius)
        {
            HasInteracted = true;
            Interract();
        }
        else if (HasInteracted && Distance >= Radius)
            HasInteracted = false;
    }

    public virtual void Interract()
    {
        Debug.Log("Interract");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, Radius);
    }
}
