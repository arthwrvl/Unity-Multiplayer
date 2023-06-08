using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using UnityEngine;

public class AuthenticationManager : MonoBehaviour
{
    public static AuthenticationManager manager;

    private void OnEnable() {
        if(AuthenticationManager.manager == null) {
            AuthenticationManager.manager = this;
        } else {
            if(AuthenticationManager.manager != this) {
                Destroy(this);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public async Task Authenticate() {
        if(!IsLogged()) {
            AuthenticationService.Instance.ClearSessionToken();
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
    }
    public bool IsLogged() {
        return AuthenticationService.Instance.IsSignedIn;
    }
    public string GetPlayerId() {
        return AuthenticationService.Instance.PlayerId;
    }
}
