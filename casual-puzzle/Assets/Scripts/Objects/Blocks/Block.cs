using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Behaviors;

namespace Blocks
{
    public class Block : MonoBehaviour
    {
        private Behaviors.IColor _color;
        private MeshRenderer _renderer;
        private SortingGroup _sortingGroup;
        private GameManager _gameManager; 
        private Vector3 _target;
        private bool _isOnTarget;
        private float _speed;

        private float _initialXCoordinate;
        private float _initialYCoordinate;
        private float _rowLength;
        private float _columnLength;


        public void Initialize(Behaviors.IColor color, int order, float speed, GameManager gameManager,
         float initialXCoordinate, float initialYCoordinate, float rowLength, float columnLength) {
            SetBoardProperties(initialXCoordinate, initialYCoordinate, rowLength, columnLength);
            SetColor(color);
            SetSortingOrder(order);
            SetSpeed(speed);
            SetGameManager(gameManager);
            SetRenderer();
            SetTarget(this.transform);
            ChangeLevelToDefault();
        }
        private void SetBoardProperties(float initialXCoordinate, float initialYCoordinate, float rowLength, float columnLength) {
            _initialXCoordinate = initialXCoordinate;
            _initialYCoordinate = initialYCoordinate;
            _rowLength = rowLength;
            _columnLength = columnLength;
        }

        private void SetTarget(Transform transformObject) {
            _isOnTarget = true;
            _target = transformObject.position;
        }
        private void SetGameManager(GameManager gameManager) {
            _gameManager = gameManager;
        }
        private void SetSpeed(float speed) {
            _speed = speed;
        }
        private void SetColor(Behaviors.IColor color) {
            _color = color;
        }

        public void SetSortingOrder(int order) {
            if(_sortingGroup == null) {
                _sortingGroup = GetComponent<SortingGroup>();
            }
            _sortingGroup.sortingOrder = order;
        }

        public int GetSortingOrder() {
            if(_sortingGroup == null) {
                _sortingGroup = GetComponent<SortingGroup>();
            }
            return _sortingGroup.sortingOrder;
        }

        private void SetRenderer() {
            _renderer = GetComponent<MeshRenderer>();
            
        }

        public void SetLevel(int level) {
            _renderer.sharedMaterial = _color.GetMaterial(level);
        }

        public void ChangeLevelToDefault() {
            _renderer.sharedMaterial = _color.GetMaterial(0);
        }

        public IColor GetColor() {
            return _color;
        }

        public bool IsEqual(Blocks.Block block) {
            bool result = false;
            Debug.Log(block == null);
            if(block.GetSortingOrder() == _sortingGroup.sortingOrder) {
                result = true;
            }
            return result;
        } 

        public void Move(float targetRow, float targetColumn) {
            float yCoordinate =  _initialYCoordinate + _columnLength * targetColumn; 
            float xCoordinate = _initialXCoordinate + _rowLength * targetRow;
            _target = new Vector3(xCoordinate, yCoordinate, transform.position.z);
            _isOnTarget = false;
        }
        

        void OnMouseDown(){
            _gameManager.CubeClicked(this);
        }

        void Update() {
            if(!_isOnTarget) {
                transform.position = Vector3.MoveTowards(transform.position, _target, _speed* Time.deltaTime);
                if(_target.x == transform.position.x && _target.y == transform.position.y) {
                    _isOnTarget = true;
                }
            }
        }
    }
}

