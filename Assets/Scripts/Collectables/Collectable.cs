using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private Transform player;
    private float minCatchDistance = 5.0f;
    private Rigidbody rb;
    private float catchForce = 2500.0f;
    private float dropForce = 300f;
    private float dropTorque = 100f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("PlayerModel").transform;
        rb = GetComponent<Rigidbody>();
        rb.AddForce(Vector3.up * dropForce + Vector3.forward * Mathf.Cos(Random.Range(0, 180)) * dropForce / 4f + Vector3.right * Mathf.Cos(Random.Range(0, 180)) * dropForce / 4f);
        rb.AddTorque(Vector3.one * dropTorque);
        transform.parent = GameObject.FindGameObjectWithTag("CollectablesEmpty").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance < minCatchDistance) {
            Vector3 forceToPlayer = (player.position - transform.position).normalized;

            rb.AddForce(forceToPlayer * Time.fixedDeltaTime * catchForce);
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "PlayerModel")
            Destroy(gameObject);
    }
}
