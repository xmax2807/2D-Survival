namespace Project.GameEventSystem.EventGraph
{
    public class Tree<TNode> where TNode : RuntimeNode
    {
        public readonly TNode Root;
        public TNode CurrentNode {get; private set;}
        public Tree(TNode root){
            Root = root;
            CurrentNode = Root;
        }
        
        public void AddNode(TNode node, TNode parent = null){
            if(parent == null){
                Root.AddChild(node);
            }else{
                parent.AddChild(node);
            }
        }

        public void NextNode(){
            if(CurrentNode.next == null){
                CurrentNode = null;
                return;
            }
            CurrentNode = (TNode)CurrentNode.next;
        }
    }
}