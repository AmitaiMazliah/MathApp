namespace MathApp
{
    public class SceneService : CoreBehaviour
    {
        public SceneContext Context => context;
        public Scene Scene => scene;
        public bool IsActive => isActive;
        public bool IsInitialized => isInitialized;

        Scene scene;
        SceneContext context;
        bool isInitialized;
        bool isActive;

        // INTERNAL METHODS

        internal void Initialize(Scene scene, SceneContext context)
        {
            if (isInitialized)
                return;

            this.scene = scene;
            this.context = context;

            OnInitialize();

            isInitialized = true;
        }

        internal void Deinitialize()
        {
            if (isInitialized == false)
                return;

            Deactivate();

            OnDeinitialize();

            scene = null;
            context = null;

            isInitialized = false;
        }

        internal void Activate()
        {
            if (isInitialized == false)
                return;

            if (isActive)
                return;

            OnActivate();

            isActive = true;
        }

        internal void Tick()
        {
            if (isActive == false)
                return;

            OnTick();
        }

        internal void LateTick()
        {
            if (isActive == false)
                return;

            OnLateTick();
        }

        internal void Deactivate()
        {
            if (isActive == false)
                return;

            OnDeactivate();

            isActive = false;
        }

        // GameService INTERFACE

        protected virtual void OnInitialize()
        {
        }

        protected virtual void OnDeinitialize()
        {
        }

        protected virtual void OnActivate()
        {
        }

        protected virtual void OnDeactivate()
        {
        }

        protected virtual void OnTick()
        {
        }

        protected virtual void OnLateTick()
        {
        }
    }
}
