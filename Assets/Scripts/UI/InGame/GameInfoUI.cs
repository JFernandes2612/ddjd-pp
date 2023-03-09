using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameInfoUI : MonoBehaviour
{
    [SerializeField]
    private GameController gameData;
    private Label gameInfoUI;

    // Start is called before the first frame update
    void Start()
    {
        gameInfoUI = (Label)GetComponent<UIDocument>().rootVisualElement.Q("GameInfo").Q("GameInfoText");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        gameInfoUI.text = string.Format("<b>Wave:</b> {0}\n<b>Score:</b> {1}\n<b>Time left:</b> {2}\n<b>Total time:</b> {3}",
                                        gameData.checkPause() ? string.Format("{0} <color=#00ffffff>→</color> {1}", gameData.getWave(), gameData.getWave() + 1) : gameData.getWave(),
                                        gameData.getPoints(), gameData.checkPauseEnemiesOnBoard() ? "<color=red>Clear Enemies on Board!</color>" : gameData.pleaseReturnToMainAreaCheck() ? "<color=red>Please return right now to the Main Arena!</color>" : secondsToTime(gameData.getRemainingTime()),
                                        secondsToTime(gameData.getCurrentTime()));
    }

    private string secondsToTime(float seconds)
    {
        return string.Format("{0}:{1:D2}", (int)seconds / 60, (int)seconds % 60);
    }
}
