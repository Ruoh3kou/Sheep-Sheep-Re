using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace SheepSheep
{
    public class LevelLogic
    {
        // 可以生成的最大卡片数量
        private int MaxCardCount => Mathf.Max(3, Define.MAX_Z * Define.MAX_Y * Define.MAX_X);

        private int[,,] _levelMapping;

        public void GenerateLevel(int totalCount = 0)
        {
            _levelMapping = new int[Define.MAX_X, Define.MAX_Y, Define.MAX_Z];

            Random random = new Random();
            if (totalCount == 0 || totalCount % 3 != 0)
            {
                do
                    totalCount = random.Next(3, MaxCardCount);
                while (totalCount % 3 != 0);
            }
            Debug.Log("生成数量：" + totalCount);

            // 创建一个列表,用于存储已分配的方块种类
            List<int> assignedTypes = new List<int>();
            for (int i = 0; i < totalCount; i++)
            {
                // 获取一个未分配的卡片种类
                int blockType = GetUnassignedCardType(assignedTypes, random);
                // 如果所有种类都已分配,则从头开始
                if (blockType == -1)
                {
                    assignedTypes.Clear();
                    blockType = GetUnassignedCardType(assignedTypes, random);
                }
                // 将该种类添加到已分配列表中
                assignedTypes.Add(blockType);
                // 获取一个随机位置,确保该位置未被占用
                int x, y, z;
                do
                {
                    x = random.Next(Define.MAX_X);
                    y = random.Next(Define.MAX_Y);
                    z = random.Next(Define.MAX_Z);
                } while (_levelMapping[x, y, z] != 0);

                // 将该位置设置为当前方块种类
                _levelMapping[x, y, z] = blockType;
            }
        }

        public int GetCardType(int x, int y, int z)
        {
            return _levelMapping[x, y, z];
        }

        private int GetUnassignedCardType(List<int> assignedTypes, Random random)
        {
            List<int> unassignedTypes = new List<int>();
            for (int i = 0; i < Define.NUM_TYPE_COUNT; i++)
            {
                if (!assignedTypes.Contains(i) || assignedTypes.Count(t => t == i) < 3)
                {
                    unassignedTypes.Add(i);
                }
            }
            if (unassignedTypes.Count == 0)
            {
                return -1;
            }
            // 从未分配列表中随机选择一个种类
            int index = random.Next(unassignedTypes.Count);
            return unassignedTypes[index];
        }
    }
}