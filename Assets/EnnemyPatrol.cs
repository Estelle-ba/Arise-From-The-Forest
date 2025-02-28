using UnityEngine;

public class EnnemyPatrol : MonoBehaviour
{
    public float speed;
    public Transform[] waypoints;
    public Transform target;
    private int destPoint;
    public SpriteRenderer graphics;
    void Start()
    {
        target = waypoints[0];
        
    }
    
    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        //si l'ennemi est presque arrivé
        if (Vector3.Distance(transform.position, target.position) < 0.3f)
        {
            
            destPoint = (destPoint + 1) % waypoints.Length;
            target = waypoints[destPoint];
            graphics.flipX = !graphics.flipX;
        }
    }
}
