using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private EnemyController enemyController;

    private int wave = 0;
    private bool pause = false;
    private float baseRoundTime = 120.0f;
    private float pauseTime = 60.0f;

    private float remainingTime;
    private float currentTime = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        newRound();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentTime += Time.fixedDeltaTime;

        if (!checkPauseEnemiesOnBoard())
            remainingTime -= Time.fixedDeltaTime;

        if (remainingTime <= 0) {
            StartCoroutine(betweenRound());
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

    public bool checkPauseEnemiesOnBoard() {
        return pause && enemyController.stillHasEnemies();
    }

    public bool checkPause() {
        return pause;
    }

    IEnumerator betweenRound() {
        enemyController.StopSpawning();
        pause = true;
        remainingTime = pauseTime;
        while (checkPauseEnemiesOnBoard())
            yield return new WaitForSeconds(1);
        yield return new WaitForSeconds(pauseTime);
        newRound();
    }

    private void newRound() {
        pause = false;
        enemyController.StartSpawning();
        wave++;
        remainingTime = baseRoundTime * (1f + (wave - 1) * 0.1f);
    }
}
