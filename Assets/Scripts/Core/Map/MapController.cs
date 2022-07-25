using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

namespace Core.Map
{
    public class MapController : MonoBehaviour
    {
        [SerializeField] private MapSettings _mapSettings;

        private List<Vector3> _allPatrolPoints;
        private List<Vector3> _generatedPatrolPoints;
        
        private Vector3 _basePoint;

        private float _lengthMap;
        private float _widthMap;
        
        public Vector3 BasePoint => _basePoint;
        public List<Vector3> PatrolPoints => _generatedPatrolPoints;

        public void StartGame()
        {
            GenerateMap();
        }

        private void GenerateMap()
        {
            var startMapPoint = _mapSettings.StartMapPoint;
            var endMapPoint = _mapSettings.EndMapPoint;
            var minDistancePatrolPoints = _mapSettings.MinDistancePatrolPoints;

            _widthMap = endMapPoint.x - startMapPoint.x;
            _lengthMap = endMapPoint.z - startMapPoint.z;

            _allPatrolPoints = GenerateMaxPatrolPoints(startMapPoint, _widthMap, _lengthMap, minDistancePatrolPoints);
            _generatedPatrolPoints = new List<Vector3>();
            
            _basePoint = GenerateBasePoint(startMapPoint, endMapPoint);
            
            while(_generatedPatrolPoints.Count != _mapSettings.NumberPatrolPoints)
            {
                var randomPoint = _allPatrolPoints[Random.Range(0, _allPatrolPoints.Count)];
                while (_generatedPatrolPoints.Contains(randomPoint))
                {
                    randomPoint = _allPatrolPoints[Random.Range(0, _allPatrolPoints.Count)];
                }
                _generatedPatrolPoints.Add(randomPoint);
            }
        }
        
        private Vector3 GenerateBasePoint(Vector3 startMapPoint, Vector3 endMapPoint)
        {
            var x = Random.Range(startMapPoint.x, endMapPoint.x);
            var z = Random.Range(startMapPoint.z, endMapPoint.z);

            return new Vector3(x, 0f, z);
        }
        
        private List<Vector3> GenerateMaxPatrolPoints(Vector3 startMapPoint, float widthMap, float lengthMap, float minDistancePatrolPoints)
        {
            var generatedPatrolPoints = new List<Vector3>();

            var maxPointsByX = widthMap / minDistancePatrolPoints;
            var maxPointsByZ = lengthMap / minDistancePatrolPoints;
            
            for (var i = 0; i <= maxPointsByX; i++)
            {
                var pointX = startMapPoint.x + minDistancePatrolPoints * i;
                for (var j = 0; j <= maxPointsByZ; j++)
                {
                    var pointZ = startMapPoint.z + minDistancePatrolPoints * j;
                    var patrolPoint = new Vector3(pointX, 0f, pointZ);
                    
                    generatedPatrolPoints.Add(patrolPoint);
                }
            }

            return generatedPatrolPoints;
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0.0f, 1.0f, 0.0f);
            
            if (_generatedPatrolPoints != null && _generatedPatrolPoints.Count != 0)
            {
                DrawMap();
            }
        }

        private void DrawMap()
        {
            Gizmos.DrawWireCube(new Vector3(_widthMap / 2, 0 , _lengthMap / 2), new Vector3(_widthMap, 0f, _lengthMap));

            foreach (var point in _generatedPatrolPoints)
            {
                Gizmos.DrawWireCube(point, new Vector3(0.5f, 0.5f, 0.5f));
            }
            
            Gizmos.DrawSphere(_basePoint, 0.2f);
        }
    }
}

