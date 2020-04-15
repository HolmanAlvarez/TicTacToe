using UnityEngine;
using UnityEngine.UI;

public class HudManager : Singleton<HudManager> 
{
    [SerializeField] GameObject topMenu;
    [SerializeField] Animator optionsAnimator;
    [SerializeField] GameObject infoPanel;
    [SerializeField] GameObject audioPanel;
    [SerializeField] GameObject creditsPanel;
    [SerializeField] GameObject leaderboardPanel;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject gamePanel;
    [SerializeField] GameObject winnerPanel;
    [SerializeField] Text[] boxesGame;
    [SerializeField] Text infoText;
    [SerializeField] Text scoreText;
    [SerializeField] Color[] playersColor;

    private int playerWins , cpuWins;

	private void Start()
	{
        playerWins = 0;
        cpuWins = 0;
	}

    public void ShowPanel(string panelName)
    {
        switch(panelName)
        {
            case "Info":        infoPanel.SetActive(true);  break;
            case "Audio":       audioPanel.SetActive(true); break;
            case "Credits":     creditsPanel.SetActive(true);   break;
            case "Leaderboard": leaderboardPanel.SetActive(true);   break;
            case "Options":     optionsAnimator.SetBool("IsHidden", !optionsAnimator.GetBool("IsHidden"));  break;
            default:            gamePanel.SetActive(true);  break;
        }
    }

    public void AssignBox(int boxPosition, string playerFigure)
    {
        boxesGame[boxPosition].text = playerFigure;
        boxesGame[boxPosition].color = playerFigure == "O" ? playersColor[0] : playersColor[1];
    }

    public void UpdateScore(int winner)
    {
        if (winner == 0)
            playerWins++;
        else
            cpuWins++;

        infoText.text = winner == 0 ? "Winner: Player" : "Winner: CPU";
        infoText.color = winner == 0 ? playersColor[0] : playersColor[1];
        scoreText.text = winner == 0 ? playerWins.ToString() + " : 0" : "0 : " + cpuWins.ToString();
        winnerPanel.SetActive(true);
    }

    public void UpdateInfo(int turn)
    {
        infoText.text = turn == 0 ? "Turn: Player" : "Turn: CPU";
        infoText.color = turn == 0 ? playersColor[0] : playersColor[1];
    }

    public void WinnerPanel()
    {
        winnerPanel.SetActive(true);
    }

    public void Reset(string buttonName)
	{
        winnerPanel.SetActive(false);
        UpdateInfo(0);

        for (int i = 0; i < boxesGame.Length; i++)
        {
            boxesGame[i].text = string.Empty;
            boxesGame[i].GetComponentInParent<Button>().interactable = true;
        }

        if (buttonName.Contains("Yes"))
            GameManager.Instance.StartGame();
        else
        {
            playerWins = 0;
            cpuWins = 0;
            scoreText.text = "0 : 0";
            gamePanel.SetActive(false);
        }
	}
}