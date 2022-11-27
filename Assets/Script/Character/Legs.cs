using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

  [Serializable]
    public class Legs
    {
        private int _stepDirX, _stepDirY;
        public bool _isWalking;
        private Character _character;
        private Field _field;
        public IEnumerator Walk(Vector2 destination, Character character )
        {
            _character = character;
            _field = _character.field;
            _field.SetTileType(character, true);
            _field.HideTiles();
           
            while (character.Position != destination)
            {
                MakeSteps(destination);
                yield return new WaitForSeconds(0.15f);
            }
            FinishSteps();
        }
        
        void MakeSteps(Vector2 destination)
        {
            _isWalking = true;
            
            Vector2 pos = _character.Position;
            _stepDirX = destination.x == pos.x ? 0 : destination.x > pos.x ? 1 : -1;
            _stepDirY = destination.y == pos.y ? 0 : destination.y > pos.y ? 1 : -1; 
            _character.SetPosition(new Vector2(_stepDirX, _stepDirY));
        }

        public void FinishSteps()
        {
            _field.SetTileType(_character, false);
            _field.ShowWalkTile(_character);
            _field.CreateHighLight(_character.Position, true);
            _isWalking = false;
        }
    }
