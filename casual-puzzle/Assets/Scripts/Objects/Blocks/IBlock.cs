using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Blocks 
{
    public interface IBlock 
    {
        void Initialize(Behaviors.IColor color, int order, float speed, GameManager gameManager,
         float initialXCoordinate, float initialYCoordinate, float rowLength, float columnLength);

        void SetSortingOrder(int order);

        int GetSortingOrder();

        void SetLevel(int level);

        void ChangeLevelToDefault();

        IColor GetColor();

        bool IsEqual(Blocks.Block block);

        void Move(float targetRow, float targetColumn);
    }
}

