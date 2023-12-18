using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using AsyncOperation = UnityEngine.AsyncOperation;
using Object = UnityEngine.Object;

namespace GD.Examples
{
    /// <summary>
    /// Manages the game flow, scene loading, and persistent objects.
    /// </summary>
    public class SimpleGameManager : MonoBehaviour
    {
        /// <summary>
        /// Indicates whether the game manager has been loaded.
        /// </summary>
        [SerializeField]
        [ReadOnly]
        private bool isLoaded = false;

        [Header("Persistent UI")]
        [SerializeField]
        private GameObject mainMenuPrefab;

        [SerializeField]
        private GameObject uiPrefab;

        [Header("Persistent In-Game")]
        [SerializeField]
        [Tooltip("Spawns once to load objects that persist between scenes")]
        private List<GameObject> corePrefabs;

        [Header("Level 1")]
        [SerializeField]
        private List<Object> scenesLevelOne;

        private GameObject mainMenu;

        /// <summary>
        /// Start is called before the first frame update.
        /// </summary>
        private void Start()
        {
            // Don't start twice!
            if (isLoaded)
                return;

            // Don't destroy if we are running the game (i.e., not in Edit mode)
            DontDestroyOnLoad(this);

            // Check if corePrefabs is set
            if (corePrefabs == null)
                throw new ArgumentNullException("corePrefabs has not been set!");

            // Load menu
            mainMenu = LoadPersistentPrefab(mainMenuPrefab);
            
            // Add menu overlay camera to stack
            var menuCamera = mainMenu.GetComponentInChildren<Camera>();
            if (menuCamera != null)
                Camera.main.GetUniversalAdditionalCameraData().cameraStack.Add(menuCamera);

            // Load all the core system objects (camera, managers, etc.)
            foreach (var persistentObject in corePrefabs)
                LoadPersistentPrefab(persistentObject);

            // Set timescale to 0 to pause all dynamic elements
            //  PauseGame();

            // Start game
            StartGame();

            //is this true?
            isLoaded = true;
        }

        /// <summary>
        /// Loads a persistent prefab, instantiates it, and marks it as "Don't Destroy On Load."
        /// </summary>
        /// <param name="prefab">The prefab to load.</param>
        /// <returns>The instantiated object.</returns>
        public GameObject LoadPersistentPrefab(GameObject prefab)
        {
            if (prefab == null)
                return null;

            // Instantiate and don't destroy if we load a new scene (i.e., persist across scenes)
            var instance = Instantiate(prefab);

            // Set name
            instance.name = prefab.name;

            // Don't destroy if we are running the game (i.e., not in Edit mode) and load a new scene (i.e., persist across scenes)
            DontDestroyOnLoad(instance);

            return instance;
        }

        /// <summary>
        /// Starts the game by loading the initial level.
        /// </summary>
        public void StartGame()
        {
            LoadLevel(scenesLevelOne);
        }

        /// <summary>
        /// Loads a list of scenes asynchronously in additive mode.
        /// </summary>
        /// <param name="scenes">The list of scenes to load.</param>
        public void LoadLevel(List<Object> scenes)
        {
            if (scenes.Count == 0)
                return;

            foreach (Object scene in scenes)
            {
                if (!SceneManager.GetSceneByName(scene.name).isLoaded)
                     SceneManager.LoadSceneAsync(scene.name, LoadSceneMode.Additive);
            }

            //more stuff here...
        }

        /// <summary>
        /// Unloads a list of scenes asynchronously.
        /// </summary>
        /// <param name="scenes">The list of scenes to unload.</param>
        public void UnLoadLevel(List<Object> scenes)
        {
            if (scenes.Count == 0)
                return;

            foreach (Object scene in scenes)
            {
                 if (SceneManager.GetSceneByName(scene.name).isLoaded)
                    SceneManager.UnloadSceneAsync(scene.name);
            }
        }

        /// <summary>
        /// Pauses the game by setting the time scale to 0.
        /// </summary>
        public void PauseGame()
        {
            Time.timeScale = 0;
        }

        /// <summary>
        /// Resumes the game by restoring the original time scale.
        /// </summary>
        public void ContinueGame()
        {
            Time.timeScale = 1; // or whatever it was originally
        }

        /// <summary>
        /// Exits the game.
        /// </summary>
        public void ExitGame()
        {
            // Do other housekeeping here...
            Application.Quit();
        }

        /// <summary>
        /// Update is called once per frame.
        /// </summary>
        private void Update()
        {
            //Handle input, e.g., escape key to show the menu
            
        }

        public void ShowDebug()
        {
            Debug.Log($"ShowDebug...");
        }
    }
}