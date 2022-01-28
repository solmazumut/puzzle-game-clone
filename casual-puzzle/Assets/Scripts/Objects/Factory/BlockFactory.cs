using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Blocks
{
    public class BlockFactory
    {
        private GameManager _gameManager;
        private GameObject _blockPrefab;
        private ArrayList _colors;


        
        private float _rowLength;
        private float _columnLength;
        private float _blockSpeed;
        private float _newBlockEmergingPointYCoordinate;
        private float _initialXCoordinate;
        private float _initialYCoordinate;
        private float _initialZCoordinate;

        public BlockFactory(GameManager gameManager, GameObject blockPrefab, ArrayList colors, float rowLength, float columnLength, 
            float blockSpeed, float newBlockEmergingPointYCoordinate, float initialXCoordinate, float initialYCoordinate, float initialZCoordinate) {
            _gameManager = gameManager;
            _blockPrefab = blockPrefab;
            _colors = colors;
            _rowLength = rowLength;
            _columnLength = columnLength;
            _blockSpeed = blockSpeed;
            _newBlockEmergingPointYCoordinate = newBlockEmergingPointYCoordinate;
            _initialXCoordinate = initialXCoordinate;
            _initialYCoordinate = initialYCoordinate;
            _initialZCoordinate = initialZCoordinate;
        }

       

        public  Blocks.Block CreateBlock(int colorIndex, int order, float row, float column) {
            float yCoordinate =  _initialYCoordinate + _columnLength * column; 
            float xCoordinate = _initialXCoordinate + _rowLength * row;
            GameObject gameObjectFromBlockPrefab = GameObject.Instantiate(_blockPrefab, 
                new Vector3(xCoordinate, yCoordinate, _initialZCoordinate), Quaternion.identity) as GameObject;
            Blocks.Block block = gameObjectFromBlockPrefab.gameObject.GetComponent<Blocks.Block>();
            block.Initialize((Behaviors.IColor) _colors[colorIndex], order, _blockSpeed, _gameManager, _initialXCoordinate, _initialYCoordinate, _rowLength, _columnLength);
            return block;
        }

        public  Blocks.Block CreateBlockFromEmegingPoint(int colorIndex, int order, float row, float column) {
            float yCoordinate =  _initialYCoordinate + _columnLength * column + _newBlockEmergingPointYCoordinate; 
            float xCoordinate = _initialXCoordinate + _rowLength * row;
            GameObject gameObjectFromBlockPrefab = GameObject.Instantiate(_blockPrefab, 
                new Vector3(xCoordinate, yCoordinate, _initialZCoordinate), Quaternion.identity) as GameObject;
            Blocks.Block block = gameObjectFromBlockPrefab.gameObject.GetComponent<Blocks.Block>();
            block.Initialize((Behaviors.IColor) _colors[colorIndex], order, _blockSpeed, _gameManager, _initialXCoordinate, _initialYCoordinate, _rowLength, _columnLength);
            block.Move(row, column);
            return block;
        }
    }
}
