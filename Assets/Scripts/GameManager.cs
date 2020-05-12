using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static bool GameOver;
    private static bool _gameOver => GameOver;

    public static float difficulty = 1;
    private float _timer;
    private float _timerLimit;

    public GameObject GameOverObject;

    public Text gameOverScoreText;
    public Light pointLight1, pointLight2;

    // Start is called before the first frame update
    void Start()
    {
        _timer = 0;
        _timerLimit = 12f;
        difficulty = 1f;
        GameOverObject.SetActive(false);
        GameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_timer >= _timerLimit && difficulty < 2)
        {
            _timer = 0f;
            difficulty += .1f;
            Debug.Log(difficulty);
        }

        if (_gameOver)
        {
            GameOverObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(0);
            }
        }

        if (!_gameOver)
        {
            gameOverScoreText.text = Time.timeSinceLevelLoad.ToString();
            _timer += Time.deltaTime;
        }

        pointLight1.color = ChangeLights();
        pointLight2.color = ChangeLights();
    }

    private Color ChangeLights()
    {
        Color lightClolor = Color.Lerp(Color.blue, Color.magenta, Mathf.PingPong(Time.timeSinceLevelLoad, 1));
        return lightClolor;
    }
}
