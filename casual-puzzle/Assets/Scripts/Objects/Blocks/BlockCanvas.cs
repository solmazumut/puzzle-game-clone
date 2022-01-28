using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Blocks 
{
    public class BlockCanvas : MonoBehaviour
    {
        private BlockManager _blockManager;
        public GameObject blockManagerObjects; 
        private Blocks.Block [,] _blocks;
        private int _rowNumber;
        private int _columnNumber;
        private int _order;
        private System.Random _random;

        void Awake() {
            _blockManager = (BlockManager) blockManagerObjects.GetComponent("BlockManager");
            _random = new System.Random();
            _order = 0;
            _rowNumber = 0;
            _columnNumber = 0;
            _blocks = new Blocks.Block[10,10];
        }
        void Start()
        {
            
        }

        public void SetRowAndColumn( int rowNumber, int columnNumber) {
            _rowNumber = rowNumber;
            _columnNumber = columnNumber;
        }

        public void SetBlock(int row, int column, Blocks.Block block) {
            _blocks[row, column] = block;
        }

        public Blocks.Block GetBlock(int row, int column) {
            return _blocks[row, column];
        }

        public void DestroyBlock(int row, int column) {
            Destroy(_blocks[row, column].gameObject);
            _blocks[row, column] = null;
        }

        public int GetAndIncreaseOrder() {
            int order = _order;
            _order += 1;
            return order;
        }

        private void ChangeLevelOfBlock(int row, int col, int level) {
            _blocks[row, col].SetLevel(level);
        }

        public void ChangeLevelAllBlocksToDefault() {
            for(int column = 0; column < _columnNumber; column++) {
                for(int row = 0; row < _rowNumber; row++) {
                    _blocks[row, column].ChangeLevelToDefault();
                }
            }
        }

        public void SetLevelOfBlockGrop(ArrayList blockGroup) {
            int level = blockGroup.Count;
            foreach (int[] blockCoordinates in blockGroup) {
                int row = blockCoordinates[0];
                int column = blockCoordinates[1];
                ChangeLevelOfBlock(row, column, level);
            }
        }

        public ArrayList GetCubeGroups() {
            ArrayList blockGroups = new ArrayList();
            int [,] visited = new int[10,10]; 
            int notVisited = 0; 
            for(int column = 0; column < _columnNumber; column++) {
                for(int row = 0; row < _rowNumber; row++) {
                    if(visited[row, column] == notVisited) { 
                        ArrayList blockGroup = ExploreBlocks(ref visited, row, column);
                        if(blockGroup.Count > 1) {
                            blockGroups.Add(blockGroup);
                        }
                    }
                }
            }
            return blockGroups;
        }

        private ArrayList ExploreBlocks(ref int[,] visited, int row, int column) {
            ArrayList blockGroup = new ArrayList();
            Stack<int[]> stack = new Stack<int[]>();
            int [] rowCoor = {-1, 1, 0, 0};
            int [] colCoor = {0, 0, -1, 1};
            int notVisited = 0;        
            stack.Push(new int[] {row, column});
            Behaviors.IColor color = _blocks[row, column].GetColor();
            while(stack.Count > 0) {
                int[] point = stack.Peek();
                stack.Pop();
                blockGroup.Add(point);
                visited[point[0], point[1]] = 1;
                for(int i = 0; i < 4; i++) {
                    int newRow = point[0] + rowCoor[i];
                    int newCol = point[1] + colCoor[i];
                    if(0 <= newRow && newRow< _rowNumber
                        && 0 <= newCol && newCol < _columnNumber) {
                        if(visited[newRow, newCol] == notVisited) {
                            if(color.IsSame(_blocks[newRow, newCol].GetColor())) {
                                stack.Push(new int[] {newRow, newCol});
                                visited[newRow, newCol] = 1;
                            }
                        }    
                    }
                }
            }
            return blockGroup;
        }

        public void SlideBlocksIntoSpaces() {
            for(int row = 0; row < _rowNumber; row++) {         
                for(int column = 0; column < _columnNumber; column++) {     
                    if(_blocks[row, column] != null) { 
                        int[] emptyCell  = GetEmptyCellInColumn(row, column);
                        if(emptyCell != null) {      
                            _blocks[row, column].Move(emptyCell[0], emptyCell[1]);
                            _blocks[emptyCell[0], emptyCell[1]] = _blocks[row, column];
                            _blocks[row, column] = null;
                        }
                    }
                } 
            }
        }

        private int[] GetEmptyCellInColumn(int row, int columnLimit) {
            int[] coordinates = null;
            for(int column = 0; column < columnLimit; column++) {
                if(_blocks[row, column] == null) {
                    coordinates = new int[] {row, column};
                    break;
                }
            }
            return coordinates;
        }

        public void CreateNewBlocksForEmptyCells() {
            for(int row = 0; row < _rowNumber; row++) {         
                for(int column = 0; column < _columnNumber; column++) {     
                    if(_blocks[row, column] == null) {
                        _blockManager.CreateBlockAboveCanvas(row, column);
                    }
                } 
            }
        }

        public void ChangeLocationOfTwoBlocksRandomly() {
            bool blocksAreSame = true;
            int first_row = 0;
            int first_col = 0;
            int second_row = 0;
            int second_col = 0;
            while(blocksAreSame) {
                first_row = _random.Next(_rowNumber);
                first_col = _random.Next(_columnNumber);
                second_row = _random.Next(_rowNumber);
                second_col = _random.Next(_columnNumber);
                blocksAreSame = first_row == second_row && first_col == second_col;
            }

            Block tmp = _blocks[first_row, first_col];
            _blocks[first_row, first_col] = _blocks[second_row, second_col];
            _blocks[second_row, second_col] = tmp;

            ResetOrder();
            _blocks[first_row, first_col].Move(second_row, second_col);
            _blocks[second_row, second_col].Move(first_row, first_col);
        }

        private void ResetOrder() {
            for(int column = 0; column < _columnNumber; column++) {
                for(int row = 0; row < _rowNumber; row++) {
                    _blocks[row, column].SetSortingOrder(_order++); 
                }
            }
        }

        public void ResetCanvas() {
            DestroyAllBlocks();
            _blockManager.CreateCanvas();
        }

        private void DestroyAllBlocks() {
            for(int column = 0; column < _columnNumber; column++) {
                for(int row = 0; row < _rowNumber; row++) {
                    DestroyBlock(row, column);
                }
            }
            _blocks = new Blocks.Block[10,10];
        }
    }

}
