using RealTimeWebChat.Presentation.Requests.Login;


    public interface IAuthService
    {
        public Task<string> LoginAsync(LoginRequest request);
    }

