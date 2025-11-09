using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Manager : MonoBehaviour
{
    //Datas
    public int set; //0 = white, 1 = gray, 2 = blue, 3 = green, 4 = yellow, 5 = orange, 6 = red.
    public int mode; //0 = timed, 1 = 2 player.
    public float finalTime;
    public float topScore;

    //CusVar
    public int current; //0 = main, 1 = gameSelect, 2 = gameMode, 3 = gameLevel, 4 = about, 5 = settings, 6 = game, 7 = endGame, 8 = pauseGame, 9 = settingsGame

    //Desks
    public Desk[] desks;

    //Main
    public GameObject mainMenuScene;
    //Start
    public GameObject gameSelectionScene;
    public GameObject gameModeScene;
    public GameObject levelSelectScene;
    //Game
    public GameObject gameScene;
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public TextMeshProUGUI currentTime;
    public TextMeshProUGUI currentTurn;
    //public TextMeshProUGUI currentFirstScore;
    //public TextMeshProUGUI currentSecondScore;
    public GameObject timerObj;
    public GameObject turnObj;
    //End
    public GameObject endGameScene;
    public GameObject endTimerObj;
    public GameObject endScoreObj;
    public TextMeshProUGUI timer;
    public TextMeshProUGUI firstScore;
    public TextMeshProUGUI secondScore;
    //About
    public GameObject aboutScene;
    //Settings
    public GameObject settingsScene;
    public Slider musicSlider;
    public Slider musicSliderAlt;
    public Slider volumeSlider;
    public Slider volumeSliderAlt;
    public Toggle fullscreenToggle;
    public Toggle fullscreenToggleAlt;

    //Labels
    public Image modeLabel;
    public Sprite[] modeLabels;
    public Image setLabel;
    public Sprite[] setLabels;

    //Backgrounds
    public Sprite mainBG;
    public Sprite endBG;
    public Sprite[] deskBGs;
    public Image background;

    //Clips
    public AudioSource click;
    public AudioSource cardChooseCorrect;
    public AudioSource cardChooseIncorrect;
    public AudioSource endGame;

    //GameSettings
    public AudioMixer master;
    public int volume;
    public int music;
    public bool fullscreen;
    void Start()
    {
        Save save = SaveSystem.Load();

        if(save != null)
        {
            fullscreen = save.fullscreen;
            music = save.musicVolume;
            volume = save.effectsVolume;
            topScore = save.topTime;
        }
        else
        {
            fullscreen = true;
            music = 0;
            volume = 0;
            topScore = 0;
        }

        ApplySettings();

        background.sprite = mainBG;
        background.gameObject.SetActive(true);

        current = 0;
        mainMenuScene.SetActive(true);
        gameSelectionScene.SetActive(false);
        gameModeScene.SetActive(false);
        levelSelectScene.SetActive(false);
        gameScene.SetActive(false);
        endGameScene.SetActive(false);
        aboutScene.SetActive(false);
        settingsScene.SetActive(false);
    }
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (current == 0)
                QuitButton();
            else if (current == 1)
                ReturnToMainMenuButton();
            else if (current == 2)
                QuitModeorLevelSelection();
            else if (current == 3)
                QuitModeorLevelSelection();
            else if (current == 4)
                ReturnToMainMenuButton();
            else if (current == 5)
                SaveSettings();
            else if (current == 6)
                PauseGame();
            else if (current == 7)
                ReturnToMainMenuButton();
            else if (current == 8)
                ContinueGame();
            else if (current == 9)
                PauseGame();
            else
                Debug.Log("No scenes set for index: " + current);
        }
    }
    void ApplySettings()
    {
        fullscreenToggle.isOn = fullscreen;
        musicSlider.value = music;
        volumeSlider.value = volume;

        fullscreenToggleAlt.isOn = fullscreen;
        musicSliderAlt.value = music;
        volumeSliderAlt.value = volume;

        Screen.fullScreen = fullscreen;
        master.SetFloat("Volume_Music", music);
        master.SetFloat("Volume_Effects", volume);
    }
    //Buttons
    public void GameSelectionButton()
    {
        click.Play();
        background.sprite = mainBG;

        current = 1;
        mainMenuScene.SetActive(false);
        gameSelectionScene.SetActive(true);
    }
    public void AboutButton()
    {
        click.Play();
        background.sprite = mainBG;

        current = 4;
        mainMenuScene.SetActive(false);
        aboutScene.SetActive(true);
    }
    public void SettingsButton()
    {
        click.Play();
        background.sprite = mainBG;

        ApplySettings();

        current = 5;
        mainMenuScene.SetActive(false);
        settingsScene.SetActive(true);
    }
    public void SaveSettings()
    {
        click.Play();

        SaveSystem.Save(Mathf.RoundToInt(topScore), fullscreen, music, volume);

        background.sprite = mainBG;
        current = 0;
        mainMenuScene.SetActive(true);
        settingsScene.SetActive(false);
    }
    public void PauseGame()
    {
        current = 8;
        click.Play();
        pauseMenu.SetActive(true);
        SaveSystem.Save(Mathf.RoundToInt(topScore), fullscreen, music, volume);
        settingsMenu.SetActive(false);
        desks[set].run = false;
    }
    public void ContinueGame()
    {
        current = 6;
        click.Play();
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        desks[set].run = true;
    }
    public void SettingsGame()
    {
        current = 9;
        click.Play();
        settingsMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }
    public void SettingsValueChanged()
    {
        if(current == 9)
        {
            music = Mathf.RoundToInt(musicSliderAlt.value);
            volume = Mathf.RoundToInt(volumeSliderAlt.value);
            fullscreen = fullscreenToggleAlt.isOn;

            musicSlider.value = musicSliderAlt.value;
            volumeSlider.value = volumeSliderAlt.value;
            fullscreenToggle.isOn = fullscreenToggleAlt.isOn;
        }
        if(current == 5)
        {
            music = Mathf.RoundToInt(musicSlider.value);
            volume = Mathf.RoundToInt(volumeSlider.value);
            fullscreen = fullscreenToggle.isOn;

            musicSliderAlt.value = musicSlider.value;
            volumeSliderAlt.value = volumeSlider.value;
            fullscreenToggleAlt.isOn = fullscreenToggle.isOn;
        }

        ApplySettings();
    }
    public void QuitButton()
    {
        click.Play();
        SaveSystem.Save(Mathf.RoundToInt(topScore), fullscreen, music, volume);
        Application.Quit();
    }
    public void ReturnToMainMenuButton()
    {
        click.Play();
        current = 0;
        background.sprite = mainBG;
        mainMenuScene.SetActive(true);
        gameSelectionScene.SetActive(false);
        gameModeScene.SetActive(false);
        levelSelectScene.SetActive(false);
        pauseMenu.SetActive(false);
        gameScene.SetActive(false);
        endGameScene.SetActive(false);
        aboutScene.SetActive(false);
        settingsScene.SetActive(false);

        endTimerObj.SetActive(false);
        endScoreObj.SetActive(false);

        desks[set].run = false;
        desks[set].time = 0f;
        for (int i = 0; i < desks[set].transform.childCount; i++)
            Destroy(desks[set].transform.GetChild(i).gameObject);
    }
    public void ModeButton()
    {
        click.Play();
        background.sprite = mainBG;

        current = 2;
        gameModeScene.SetActive(true);
    }
    public void SelectMode(int i)
    {
        click.Play();
        mode = i;
        modeLabel.sprite = modeLabels[i];
        current = 1;
        gameModeScene.SetActive(false);
    }
    public void SetButton()
    {
        click.Play();
        background.sprite = mainBG;

        current = 3;
        levelSelectScene.SetActive(true);
    }
    public void SelectSet(int i)
    {
        click.Play();
        set = i;
        setLabel.sprite = setLabels[i];
        current = 1;
        levelSelectScene.SetActive(false);
    }
    public void QuitModeorLevelSelection()
    {
        click.Play();
        current = 1;
        gameModeScene.SetActive(false);
        levelSelectScene.SetActive(false);
    }
    public void StartButton()
    {
        click.Play();

        current = 6;
        gameSelectionScene.SetActive(false);
        background.sprite = deskBGs[set];
        endGameScene.SetActive(false);
        gameScene.SetActive(true);
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);

        if (mode == 0)
        {
            timerObj.SetActive(true);
            turnObj.SetActive(false);
        }
        else if (mode == 1)
        {
            timerObj.SetActive(false);
            turnObj.SetActive(true);
        }
        else
            Debug.Log("No modes set for index:" + mode);

        Desk validDesk = desks[set];
        validDesk.Generate();
    }
    public void FinishGame()
    {
        finalTime = Mathf.RoundToInt(desks[set].time);
        firstScore.text = desks[set].player1.ToString();
        secondScore.text = desks[set].player2.ToString();

        desks[set].run = false;
        desks[set].time = 0f;
        desks[set].player1 = 0;
        desks[set].player2 = 0;
        for (int i = 0; i < desks[set].transform.childCount; i++)
            Destroy(desks[set].transform.GetChild(i).gameObject);

        current = 7;
        gameScene.SetActive(false);
        endGameScene.SetActive(true);

        endTimerObj.SetActive(false);
        endScoreObj.SetActive(false);
        if (mode == 0)
            endTimerObj.SetActive(true);
        else if (mode == 1)
            endScoreObj.SetActive(true);

        background.sprite = endBG;
        timer.text = Mathf.RoundToInt(finalTime).ToString();
        endGame.Play();
    }
    public void PlaySound(int sound)
    {
        AudioSource[] sources = new AudioSource[4];
        sources[0] = click;
        sources[1] = cardChooseCorrect;
        sources[2] = cardChooseIncorrect;
        sources[3] = endGame;
        sources[sound].Play();
    }
}