using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using AsyncOperation = UnityEngine.AsyncOperation;
using Object = System.Object;

[CreateAssetMenu( fileName = "SceneManager", menuName = "Scriptable Objects/Scene Manager" )]
public class MySceneManager : ScriptableObject
{
    private Stack<int> loadedLevels;

    [System.NonSerialized]
    private bool initialized;

    /*
     * This Script has been sourced from the following link:
     * https://discussions.unity.com/t/how-i-can-open-a-previous-scene-with-button-back/221964
     */
    private void Init()
    {
        loadedLevels = new Stack<int>();
        initialized = true;
    }
 
    public Scene GetActiveScene()
    {
        return SceneManager.GetActiveScene();
    }
    
    public void LoadScene( int buildIndex )
    {
        if ( !initialized ) Init();
        loadedLevels.Push( GetActiveScene().buildIndex );
        SceneManager.LoadScene( buildIndex );
    }

    // public void LoadScene(List<string> scenes )
    // {
    //     // if ( !initialized ) Init();
    //     // loadedLevels.Push(GetActiveScene().buildIndex);
    //     // UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    //     if (scenes.Count == 0)
    //         return;
    //
    //     foreach (string scene in scenes)
    //     {
    //         if ( !initialized ) Init();
    //         loadedLevels.Push(GetActiveScene().buildIndex);
    //         if (!SceneManager.GetSceneByName(scene).isLoaded)
    //             SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
    //             
    //     }
    // }
    
    public void LoadScene( string sceneName )
    {
        if ( !initialized ) Init();
        loadedLevels.Push( GetActiveScene().buildIndex );
        SceneManager.LoadSceneAsync( sceneName, LoadSceneMode.Additive );
    }

    
    public void LoadPreviousScene()
    {
        if ( !initialized )
        {
            Debug.LogError( "You haven't used the LoadScene functions of the scriptable object. Use them instead of the LoadScene functions of Unity's SceneManager." );
        }
        if ( loadedLevels.Count > 0 )
        {
            SceneManager.LoadScene( loadedLevels.Pop() );
        }
        else
        {
            Debug.LogError( "No previous scene loaded" );
            // If you want, you can call `Application.Quit()` instead
        }
    }
    
    public void Quit()
    {
        Application.Quit();
        
    }
}
