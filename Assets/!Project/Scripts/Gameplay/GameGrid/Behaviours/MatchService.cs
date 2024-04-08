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
    }
}