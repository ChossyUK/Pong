using UnityEngine;
using UnityEngine.EventSystems;

namespace CedarWoodSoftware.Scene
{
    public class MenuController : MonoBehaviour
    {
        #region Canvas & Selected Items Variables
        // Add or remove canvas & 1st selected item variables below
        [Header("Menu Canvas's")]
        public Canvas mainMenu;
        public Canvas pauseMenu;
        public Canvas optionsMenu;
        public Canvas creditsMenu;
        public Canvas infoMenu;
        public Canvas gameOver;

        [Header("Menu's 1st Selected Items")]
        public GameObject mainMenuButton;
        public GameObject pauseMenuButton;
        public GameObject optionsMainButton;
        public GameObject creditsMenuButton;
        public GameObject infoMenuButton;
        public GameObject gameOverMenu;

        [Header("Auto Select Button")]
        public bool use1stSelectedItem = false;
        #endregion

        #region Menu Methods
        // Add or remove open/close menu methods below
        public void OpenMainMenu()
        {
            mainMenu.gameObject.SetActive(true);
            if (use1stSelectedItem)
            {
                SetFirstItem(mainMenuButton);
            }
        }

        public void CloseMainMenu()
        {
            mainMenu.gameObject.SetActive(false);
        }

        public void OpenPauseMenu()
        {
            pauseMenu.gameObject.SetActive(true);
            if (use1stSelectedItem)
            {
                SetFirstItem(pauseMenuButton);
            }
        }

        public void ClosePauseMenu()
        {
            pauseMenu.gameObject.SetActive(false);
        }

        public void OpenOptionsMenu()
        {
            optionsMenu.gameObject.SetActive(true);
            if (use1stSelectedItem)
            {
                SetFirstItem(optionsMainButton);
            }
        }

        public void CloseOptionsMenu()
        {
            optionsMenu.gameObject.SetActive(false);
        }

        public void OpenCreditsMenu()
        {
            creditsMenu.gameObject.SetActive(true);
            if (use1stSelectedItem)
            {
                SetFirstItem(creditsMenuButton);
            }
        }

        public void CloseCreditsMenu()
        {
            creditsMenu.gameObject.SetActive(false);
        }

        public void OpenGameOverMenu()
        {
            gameOver.gameObject.SetActive(true);
            if (use1stSelectedItem)
            {
                SetFirstItem(gameOverMenu);
            }
        }

        public void CloseGameOverMenu()
        {
            gameOver.gameObject.SetActive(false);
        }

        public void OpenInfoMenu()
        {
            infoMenu.gameObject.SetActive(true);
            if (use1stSelectedItem)
            {
                SetFirstItem(infoMenuButton);
            }
        }

        public void CloseInfoMenu()
        {
            infoMenu.gameObject.SetActive(false);
        }

        public void SetFirstItem(GameObject selectedItem)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(selectedItem);
        }
        #endregion
    }
}
