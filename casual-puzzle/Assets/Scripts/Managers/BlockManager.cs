using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Blocks;
using Behaviors;
public class BlockManager : MonoBehaviour
{
    // Start is called before the first frame update
    private System.Random _random;
    private Blocks.BlockFactory _blockFactory;
    private float _initialXCoordinate;
    private float _initialYCoordinate;
    private float _initialZCoordinate;
    private int _rowNumber;
    private int _columnNumber;
    private int _colorNumber;
    private GameManager _gameManager;
    public GameObject gameManagerObjects; 

    private InputManager _inputManager; 
    public GameObject inputManagerObject; 
    private BlockCanvas _blockCanvas;
    public GameObject blockCanvasObject;
    public GameObject blockPrefab;
    public GameProperties gameProperties;
    void Start()
    {
        SetScriptsFromGameObjects();
        SetInitialCoordinates();
        InitializeBlockFactory();
        CreateCanvas();
    }
    private void SetScriptsFromGameObjects() {
        _gameManager = (GameManager) gameManagerObjects.GetComponent("GameManager");
        _inputManager = (InputManager) inputManagerObject.GetComponent("InputManager");
        _blockCanvas = (BlockCanvas) blockCanvasObject.GetComponent("BlockCanvas");
    }
    private void SetInitialCoordinates() {
        _initialXCoordinate = (Screen.width / 2) - ((gameProperties.RowLength * _inputManager.GetRowNumber()) / 2);
        _initialYCoordinate = (Screen.height / 2) - ((gameProperties.ColumnLength * _inputManager.GetColumnNumber()) / 2);
        _initialZCoordinate = gameProperties.ZValue;
    }

    private void InitializeBlockFactory() {
        ArrayList levelBoundaries = _inputManager.GetLevelBoundaries();
        ArrayList colors = new ArrayList{
            new Behaviors.Blue(levelBoundaries), 
            new Behaviors.Green(levelBoundaries), 
            new Behaviors.Pink(levelBoundaries), 
            new Behaviors.Purple(levelBoundaries), 
            new Behaviors.Red(levelBoundaries), 
            new Behaviors.Yellow(levelBoundaries) 
        };
        _blockFactory = new Blocks.BlockFactory(_gameManager, blockPrefab, colors, gameProperties.RowLength, 
            gameProperties.ColumnLength, gameProperties.BlockSpeed, gameProperties.NewBlockEmergingPointYCoordinate,
            _initialXCoordinate, _initialYCoordinate, _initialZCoordinate);
    }
    private void SetGameRules() {  
        _rowNumber = _inputManager.GetRowNumber();
        _columnNumber = _inputManager.GetColumnNumber();
        _colorNumber = _inputManager.GetColorNumber();
    }

    private int GetRandomColorIndex() {
        int randomIndex = _random.Next(_colorNumber);
        return randomIndex;
    }
    public void CreateCanvas() {
        _random = new System.Random();
        
        SetGameRules();
        _blockCanvas.SetRowAndColumn(_rowNumber,_columnNumber);
        for(int column = 0; column < _columnNumber; column++) {
            for(int row = 0; row < _rowNumber; row++) {
                int order = _blockCanvas.GetAndIncreaseOrder();

                Blocks.Block block = _blockFactory.CreateBlock(GetRandomColorIndex(), order, row, column);
                Debug.Log(block == null);
                _blockCanvas.SetBlock(row, column, block);
            }
        }
    }

    public void CreateBlockAboveCanvas(int row, int column) {
        int order = _blockCanvas.GetAndIncreaseOrder();
        Blocks.Block block = _blockFactory.CreateBlockFromEmegingPoint(GetRandomColorIndex(), order, row, column);
        _blockCanvas.SetBlock(row, column, block);
    }


}
