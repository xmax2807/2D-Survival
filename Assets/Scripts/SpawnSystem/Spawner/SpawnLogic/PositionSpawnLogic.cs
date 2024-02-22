using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Project.SpawnSystem
{
    public class PositionSpawnLogic : ISpawnLogic, IPositionSpawnModifier
    {
        private Camera currentCam;

        public PositionSpawnLogic(Camera cam)
        {
            currentCam = cam;
        }

        public Task<bool> PerformLogic(ISpawnContext context)
        {
            context.Accept(this);
            return Task.FromResult(true);
        }

        public void ReplaceSpawnPosition(ref Vector3 position, SpawnPosition spawnPositionType, ISpawnContext context)
        {
            switch (spawnPositionType)
            {
                case SpawnPosition.AroundPlayer:
                    position = CalculateSpawnPositionAroundPlayer(context);
                    break;
                case SpawnPosition.DistanceFromPlayer: break;
                case SpawnPosition.GivenPosition: break;
            }
        }

        private Vector3 CalculateSpawnPositionAroundPlayer(ISpawnContext context)
        {
            //Get random direction x,y from -1 to 1 for each and plus with camera viewport
            float offset = 3f; // For now just use hard code
            float height = this.currentCam.orthographicSize + offset;
            float width = height * this.currentCam.aspect;
            Vector3 camPosition = this.currentCam.transform.position;

            // get random point on ellipse of this rectangle using x^2 / a^2 + y^2 / b^2 = 1
            // where a: width and b: height

            //pick random x first from -width to width
            float x = UnityEngine.Random.Range(0, width);
            //to avoid sqrt, use formula: y = (1-x/a)(1+x/a) * b
            float y = Mathf.Sqrt((1f - x / width) * (1f + x / width)) * height;
            x = UnityEngine.Random.Range(0, 2) == 0 ? -x : x;
            y = UnityEngine.Random.Range(0, 2) == 0 ? -y : y;

            return new Vector3(x + camPosition.x, y + camPosition.y);
        }
    }
}