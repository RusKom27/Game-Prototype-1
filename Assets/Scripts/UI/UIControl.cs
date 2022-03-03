using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    [SerializeField] private GameObject[] scoreLabels;
	[SerializeField] private GameObject gamePanel;
	[SerializeField] private GameObject pausePanel;
	[SerializeField] private GameObject gameOverPanel;
	[SerializeField] private GameObject menuPanel;
	[SerializeField] private GameObject player;
	[SerializeField] private GameObject field;

    private int scores = 0;

	private void Start()
	{
		Time.timeScale = 0;
		player.SetActive(false);
		field.SetActive(false);
		menuPanel.SetActive(true);
		gamePanel.SetActive(false);
		gameOverPanel.SetActive(false);
		pausePanel.SetActive(false);
	}

    public void UpdateScoreLabels()
	{
		for (int i = 0; i < scoreLabels.Length; i++)
		{
            scoreLabels[i].GetComponent<Text>().text = scores.ToString();
		}
	}

	public void IncreaseScores()
	{
		scores++;
		UpdateScoreLabels();
	}

    public void GameOver()
	{
		gameOverPanel.SetActive(true);
		gamePanel.SetActive(false);
		Time.timeScale = 0;
	}

	public void Pause()
	{
		pausePanel.SetActive(true);
		Time.timeScale = 0;
	}

	public void Continue()
	{
		pausePanel.SetActive(false);
		Time.timeScale = 1;
	}

	public void Exit()
	{
		menuPanel.SetActive(true);
		pausePanel.SetActive(false);
		gameOverPanel.SetActive(false);
		gamePanel.SetActive(false);
		player.SetActive(false);
		field.SetActive(false);
		Time.timeScale = 0;
	}

	public void NewGame()
	{
		player.SetActive(true);
		field.SetActive(true);
		gameOverPanel.SetActive(false);
		menuPanel.SetActive(false);
		gamePanel.SetActive(true);
		scores = 0;
		player.GetComponent<Player>().Respawn();
		UpdateScoreLabels();
		field.GetComponent<Field>().UpdateFood();
		Time.timeScale = 1;
	}
}
