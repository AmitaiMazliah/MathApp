using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace MathApp
{
    public class Scene : CoreBehaviour
    {
        public bool ContextReady { get; private set; }
        public bool IsActive { get; private set; }
        public SceneContext Context => context;

        [SerializeField] private bool selfInitialize;
        [SerializeField] private SceneContext context;

        private bool isInitialized;
        private List<SceneService> services = new List<SceneService>();

        public void PrepareContext()
        {
            OnPrepareContext(context);
        }

        public void Initialize()
        {
            if (isInitialized)
                return;

            if (ContextReady == false)
            {
                OnPrepareContext(context);
            }

            OnInitialize();

            isInitialized = true;
        }

        public async Task Activate()
        {
            if (isInitialized == false)
                await Task.Delay(TimeSpan.FromMilliseconds(25));

            OnActivate();

            IsActive = true;
        }

        public void Deactivate()
        {
            if (IsActive == false)
                return;

            OnDeactivate();

            IsActive = false;
        }

        public void Deinitialize()
        {
            if (isInitialized == false)
                return;

            Deactivate();

            OnDeinitialize();

            ContextReady = false;
            isInitialized = false;
        }

        public T GetService<T>() where T : SceneService
        {
            for (int i = 0, count = services.Count; i < count; i++)
            {
                if (services[i] is T service)
                    return service;
            }

            return null;
        }

        public void Quit()
        {
            Deinitialize();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
			Application.Quit();
#endif
        }

        protected void Awake()
        {
            if (selfInitialize)
            {
                Initialize();
            }
        }

        protected IEnumerator Start()
        {
            if (isInitialized == false)
                yield break;

            if (selfInitialize && IsActive == false)
            {
                // UI cannot be initialized in Awake, Canvas elements need to Awake first
                AddService(context.UI);

                yield return Activate();
            }
        }

        protected virtual void Update()
        {
            if (IsActive == false)
                return;

            OnTick();
        }

        protected virtual void LateUpdate()
        {
            if (IsActive == false)
                return;

            OnLateTick();
        }

        protected void OnDestroy()
        {
            Deinitialize();
        }

        protected void OnApplicationQuit()
        {
            Deinitialize();
        }

        protected virtual void OnPrepareContext(SceneContext context)
        {
            // context.PlayerData = Global.PlayerService.PlayerData;
            // context.Settings = Global.Settings;
            // context.RuntimeSettings = Global.RuntimeSettings;

            context.hasInput = true;
            context.isVisible = true;

            ContextReady = true;
        }

        protected virtual void OnInitialize()
        {
            CollectServices();
        }

        protected virtual void OnActivate()
        {
            for (var i = 0; i < services.Count; i++)
            {
                services[i].Activate();
            }
        }

        protected virtual void OnTick()
        {
            for (int i = 0, count = services.Count; i < count; i++)
            {
                services[i].Tick();
            }
        }

        protected virtual void OnLateTick()
        {
            for (int i = 0, count = services.Count; i < count; i++)
            {
                services[i].LateTick();
            }
        }

        protected virtual void OnDeactivate()
        {
            for (var i = 0; i < services.Count; i++)
            {
                services[i].Deactivate();
            }
        }

        protected virtual void OnDeinitialize()
        {
            for (var i = 0; i < services.Count; i++)
            {
                services[i].Deinitialize();
            }

            services.Clear();
        }

        protected virtual void CollectServices()
        {
            var services = GetComponentsInChildren<SceneService>(true);

            foreach (var service in services)
            {
                AddService(service);
            }
        }

        protected void AddService(SceneService service)
        {
            if (service == null)
            {
                Debug.LogError("Missing service");
                return;
            }

            if (services.Contains(service))
            {
                Debug.LogError($"Service {service.gameObject.name} already added.");
                return;
            }

            service.Initialize(this, Context);

            services.Add(service);
        }
    }
}
