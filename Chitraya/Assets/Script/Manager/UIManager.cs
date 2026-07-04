using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> healthUI = new List<GameObject>();
    [SerializeField] private GameObject healthUIPrefab;
    [SerializeField] private Transform healthUISpawner;

    [SerializeField] private GameObject pausedPanel;
    [SerializeField] private GameObject settingPanel;


    [SerializeField] private bool playerDead;
    [SerializeField] private GameObject LosePanel;
    //[Header("Reference")]
    //[SerializeField] private PlayerScript player;

    public void PausedGame(InputAction.CallbackContext ctx)
    {
        if (ctx.started && !playerDead)
        {
            pausedPanel.SetActive(!pausedPanel.activeSelf);
            if (pausedPanel.activeSelf)
            {
                PausedSystem.instance.PausedGame();
                settingPanel.SetActive(false);
            }
            else
            {
                PausedSystem.instance.UnPausedGame();
                settingPanel.SetActive(false);
            }
        }

    }

    public void UpdateHealthUI()
    {
        // 1. Bersihkan semua objek hati yang ada di layar dan kosongkan List-nya
        foreach (GameObject heart in healthUI)
        {
            if (heart != null) Destroy(heart);
        }
        healthUI.Clear(); // Mengosongkan isi list agar ukurannya kembali jadi 0

        int curr = PlayerScript.instance.Health;

        // 2. Spawn ulang jumlah hati sesuai dengan HP saat ini
        for (int i = 0; i < curr; i++)
        {
            // Spawn prefab hati di dalam UI Spawner
            GameObject newHeart = Instantiate(healthUIPrefab, healthUISpawner);

            // PENTING: Masukkan objek hati baru ini ke dalam List agar bisa dihancurkan nanti
            healthUI.Add(newHeart);
        }
    }

    public void SettingButton()
    {
        settingPanel.SetActive(!settingPanel.activeSelf);
    }
    public void BackToMainMenu()
    {
        SceneController.instance.MainMenu();
    }

    public void PlayerDeath()
    {
        playerDead = true;
        LosePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Retry()
    {
        SceneController.instance.StartGame();
    }
}
