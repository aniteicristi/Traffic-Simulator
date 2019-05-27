using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]

public class SimulationControl : MonoBehaviour
{
    public Button PlayPauseButton;
    public Sprite PauseButton;
    public Sprite playButton;
    public bool IsTimeStopped = true;
    public float x=1;
    private InterfaceManager manager;

    [SerializeField]
    private Text textTimeScale;

    [SerializeField]
    private Analytics analytics;

    public void Awake()
    {
        Time.timeScale = 0;
        textTimeScale.text = "Speed x" + x;
        manager = FindObjectOfType<InterfaceManager>();
    }

    public void PlayPause()
    {
        if (manager.buildMode)
            return;

        if (IsTimeStopped)
        {
            Time.timeScale = x;
            IsTimeStopped = false;
            PlayPauseButton.image.overrideSprite = PauseButton;

            if (!analytics.gameObject.activeSelf)
              analytics.gameObject.SetActive(true);

            analytics.Init();
        }
        else
        {
            x = Time.timeScale;
            Time.timeScale = 0.0F;
            IsTimeStopped = true;
            PlayPauseButton.image.overrideSprite = playButton;
            analytics.StopWatchCoroutine();
        }
    }

    public void Restart()
    {
        SpawnCache.DespawnAllCars();
        analytics.Reset();
    }

    public void SpeedUp()
    {
        if (manager.buildMode || IsTimeStopped)
            return;

        var timeScale = Mathf.Min(Time.timeScale * 2, 64);

        Time.timeScale = timeScale;
        textTimeScale.text = "Speed x" + timeScale.ToString();
    }

    public void SlowDown()
    {
        if (manager.buildMode || IsTimeStopped)
            return;

        var timeScale = Mathf.Max(Time.timeScale / 2, 1);

        Time.timeScale = timeScale;
        textTimeScale.text = "Speed x" + timeScale.ToString();
    }

}