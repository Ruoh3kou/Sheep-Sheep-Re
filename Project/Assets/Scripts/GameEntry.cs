using System;
using System.Collections;
using System.Collections.Generic;
using SheepSheep;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class GameEntry : MonoBehaviour
{
    public GameObject canvas;
    public GameObject layerRootPrefab;
    public GameObject cardPrefab;

    private readonly LevelLogic _levelLogic = new LevelLogic();
    private GameObject[] layerRoots;

    private void Start()
    {
        _levelLogic.GenerateLevel();

        // 创建不同层的card root，用于渲染顺序
        layerRoots = new GameObject[Define.MAX_Z];
        for (int i = layerRoots.Length - 1; i >= 0; i--)
        {
            layerRoots[i] = Instantiate(layerRootPrefab, canvas.transform);
        }
        // 创建新的card
        for (int z = 0; z < Define.MAX_Z; z++)
        {
            for (int y = 0; y < Define.MAX_Y; y++)
            {
                for (int x = 0; x < Define.MAX_X; x++)
                {
                    int blockType = _levelLogic.GetCardType(x, y, z);
                    if (blockType > 0)
                    {
                        CreateCard(x, y, z, blockType);
                    }
                }
            }
        }
    }

    private void CreateCard(int x, int y, int z, int typeId)
    {
        var card = Instantiate(cardPrefab, layerRoots[z].transform);
        card.name = $"card_{x}_{y}_{z}_{typeId}";
        var rect = card.GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(0, 1);
        rect.anchorMax = new Vector2(0, 1);
        rect.anchoredPosition3D = new Vector3(
            x * 5f + Define.CARD_X * 0.5f,
            -(y * Define.CARD_Y * 0.5f + Define.CARD_Y * 0.5f),
            z);

        var image = card.GetComponent<RawImage>();
        Sprite cardImage;
        if (z != 0)
            cardImage = AssetLoader.LoadGrayCard(typeId);
        else
            cardImage = AssetLoader.LoadCard(typeId);
        image.texture = cardImage.texture;
    }
}