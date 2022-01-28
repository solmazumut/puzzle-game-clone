using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private ArrayList _blockGroups;
    private Blocks.BlockCanvas _blockCanvas;
    public GameObject blockCanvasObject;

    private void Initialize() {
        _blockCanvas = (Blocks.BlockCanvas) blockCanvasObject.GetComponent("BlockCanvas");
        _blockGroups = new ArrayList();
    }
   
    void Start()
    {
        Initialize();
        ChangeLevelOfSuitableBlocks();
        MixUntilNoDeadlocks();
    }

    private void ChangeLevelOfSuitableBlocks() {
        if(_blockCanvas == null ) {
            Debug.Log("boş");
        }
        _blockCanvas.ChangeLevelAllBlocksToDefault();
        _blockGroups = _blockCanvas.GetCubeGroups();
        foreach (ArrayList blockGroup in _blockGroups) {
            _blockCanvas.SetLevelOfBlockGrop(blockGroup);
        }
    }

    public void CubeClicked(Blocks.Block block) {
        int canCubeBeExploded = CanCubeBeExploded(block);
        int cannotBeExploded = -1;
        if(canCubeBeExploded != cannotBeExploded) {
            int blockGroupIndex = canCubeBeExploded;
            ArrayList blockGroup = (ArrayList) _blockGroups[blockGroupIndex];
            DestroyBlockGroup(blockGroup);
            UpdateCanvasWhenBlocksExplode();
        }
    }

    private void UpdateCanvasWhenBlocksExplode() {
        _blockCanvas.SlideBlocksIntoSpaces();
        _blockCanvas.CreateNewBlocksForEmptyCells();
        ChangeLevelOfSuitableBlocks(); 
        bool isItDeadlock = CheckDeadlock();
        if(isItDeadlock) {
            MixUntilNoDeadlocks();
        }
    }

    private int CanCubeBeExploded(Blocks.Block block) {
        int result = -1;
        int index = 0;
        foreach (ArrayList blockGroup in _blockGroups) {
            foreach (int[] blockCoordinates in blockGroup) {
                int row = blockCoordinates[0];
                int column = blockCoordinates[1];
                Blocks.Block blockInGroup = _blockCanvas.GetBlock(row, column);
                if(block.IsEqual(blockInGroup)) {
                    result = index;
                    break;
                }
            }
            if(result != -1) {
                break;
            }
            index++;
        }
        return result;
    }

    private void DestroyBlockGroup(ArrayList blockGroup) {
        foreach (int[] coordinates in blockGroup)
        {
            int row = coordinates[0];
            int column = coordinates[1];
            _blockCanvas.DestroyBlock(row, column);
        }
    }

    private bool CheckDeadlock() {
        int thereIsNoGroup = 0;
        bool isDeadlock = _blockGroups.Count == thereIsNoGroup;
        return isDeadlock;
    }

    private void MixUntilNoDeadlocks() {
        int count = 0;
        bool isDeadlock = CheckDeadlock();
        while(isDeadlock) {
            count++;
            _blockCanvas.ChangeLocationOfTwoBlocksRandomly();
            ChangeLevelOfSuitableBlocks();
            Debug.Log("hata");
            if(count > 100) {
                Debug.Log("dsf");
                ResetCanvas();
            }
            isDeadlock = CheckDeadlock();
        }
    }

    private void ResetCanvas() {
        _blockCanvas.ResetCanvas();
        ChangeLevelOfSuitableBlocks();
    }
}
