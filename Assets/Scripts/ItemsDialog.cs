﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsDialog : MonoBehaviour
{
    [SerializeField] private int buttonNumber = 15;
    [SerializeField] private ItemButton itemButton;

    private ItemButton[] _itemButtons;

    // Start is called before the first frame update
    private void Start()
    {
        //初期状態は非表示
        gameObject.SetActive(false);

        //アイテム欄を必要な分だけ複製
        for(var i = 0; i < buttonNumber - 1; i++)
        {
            Instantiate(itemButton, transform);
        }

        //子要素のitemButtonを一括取得、保持しておく
        _itemButtons = GetComponentsInChildren<ItemButton>();
    }

    //アイテム欄の表示/非表示を切り替えます
    public void Toggle()
    {
        gameObject.SetActive(!gameObject.activeSelf);

        if(gameObject.activeSelf)
        {
            //表示された場合はアイテム欄をリフレッシュする
            for(var i = 0; i < buttonNumber; i++)
            {
                //各アイテムボタンに所持アイテム情報をセット
                _itemButtons[i].OwnedItem = OwnedItemsData.Instance.OwnedItems.Length > i
                    ? OwnedItemsData.Instance.OwnedItems[i]
                    : null;
            }
        }
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}
}
