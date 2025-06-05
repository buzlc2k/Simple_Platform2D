using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer2D
{
    public class GameManager : Singleton<GameManager>, ISMContext<GameStateID>
    {
        private BaseSMState<GameStateID> currentState;
        Dictionary<GameStateID, BaseSMState<GameStateID>> states;

        public GameStateID PreviousStateID { get; set; }
        public GameStateID CurrentStateID { get; set; }

        BaseSMState<GameStateID> ISMContext<GameStateID>.CurrentState { get => currentState; set => currentState = value; }
        Dictionary<GameStateID, BaseSMState<GameStateID>> ISMContext<GameStateID>.States { get => states; set => states = value; }

        protected override void Awake()
        {
            base.Awake();

            InitializeStates();
        }

        private void Start()
        {
            ((ISMContext<GameStateID>)this).ChangeState(GameStateID.MainMenu);
        }

        public void InitializeStates()
        {
            states = new();

            var mainMenuState = new MainMenuState(this, this);
            var levelSelectedState = new LevelSelectedState(this, this);
            var gameRunningState = new GameRunningState(this, this);
            var gamePausedState = new GamePausedState(this, this);
            var loadingState = new LoadingState(this, this);
            var overState = new GameOverState(this, this);
            var winState = new GameWinningState(this, this);

            states.Add(mainMenuState.ID, mainMenuState);
            states.Add(levelSelectedState.ID, levelSelectedState);
            states.Add(gameRunningState.ID, gameRunningState);
            states.Add(gamePausedState.ID, gamePausedState);
            states.Add(loadingState.ID, loadingState);
            states.Add(overState.ID, overState);
            states.Add(winState.ID, winState);
        }

        public void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit(); 
#endif
        }
    }
}