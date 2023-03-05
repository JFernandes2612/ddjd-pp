using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsController : MonoBehaviour
{
    private DoorController[] doorControllers;

    private int currentDoor = 0;

    private void Start() {
        doorControllers = new DoorController[transform.childCount];
        for (int i = 0; i < transform.childCount; i++) {
            doorControllers[i] = transform.GetChild(i).GetChild(1).gameObject.GetComponent<DoorController>();
        }

        doorControllers[currentDoor].Unlock();
    }

    private void Update() {
        if (doorControllers[currentDoor].fullOpen()) {
            currentDoor++;
            if (currentDoor < doorControllers.Length) {
                doorControllers[currentDoor].Unlock();
            }
        }
    }
}
