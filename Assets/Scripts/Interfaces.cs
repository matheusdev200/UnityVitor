using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interfaces { }

//palavra-chave de acesso -> tipo interface -> nome do contrato
public interface IDamage //entrega um contrato e devolve uma assinatura
{
    //void TakeDamage();//obriga o assinante a ter um m�todo com esse nome
    void TakeDamage(float damage); //dano do canhão no player 
    void TakeDamage(int damage); //dano do player no canhão
    void HealDamage();
    //void Die();
}
public interface ICollectable
{
    void PickItem();
}
public interface IPausable
{
    bool IsPaused();
}
public interface IBoss 
{
    void TakeDamage(int damage);//dano do player no boss
    //void Recharge(float energy); //energia do boss
}