namespace Yams
{
    public class Escaped : YamState
    {
        public Escaped(YamStateManager manager) : base(manager) { }
        
        public override void Enter(YamStateName prevState)
        {
            manager.Anim.ChangeAnim("Climb");
        }

        public override YamStateName Update()
        {
            return YamStateName.Escaped;
        }

        public override void Exit()
        {
        }
    }
}