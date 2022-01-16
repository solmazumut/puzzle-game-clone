using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.UI;
using System;
public class GameManager : MonoBehaviour
{
    int [] propertiesArray = new int[6];
    int ROW_NUMBER_INDEX = 0, COLUMN_NUMBER_INDEX = 1, COLOR_NUMBER_INDEX = 2, FIRST_LEVEL_LIMIT_INDEX = 3, SECOND_LEVEL_LIMIT_INDEX = 4, THIRD_LEVEL_LIMIT_INDEX = 5; 
    
    public GameObject blockPrefab;
    public Camera mainCamera;
    private int BLUE_INDEX = 0, GREEN_INDEX = 1, PINK_INDEX = 2, PURPLE_INDEX = 3, RED_INDEX = 4, YELLOW_INDEX = 5;
    
    private BehaviorInterface [] colorBehaviors;
    private float Z_VALUE = 5, COLUMN_VALUE = .87f, ROW_VALUE = .87f;
    private float initial_x_coor, initial_y_coor;
    private int layer_index = 0;
    private System.Random random;

    private float newBlockEmergingPoint;
    private float blockSpeed;

    private Block [,] blocks = new Block[10,10];

    ArrayList blockGroups;
    // Start is called before the first frame update
    void Start()
    {
        getProperties();
        setupGame();
        setBlockLevels();
        checkDeadlockAndShuffle();
            
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void checkDeadlockAndShuffle() {
        int thereIsNoGroup = 0;
        bool isDeadlock = blockGroups.Count == thereIsNoGroup;
        bool changed = false;
        while(isDeadlock) {
            changed = true;
            int first_row = random.Next(propertiesArray[ROW_NUMBER_INDEX]);
            int first_col = random.Next(propertiesArray[COLUMN_NUMBER_INDEX]);
            int second_row = random.Next(propertiesArray[ROW_NUMBER_INDEX]);
            int second_col = random.Next(propertiesArray[COLUMN_NUMBER_INDEX]);
            bool blocksAreSame = first_row == second_row && first_col == second_col;
            if(!blocksAreSame) {
                Block tmp = blocks[first_row, first_col];
                blocks[first_row, first_col] = blocks[second_row, second_col];
                blocks[second_row, second_col] = tmp;
                setBlockLevels();
                isDeadlock = blockGroups.Count == thereIsNoGroup;
            }
        }
        if(changed) {
            layer_index = 0;
            for(int column = 0; column < propertiesArray[COLUMN_NUMBER_INDEX]; column++) {
                for(int row = 0; row < propertiesArray[ROW_NUMBER_INDEX]; row++) {
                    blocks[row, column].setSortingOrder(layer_index++);
                    blocks[row, column].Move(initial_x_coor + row*ROW_VALUE, initial_y_coor + column*COLUMN_VALUE);
                }
            }
        }
         
    }

    private Block createBlock(int row, int column) {
        GameObject newBlock = Instantiate(blockPrefab, 
                    new Vector3(initial_x_coor + row*ROW_VALUE, initial_y_coor + column*COLUMN_VALUE, Z_VALUE), Quaternion.identity);
        Block block = newBlock.gameObject.GetComponent<Block>();
        block.initializeTarget();
        block.setGameManager(this);
        block.setColorBehavior(colorBehaviors[random.Next(propertiesArray[COLOR_NUMBER_INDEX])]);
        block.setSortingOrder(layer_index++);
        block.setSpeed(blockSpeed);
        return block;
    }

    private Block createBlockAboveCanvas(int row, int column) {
        GameObject newBlock = Instantiate(blockPrefab, 
                    new Vector3(initial_x_coor + row*ROW_VALUE, initial_y_coor + newBlockEmergingPoint + column*COLUMN_VALUE, Z_VALUE), Quaternion.identity);
        Block block = newBlock.gameObject.GetComponent<Block>();
        block.setGameManager(this);
        block.setColorBehavior(colorBehaviors[random.Next(propertiesArray[COLOR_NUMBER_INDEX])]);
        block.setSortingOrder(layer_index++);
        block.setSpeed(blockSpeed);
        block.Move(initial_x_coor + row*ROW_VALUE, initial_y_coor + column*COLUMN_VALUE);
        return block;
    }

    private void createNewBlocks() {
        for(int column = 0; column < propertiesArray[COLUMN_NUMBER_INDEX]; column++) {
            for(int row = 0; row < propertiesArray[ROW_NUMBER_INDEX]; row++) {
                if(blocks[row, column] == null) {
                    Block block = createBlockAboveCanvas(row, column);
                    blocks[row, column] = block;
                }
            }
        }
    }

    private void setupGame() {
        blockSpeed = 12;
        newBlockEmergingPoint = 10;
        initial_x_coor = (Screen.width / 2) - ((ROW_VALUE*propertiesArray[ROW_NUMBER_INDEX]) / 2);
        initial_y_coor = (Screen.height / 2) - ((COLUMN_VALUE*propertiesArray[COLUMN_NUMBER_INDEX]) / 2);
        random = new System.Random();
        colorBehaviors = new BehaviorInterface [] {new BlueBehavior(), new GreenBehavior(), new PinkBehavior(), new PurpleBehavior(), new RedBehavior(), new YellowBehavior()};
        for(int column = 0; column < propertiesArray[COLUMN_NUMBER_INDEX]; column++) {
            for(int row = 0; row < propertiesArray[ROW_NUMBER_INDEX]; row++) {
                Block block = createBlock(row, column);
                blocks[row, column] = block;
            }
        }

    }
    private void getProperties() {
        propertiesArray[ROW_NUMBER_INDEX] = PlayerPrefs.GetInt("rowNumber");
        propertiesArray[COLUMN_NUMBER_INDEX] = PlayerPrefs.GetInt("columnNumber");
        propertiesArray[COLOR_NUMBER_INDEX] = PlayerPrefs.GetInt("colorNumber");
        propertiesArray[FIRST_LEVEL_LIMIT_INDEX] = PlayerPrefs.GetInt("firstLevelLimit");
        propertiesArray[SECOND_LEVEL_LIMIT_INDEX] = PlayerPrefs.GetInt("secondLevelLimit");
        propertiesArray[THIRD_LEVEL_LIMIT_INDEX] = PlayerPrefs.GetInt("thirdLevelLimit");
        Debug.Log("firstLevelLimit- " + propertiesArray[FIRST_LEVEL_LIMIT_INDEX] );
        Debug.Log("secondLevelLimit- " + propertiesArray[SECOND_LEVEL_LIMIT_INDEX] );
        Debug.Log("thirdLevelLimit- " + propertiesArray[THIRD_LEVEL_LIMIT_INDEX] );
    }

    private void setBlockLevels() {
        blockGroups = new ArrayList();
        int [,] visited = new int[10,10]; 
        int notVisited = 0; 
        for(int column = 0; column < propertiesArray[COLUMN_NUMBER_INDEX]; column++) {
            for(int row = 0; row < propertiesArray[ROW_NUMBER_INDEX]; row++) {
                
                if(visited[row, column] == notVisited) { 
                    ArrayList blockGroup = exploreBlocks(ref visited, row, column);
                    if(blockGroup.Count > 1) {
                        blockGroups.Add(blockGroup);
                        setLevelImage(blockGroup);
                    }
                }
            }
        }
    }
    private ArrayList exploreBlocks(ref int[,] visited, int row, int column) {
        ArrayList blockGroup = new ArrayList();
        Stack<int[]> stack = new Stack<int[]>();
        int [] rowCoor = {-1, 1, 0, 0};
        int [] colCoor = {0, 0, -1, 1};
        int notVisited = 0;        

        blocks[row, column].setDefault();
        stack.Push(new int[] {row, column});
        BehaviorInterface behavior = blocks[row, column].getColorBehavior();
        while(stack.Count > 0) {
            int[] point = stack.Peek();
            stack.Pop();
            blockGroup.Add(point);
            visited[point[0], point[1]] = 1;
            for(int i = 0; i < 4; i++) {
                int newRow = point[0] + rowCoor[i];
                int newCol = point[1] + colCoor[i];
                if(0 <= newRow && newRow< propertiesArray[ROW_NUMBER_INDEX]
                    && 0 <= newCol && newCol < propertiesArray[COLUMN_NUMBER_INDEX]) {
                    if(visited[newRow, newCol] == notVisited) {
                        if(behavior.isSame(blocks[newRow, newCol].getColorBehavior())) {
                            stack.Push(new int[] {newRow, newCol});
                            visited[newRow, newCol] = 1;
                        }
                    }    
                }
            }
        }
        return blockGroup;
    }
    private void setLevelImage(ArrayList blockGroup) {
        int size = blockGroup.Count;
        if(size > propertiesArray[THIRD_LEVEL_LIMIT_INDEX]) {
            foreach (int [] blockCoordinates in blockGroup)
            {
                blocks[blockCoordinates[0], blockCoordinates[1]].setThirdLevel();
            }
        } 
        else if (size > propertiesArray[SECOND_LEVEL_LIMIT_INDEX]) {
            foreach (int [] blockCoordinates in blockGroup)
            {
                blocks[blockCoordinates[0], blockCoordinates[1]].setSecondLevel();
            }
        }
        else if (size > propertiesArray[FIRST_LEVEL_LIMIT_INDEX]){
            foreach (int [] blockCoordinates in blockGroup)
            {
                blocks[blockCoordinates[0], blockCoordinates[1]].setFirstLevel();
            }
        }
    }

    public void findAndDestroy(Block block) {
        for(int group = 0; group < blockGroups.Count; group++) {
            ArrayList blockGroup = (ArrayList) blockGroups[group];
            bool founded = false;
            for(int coordinateIndex = 0; coordinateIndex < blockGroup.Count; coordinateIndex++) {
                int[] coordinates = (int[]) blockGroup[coordinateIndex];
                if(blocks[coordinates[0], coordinates[1]].isEqual(block)) {
                    blockGroups.RemoveAt(group);
                    destroyBlockGroup(blockGroup);
                    founded = true;
                    scrollBlocksInArray();
                    createNewBlocks();
                    setBlockLevels();
                    checkDeadlockAndShuffle();
                    break;
                }
            }
            if(founded) {
                break;
            }
        }
    }
    private void destroyBlockGroup(ArrayList group) {
        foreach (int[] coordinates in group)
        {
            Destroy(blocks[coordinates[0], coordinates[1]].gameObject);
            blocks[coordinates[0], coordinates[1]] = null;
        }
    }
    private void scrollBlocksInArray() {
        for(int row = 0; row < propertiesArray[ROW_NUMBER_INDEX]; row++) {
            
            for(int column = 0; column < propertiesArray[COLUMN_NUMBER_INDEX]; column++) {     
                if(blocks[row, column] != null) { 
                    int[] emptyCell  = getEmptyCellInColumn(row, column);
                    if(emptyCell != null) {
                        
                    blocks[row, column].Move(initial_x_coor + emptyCell[0]*ROW_VALUE, initial_y_coor + emptyCell[1]*COLUMN_VALUE);
                    blocks[emptyCell[0], emptyCell[1]] = blocks[row, column];
                    blocks[row, column] = null;
                    }
                }
            }
            
            
        }
    }
    

    private int[] getEmptyCellInColumn(int row, int columnLimit) {
        int[] coordinates = null;
        for(int column = 0; column < columnLimit; column++) {
            if(blocks[row, column] == null) {
                coordinates = new int[] {row, column};
                break;
            }
        }
        return coordinates;
    }

}
