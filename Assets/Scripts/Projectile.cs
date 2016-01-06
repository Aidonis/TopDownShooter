using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public LayerMask collisionMask;
    float speed = 10;
    float damage = 1;

    float lifetime = 1.5f;
    float collisionWidth = .1f;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, lifetime);

        Collider[] initialCollisions = Physics.OverlapSphere(transform.position, .1f, collisionMask);
        if(initialCollisions.Length > 0)
        {
            OnHitObject(initialCollisions[0]);
        }
	}

	// Update is called once per frame
	void Update () {
        float moveDistance = speed * Time.deltaTime;

        CheckCollisions(moveDistance);

        transform.Translate(Vector3.forward * moveDistance);

	}

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    void CheckCollisions(float moveDistance)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, moveDistance + collisionWidth, collisionMask, QueryTriggerInteraction.Collide))
        {
            OnHitObject(hit);
        }
    }

    void OnHitObject(RaycastHit hit)
    {
        IDamageable damgeableObject = hit.collider.GetComponent<IDamageable>();
        if(damgeableObject != null)
        {
            damgeableObject.TakeHit(damage, hit);
        }
        GameObject.Destroy(gameObject);
    }

    void OnHitObject(Collider c)
    {
        IDamageable damgeableObject = c.GetComponent<IDamageable>();
        if (damgeableObject != null)
        {
            damgeableObject.TakeDamage(damage);
        }
        GameObject.Destroy(gameObject);
    }
}