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
    }

    public void LockEverything() {
        if (doorControllers != null)
            for (int i = 0; i < transform.childCount; i++) {
                doorControllers[i].Lock();
            }
    }

    public void UnlockCurrentDoor() {
        doorControllers[currentDoor].Unlock();
    }

    public void UnblockEverything() {
        for (int i = 0; i <= currentDoor; i++) {
            doorControllers[i].Unblock();
        }
    }

    private void Update() {
        // Logic to handle current door
        if (doorControllers[currentDoor].fullOpen()) {
            currentDoor++;
            if (currentDoor < doorControllers.Length) {
                UnlockCurrentDoor();
            }
        }
    }
}
