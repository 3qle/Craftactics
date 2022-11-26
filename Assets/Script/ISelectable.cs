using System;
using System.Collections;
using System.Collections.Generic;
using Script.Character;
using Script.Managers;
using UnityEngine;




public interface IViewable
{
   void ShowWalkTile(IFightable obj); 
   void HideTiles();
   public void ShowAttackTiles(IWeapon stats);
  void SetTileType(Character obj,bool free);
  List<List<Cell>> GetField();
  public List<Cell> GetTargetsForEnemy(Character obj);
  public void CreateHighLight(Vector3 pos,bool create);
  public void SpawnButton();

}

public interface IUIble
{
   void UpdateTurnText(Script.Managers.TurnState act);
   void ShowTurnCount(int turns);
   public void ShowPopUp(ResistanceType result, Vector3 pos, int damage);
   
}




public interface IFightable 
{
   public bool IsDead {get;}
   public void TakeDamage(IWeapon weapon);
   IWeapon Attack();
   int Stamina { get; }
   bool OutOfAP { get; }
  public bool isDamaged { get; }
   public void PrepareCharacterForNewTurn(List<IFightable> list, Script.Managers.TurnState act, int add);
   void PrepareWeapon(IWeapon weapon);

   public void Inject(IViewable field, UI ui, Pool pool);
   void SelectCharacter(bool select);
   public void MoveCharacter(Vector3 point);
   Vector3 Pos();
   Character GetStats();
   

}
public interface IResistance 
{
   public void ShowResistance();
   int CalculateDamage(IWeapon weapon);
   ResistanceType GetAttackResult();
   
}

public interface IHealth 
{
   public bool isOver { get; }
   public Health.HealthEnum healthStatus { get; }
   int DecreaseHealth(int damage);
   void SetMaxHP(int hp);
   int HealthPoints { get; }
}












