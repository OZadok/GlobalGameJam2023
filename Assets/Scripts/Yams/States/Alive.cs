namespace Yams
{
    public class Alive : YamState
    {
        public Alive(YamStateManager manager) : base(manager)
        {
            // pass alive initialization specific args here.
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