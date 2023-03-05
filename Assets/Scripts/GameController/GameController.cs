using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Player player;

    [SerializeField]
    private EnemyController enemyController;

    [SerializeField]
    private DoorsController doorsController;

    private int wave = 0;
    private bool pause = false;
    private float baseRoundTime = 5.0f;
    private float pauseTime = 5.0f;

    private float remainingTime;
    private float currentTime = 1.0f;

    private Coroutine newRoundCoroutine = null;

    // Start is called before the first frame update
    void Start()
    {
        newRoundCoroutine = StartCoroutine(newRound());
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        if (!checkPauseEnemiesOnBoard())
            remainingTime -= Time.deltaTime;

        if (remainingTime <= 0.0f) {
            if (!pause)
                StartCoroutine(betweenRound());
            else if (newRoundCoroutine == null)
                newRoundCoroutine = StartCoroutine(newRound());
        }
    }

    public int getWave() {
        return wave;
    }

    public float getCurrentTime() {
        return currentTime;
    }

    public float getRemainingTime() {
        return remainingTime;
    }

    public bool pleaseReturnToMainAreaCheck() {
        return remainingTime <= 0.0f && playerNotInMainArena();
    }

    public bool checkPauseEnemiesOnBoard() {
        return pause && enemyController.stillHasEnemies();
    }

    public bool checkPause() {
        return pause;
    }

    public int getPoints() {
        return enemyController.getPoints();
    }

    public bool playerNotInMainArena() {
        return !player.getInMainArena();
    }

    IEnumerator betweenRound() {
        pause = true;
        enemyController.StopSpawning();
        remainingTime = pauseTime;

        while (checkPauseEnemiesOnBoard())
            yield return new WaitForEndOfFrame();

        doorsController.UnlockCurrentDoor();
        doorsController.UnblockEverything();
    }

    IEnumerator newRound() {
        while (playerNotInMainArena())
            yield return new WaitForEndOfFrame();

        doorsController.LockEverything();
        enemyController.StartSpawning();
        wave++;
        remainingTime = baseRoundTime * (1f + (wave - 1) * 0.1f);
        pause = false;
        newRoundCoroutine = null;
    }
}
