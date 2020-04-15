using UnityEngine;

public class ButtonsController : MonoBehaviour 
{
    public enum ButtonType { Audio, Close, Credits, Exit, Info, Leaderboard, MiniMaxAlfaBeta, MiniMax, MiniMaxSuspension, Options, TicTacToe, YesNot }
    public ButtonType buttonType;

    [SerializeField] UnityEngine.UI.Button _button;

	// Use this for initialization
	void Start () 
    {
        if (_button.Equals(null))
            _button = GetComponent<UnityEngine.UI.Button>();

        _button.onClick.AddListener(HandleButtonClicked);
	}
	
	void HandleButtonClicked()
    {
        switch(buttonType)
        {
            case ButtonType.Audio:              HudManager.Instance.ShowPanel("Audio"); break;
            case ButtonType.Credits:            HudManager.Instance.ShowPanel("Credits");  break;
            case ButtonType.Close:              if (transform.parent.gameObject.name.Equals("GamePanel"))
                                                    HudManager.Instance.Reset(string.Empty);
                                                transform.parent.gameObject.SetActive(false);   break;
            case ButtonType.Exit:               GameManager.Instance.Quit();    break;
            case ButtonType.Info:               HudManager.Instance.ShowPanel("Info");    break;
            case ButtonType.Leaderboard:        HudManager.Instance.ShowPanel("Leaderboard"); break;
            case ButtonType.MiniMaxAlfaBeta:    InitGame(3);  break;
            case ButtonType.MiniMax:            InitGame(1);  break;
            case ButtonType.MiniMaxSuspension:  InitGame(2);  break;
            case ButtonType.Options:            HudManager.Instance.ShowPanel("Options");  break;
            case ButtonType.TicTacToe:          GameManager.Instance.PlayerPlay(gameObject.name);   
                                                ButtonDisable();    break;
            case ButtonType.YesNot:             HudManager.Instance.WinnerPanel();
                                                HudManager.Instance.Reset(gameObject.name);    break;
        }
    }

    void InitGame(int value)
    {
        HudManager.Instance.ShowPanel("Game");
        GameManager.Instance.AlgorithmType = value;
        GameManager.Instance.StartGame();
    }

    void ButtonDisable()
    {
        GetComponent<UnityEngine.UI.Button>().interactable = false;
    }
}