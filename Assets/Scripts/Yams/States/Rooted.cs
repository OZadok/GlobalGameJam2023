namespace Yams
{
    public class Rooted : YamState
    {
        
        public Rooted(YamStateManager manager) : base(manager)
        {
            // pass rooted initialization specific args here.
        }
        
        public override void Enter()
        {
            throw new System.NotImplementedException();
        }

        public override YamStateName Update()
        {
            throw new System.NotImplementedException();
        }

        public override void Exit()
        {
            throw new System.NotImplementedException();
        }
    }
}