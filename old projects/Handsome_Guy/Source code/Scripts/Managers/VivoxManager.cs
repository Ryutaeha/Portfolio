using System.Collections;
using System.Collections.Generic;
using Unity.Services.Core;
using Unity.Services.Vivox;
using Unity.Services.Authentication;
using UnityEngine;
using System.Threading.Tasks;

public class VivoxManager
{
    private string _server = "https://unity.vivox.com/appconfig/14568-hands-62421-udash";
    private string _domain = "mtu1xp.vivox.com";
    private string _issuer = "14568-hands-62421-udash";
    private string _tokenKey = "9Q60Dqg64EMPKlHUv3GZW2XYO6anHDAc";
    private string _channelName = "lobbyChannel";
    private IChatable _currentSceneUI;
    private string _userName => Main.GameManager.NickName;
    private bool _initialized = false;
    LoginOptions options;
    public async Task InitializeAsync()
    {
        if (!_initialized)
        {
            InitializationOptions options = new InitializationOptions();
            options.SetVivoxCredentials(_server, _domain, _issuer, _tokenKey);
            await UnityServices.InitializeAsync(options);
            await AuthenticationService.Instance.SignInAnonymouslyAsync();

            await VivoxService.Instance.InitializeAsync();
            _initialized = true;

            //참가자 관련
            VivoxService.Instance.ParticipantAddedToChannel += OnParticipantAdded;
            VivoxService.Instance.ParticipantRemovedFromChannel += OnParticipantRemoved;

            //메시지 관련
            VivoxService.Instance.ChannelMessageReceived += OnChannelMessageReceived;
        }

        _currentSceneUI = (IChatable)Main.SceneManagerEx.CurrentScene.UI;

    }
    private void OnDestroy()
    {
        VivoxService.Instance.ParticipantAddedToChannel -= OnParticipantAdded;
        VivoxService.Instance.ParticipantRemovedFromChannel -= OnParticipantRemoved;
        VivoxService.Instance.ChannelMessageReceived -= OnChannelMessageReceived;

    }
    public async Task LoginToVivoxAsync()
    {
        options = new LoginOptions();
        options.DisplayName = _userName;
        await VivoxService.Instance.LoginAsync(options);
        Debug.Log("로그인 완료");
    }
    public async Task LogoutOfVivoxAsync()
    {
        await VivoxService.Instance.LogoutAsync();
        Debug.Log("로그아웃");
    }
    public async Task JoinGroupChannelAsync(string channelName = "lobbyChannel")
    {

        await VivoxService.Instance.JoinGroupChannelAsync(channelName, ChatCapability.TextAndAudio);
    }
    public async Task LeaveEchoChannelAsync(string channelName = "lobbyChannel")
    {
        await VivoxService.Instance.LeaveChannelAsync(_channelName);
        Debug.Log("채널 퇴장");
    }

    void OnParticipantAdded(VivoxParticipant participant)
    {
        _currentSceneUI.InputUser(participant);
        _currentSceneUI.InputChat($"{participant.DisplayName} 님이 접속했습니다.");
    }
    void OnParticipantRemoved(VivoxParticipant participant)
    {
        _currentSceneUI.DeleteUser(participant);
        _currentSceneUI.InputChat($"{participant.DisplayName} 님이 떠났습니다.");
    }
    void OnChannelMessageReceived(VivoxMessage message)
    {
        _currentSceneUI.InputChat(message.MessageText);
        _currentSceneUI.SetScrollToBottom();
    }
    public void SendChatMessage(string msg)
    {
        msg = $"{_userName} : " + msg;
        VivoxService.Instance.SendChannelTextMessageAsync(_channelName, msg);
    }
}

public interface IChatable
{
    public void InputChat(string str);
    public void InputUser(VivoxParticipant participant);
    public void DeleteUser(VivoxParticipant participant);
    public void SetScrollToBottom();
}