using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsController : MonoBehaviour
{
    [SerializeField]
    private GameObject player;


    // Distance from mid one door to dead center
    private float midDoorDistanceDeadCenter = 4.95f;
    private bool open = false;


    private Transform door1;
    private Rigidbody door1Rb;

    private Transform door2;
    private Rigidbody door2Rb;


    // Start is called before the first frame update
    void Start()
    {
        door1 = transform.GetChild(1);
        door1Rb = door1.GetComponent<Rigidbody>();

        door2 = transform.GetChild(2);
        door2Rb = door2.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!open) {
            Vector3 doorsDeadCenterOnFloor = new Vector3(transform.position.x, player.transform.position.y, transform.position.z) + transform.forward * midDoorDistanceDeadCenter;
            float distanceToDoor = Vector3.Distance(player.transform.position, doorsDeadCenterOnFloor);

            if (distanceToDoor < 10.0f) {
                if (Input.GetKeyDown("e")) {
                    //! Create explosion if have money check

                    open = true;
                    OpenDoors();
                }
            }
        }

        // Clamp Door Rotation Values
        door1.localEulerAngles = Vector3.up * Mathf.Clamp(door1.localEulerAngles.y, 0f, 110f);
        door2.localEulerAngles = Vector3.up * Mathf.Clamp(door2.localEulerAngles.y, 70f, 180f);
    }

    void OpenDoors() {
        float toque = 1000.0f;
        door1Rb.constraints &= ~RigidbodyConstraints.FreezeRotationY;
        door1Rb.AddTorque(Vector3.up * toque);


        door2Rb.constraints &= ~RigidbodyConstraints.FreezeRotationY;
        door2Rb.AddTorque(Vector3.down * toque);
    }
}
