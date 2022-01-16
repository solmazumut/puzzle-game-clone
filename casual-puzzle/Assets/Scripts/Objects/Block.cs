using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Block : MonoBehaviour
{
    // Start is called before the first frame update
    private BehaviorInterface colorBehavior;
    private MeshRenderer enderer;
    private SortingGroup sortingGroup;
    private GameManager gameManager;
    private Vector3 target;
    private bool isOnTarget;
    private float speed;

    public void setSpeed(float s) {
        speed = s;
    }

    public BehaviorInterface getColorBehavior() {
        return colorBehavior;
    }
    public void initializeTarget() {
        isOnTarget = true;
        target = transform.position;
    }
    public void setDefault() {
        if(enderer == null) {
            enderer = GetComponent<MeshRenderer>();
        }
        enderer.sharedMaterial = colorBehavior.defaultSprite();
    }
    public void setFirstLevel() {
        if(enderer == null) {
            enderer = GetComponent<MeshRenderer>();
        }
        enderer.sharedMaterial = colorBehavior.firstLevelSprite();
    }
    public void setSecondLevel() {
        if(enderer == null) {
            enderer = GetComponent<MeshRenderer>();
        }
        enderer.sharedMaterial = colorBehavior.secondLevelSprite();
    }
    public void setThirdLevel() {
        if(enderer == null) {
            enderer = GetComponent<MeshRenderer>();
        }
        enderer.sharedMaterial = colorBehavior.thirdLevelSprite();
    }
    public bool isEqual(Block block) {
        bool result = false;
        if(block.getSortingOrder() == sortingGroup.sortingOrder) {
            result = true;
        }
        return result;
    }
    public int getSortingOrder() {
        return sortingGroup.sortingOrder;
    }

    void Start()
    {
        enderer = GetComponent<MeshRenderer>();
        sortingGroup = GetComponent<SortingGroup>();
        
    }

    public void setGameManager(GameManager gm) {
        gameManager = gm;
    }

    public void setColorBehavior(BehaviorInterface cb) {
        if(cb != null) {
            colorBehavior = cb;
            if(enderer == null) {
                  enderer = GetComponent<MeshRenderer>();
            }
            enderer.sharedMaterial = colorBehavior.defaultSprite();
        } else {
            Debug.Log("color behavior is null");
        }
        
    }
     public void setSortingOrder(int order) {
        
        if(sortingGroup == null) {
            sortingGroup = GetComponent<SortingGroup>();
        }
        sortingGroup.sortingOrder = order;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isOnTarget) {
            transform.position = Vector3.MoveTowards(transform.position, target, speed* Time.deltaTime);
            if(target.x == transform.position.x && target.y == transform.position.y) {
                isOnTarget = true;
            }
        }
    }

     void OnMouseDown(){
        gameManager.findAndDestroy(this);
    }
    public void Move(float targetRow, float targetColumn) {
        target = new Vector3(targetRow, targetColumn, transform.position.z);
        isOnTarget = false;
    }
}
