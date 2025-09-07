using UnityEngine;

public class InvestigateGuardaState : BaseState<GuardaFSM.AIState>
{
   private GuardaFSM _guarda;

   //contrutor
   public InvestigateGuardaState(GuardaFSM.AIState key) : base(key)
   {
      StateKey = key;
   }

   public override void EnterState()
   {
      _guarda = (GuardaFSM)MyFsm;
      Debug.Log("Guarda está investigando...");
   }

   public override void UpdateState()
   {
      //verifica se varivel inicio corretamente no EnterState()
      if (_guarda == null) return;
      
      //se tem algo suspeito vai ate la
      if (_guarda.targetEnemy != null)
      {
         _guarda.MoveTowards(_guarda.targetEnemy.position);
      }
      base.UpdateState();
   }

   public override GuardaFSM.AIState GetNextState()
   {
      if(_guarda==null) return StateKey;
      if (_guarda.targetEnemy==null) 
      {
         //nada encontrado-> patrulha
         return GuardaFSM.AIState.Patrol;
      }
      //se estiver perto do suspeito, confirma e perseguir
      float distancie = Vector3.Distance(
         _guarda.transform.position,
         _guarda.transform.position);

      if (distancie<= _guarda.detectionRange/2f)
      {
         return GuardaFSM.AIState.Chase;
      }
      
      //cintina invertigando
      return StateKey;
   }
   
}
