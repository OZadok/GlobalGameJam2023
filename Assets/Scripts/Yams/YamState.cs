namespace Yams
{
    public abstract class YamState
    {
        
        public enum YamStateName
        {
            None = 0, 
            Sprouting = 1, 
            Alive = 2, 
            Rooted = 3,
            Escaped = 4,
            Destroyed = 10,
        }

        protected YamStateManager manager;

        protected YamState(YamStateManager manager)
        {
            this.manager = manager;
        }

        public abstract void Enter();
        
        public abstract YamStateName Update();
        
        public abstract void Exit();
    }
}