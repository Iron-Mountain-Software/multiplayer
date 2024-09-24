using System;
using System.Net.Http;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;
using UnityEngine.UI;

namespace Multiplayer
{
    public class RelayManager : MonoBehaviour
    {
        public static event Action OnCurrentRoomChanged;
    
        private static string _currentRoomCode = string.Empty;

        public static string CurrentRoomCode
        {
            get => _currentRoomCode;
            set
            {
                if (_currentRoomCode == value) return;
                _currentRoomCode = value;
                OnCurrentRoomChanged?.Invoke();
            }
        }

        [SerializeField] private Button hostButton;
        [SerializeField] private Button joinButton;
        [SerializeField] private InputField relayCodeInput;
        [SerializeField] private Text errorText;

        public async void Start()
        {
            SetErrorMessage(string.Empty);
            await UnityServices.InitializeAsync();
            if (!AuthenticationService.Instance.IsSignedIn)
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
            }
            hostButton.onClick.AddListener(CreateRelay);
            joinButton.onClick.AddListener(() => JoinRelay(relayCodeInput.text));
        }

        public async void CreateRelay()
        {
            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(3);
            CurrentRoomCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
            var relayServerData = new RelayServerData(allocation, "wss");
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);
            NetworkManager.Singleton.StartHost();
        }

        public async void JoinRelay(string joinCode)
        {
            SetErrorMessage(string.Empty);
            try
            {
                JoinAllocation joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode);
                CurrentRoomCode = joinCode;
                RelayServerData relayServerData = new RelayServerData(joinAllocation, "wss");
                NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);
                NetworkManager.Singleton.StartClient();
            }
            catch
            {
                SetErrorMessage("Failed");
            }
        }

        private void SetErrorMessage(string message)
        {
            if (!errorText) return;
            errorText.text = message;
        }
    }
}
