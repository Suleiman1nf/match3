using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Project.Scripts.Gameplay.GameGrid.Behaviours
{
    public class MatchService
    {
        private GridService _gridService;

        private GridModel GridModel => _gridService.GridModel;

        public MatchService(GridService gridService)
        {
            _gridService = gridService;
        }

        public List<Vector2Int> FindMatches()
        {
            List<Vector2Int> matchedCells = new List<Vector2Int>();
        
            for (int y = 0; y < GridModel.SizeY; y++)
            {
                for (int x = 0; x < GridModel.SizeX - 2; x++)
                {
                    int itemType = GridModel.Get(x, y);
                    if (!GridModel.IsEmptyAt(x,y) && itemType == GridModel.Get(x + 1, y) && itemType == GridModel.Get(x + 2, y))
                    {
                        matchedCells.Add(new Vector2Int(x, y));
                        matchedCells.Add(new Vector2Int(x + 1, y));
                        matchedCells.Add(new Vector2Int(x + 2, y));
                    }
                }
            }
        
            for (int x = 0; x < GridModel.SizeX; x++)
            {
                for (int y = 0; y < GridModel.SizeY - 2; y++)
                {
                    int itemType = GridModel.Get(x, y);
                    if (!GridModel.IsEmptyAt(x,y) && itemType == GridModel.Get(x, y + 1) && itemType == GridModel.Get(x, y + 2))
                    {
                        matchedCells.Add(new Vector2Int(x, y));
                        matchedCells.Add(new Vector2Int(x, y + 1));
                        matchedCells.Add(new Vector2Int(x, y + 2));
                    }
                }
            }
        
            return matchedCells.Distinct().ToList();
        }
        // public List<Vector2Int> FindMatches()
        // {
        //     List<Vector2Int> matchedCells = new List<Vector2Int>();
        //
        //     for (int y = 0; y < GridModel.SizeY; y++)
        //     {
        //         for (int x = 0; x < GridModel.SizeX - 2; x++)
        //         {
        //             int itemType = GridModel.Get(x, y);
        //             if (!GridModel.IsEmptyAt(x,y) && itemType == GridModel.Get(x + 1, y) && itemType == GridModel.Get(x + 2, y))
        //             {
        //                 matchedCells.Add(new Vector2Int(x, y));
        //                 matchedCells.Add(new Vector2Int(x + 1, y));
        //                 matchedCells.Add(new Vector2Int(x + 2, y));
        //             }
        //         }
        //     }
        //
        //     for (int x = 0; x < GridModel.SizeX; x++)
        //     {
        //         for (int y = 0; y < GridModel.SizeY - 2; y++)
        //         {
        //             int itemType = GridModel.Get(x, y);
        //             if (!GridModel.IsEmptyAt(x,y) && itemType == GridModel.Get(x, y + 1) && itemType == GridModel.Get(x, y + 2))
        //             {
        //                 matchedCells.Add(new Vector2Int(x, y));
        //                 matchedCells.Add(new Vector2Int(x, y + 1));
        //                 matchedCells.Add(new Vector2Int(x, y + 2));
        //             }
        //         }
        //     }
        //
        //     matchedCells = matchedCells.Distinct().ToList(); 
        //     List<Vector2Int> nearCells = new List<Vector2Int>();
        //
        //     foreach (Vector2Int matchedCell in matchedCells)
        //     {
        //         if (CheckNear(matchedCell, new Vector2Int(1, 0)))
        //         {
        //             nearCells.Add(matchedCell + new Vector2Int(1,0));
        //         }
        //         if (CheckNear(matchedCell, new Vector2Int(-1, 0)))
        //         {
        //             nearCells.Add(matchedCell + new Vector2Int(-1,0));
        //         }
        //         if (CheckNear(matchedCell, new Vector2Int(0, 1)))
        //         {
        //             nearCells.Add(matchedCell + new Vector2Int(0,1));
        //         }
        //         if (CheckNear(matchedCell, new Vector2Int(0, -1)))
        //         {
        //             nearCells.Add(matchedCell + new Vector2Int(0,-1));
        //         }
        //     }
        //     
        //     nearCells = nearCells.Distinct().ToList();
        //     matchedCells.AddRange(nearCells);
        //     return matchedCells;
        // }
        //
        // private bool CheckNear(Vector2Int position, Vector2Int direction)
        // {
        //     if (_gridService.CanMoveTo(position, direction) && GridModel.Get(position) == GridModel.Get(position + direction))
        //     {
        //         return true;
        //     }
        //
        //     return false;
        // }
    }
}