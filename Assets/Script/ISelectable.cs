using System;
using System.Collections;
using System.Collections.Generic;
using Script.Character;
using UnityEngine;

public interface ISelectable
{
   void SelectCharacter(bool select);
   public void MoveCharacter(Vector3 point);
   Vector3 Pos();
   Character GetStats();
}


public interface IViewable
{
   void ShowWalkTile(ISelectable obj); 
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
   void UpdateTurnText(Turn.Turns act);
   void ShowTurnCount(int turns);
   public void ShowPopUp(ResistanceType result, Vector3 pos, int damage);
   
}



public interface IAttackable
{
   IWeapon Attack();
}
public interface ICharacter : ISelectable, IAttackable, IDamageable,IVisualization<ICharacterView>
{
   int Stamina { get; }
   bool OutOfAP { get; }
  public bool isDamaged { get; }
   public void PrepareCharacterForNewTurn(List<ICharacter> list, Turn.Turns act, int add);
   void PrepareWeapon(IWeapon weapon);
   
   void SetField(IViewable field);
   
}
public interface IResistance
{
   public void ShowResistance();
   int CalculateDamage(IWeapon weapon);
   ResistanceType GetAttackResult();
   
}

public interface IHealth : IVisualization<IHealthView>
{
   public bool isOver { get; }
   public Health.HealthEnum healthStatus { get; }
   int DecreaseHealth(int damage);
   void SetHP(int hp);
   int HealthPoints { get; }
}

public interface IVisualization<in TView>
{
   void Visualize(TView view);
}

public interface IHealthView
{
   void DisplayHealth(float amount);
}

public interface ICharacterView : IHealthView
{
   void DisplayName(string name);
}

public interface IDamageable
{
   public bool IsDead {get;}
   public void TakeDamage(IWeapon weapon);

}









