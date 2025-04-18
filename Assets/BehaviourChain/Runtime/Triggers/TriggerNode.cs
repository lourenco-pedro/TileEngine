using System.Collections.Generic;
using ppl.PBehaviourChain.Core.Behaviours;

namespace ppl.PBehaviourChain.Core.Triggers
{
    public abstract class TriggerNode : Node
    {
        public BehaviourNode Child; 

        public override State Update(Dictionary<string, object> args = null)
        {
            if (null == Child)
                return State.Failure;
            
            Node nodeToUpdate = Child.GetNextChild();
            //Se node to update for nulo, quer dizer que chegou na ponta do grafo. Não tendo mais child para atualizar
            //Pode entender que é um caminho concluido
            if (null == nodeToUpdate)
            {
                OnStop();
                return State.Success;
            }
                
            bool hasFailed = nodeToUpdate.Update(args) == State.Failure;
            
            if(hasFailed)
                OnStop();
            
            return !hasFailed ? State.Running : State.Failure;
        }
    }
}