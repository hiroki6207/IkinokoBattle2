using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Button pauseButton;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Button resumeButton;

    [SerializeField] private Button itemsButton;
    [SerializeField] private Button recipeButton;
    [SerializeField] private ItemsDialog itemsDialog;
    // Start is called before the first frame update
    private void Start()
    {
        pausePanel.SetActive(false); //ポーズのパネルは初期状態では非表示にしておく

        pauseButton.onClick.AddListener(Pause);
        resumeButton.onClick.AddListener(Resume);
        itemsButton.onClick.AddListener(ToggleItemsDialog);
        recipeButton.onClick.AddListener(ToggleRecipeDialog);
    }

    //ゲームを一時停止します
    private void Pause()
    {
        Time.timeScale = 0; //Time.timeScaleで時間の流れの速さを決める。0だと時間が停止する
        pausePanel.SetActive(true);
    }

    //ゲームを再開します
    private void Resume()
    {
        Time.timeScale = 1; //また時間が流れるようにする
        pausePanel.SetActive(false);
    }

    //アイテムウインドウを開閉します
    private void ToggleItemsDialog()
    {
        itemsDialog.Toggle();
    }

    //レシピウインドウを開閉します
    private void ToggleRecipeDialog()
    {
        //
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}
}
