namespace BazarJok.Tests.Api.infrastructure
{
    public static class Routes
    {
        public static AdminRoutes Admin { get; set; } = new();
    }

    public class AdminRoutes
    {
        public AdminAuthenticationRoutes Authentication { get; set; } = new();
        public SupportAgentRoutes SupportAgent { get; set; } = new();
        public VendorRoutes Vendor { get; set; } = new(); 
    }

    public class VendorRoutes
    {
        public string GetById { get; set; } = "/api/vendor/{id}";
        public string Put { get; set; } = "/api/vendor/{id}";
        public string Delete { get; set; } = "/api/vendor/{id}";
        public string Get { get; set; } = "/api/vendor";
    }

    public class SupportAgentRoutes
    {
        public string GetById { get; set; } = "/api/supportAgent/{id}";
        public string Put { get; set; } = "/api/supportAgent/{id}";
        public string Delete { get; set; } = "/api/supportAgent/{id}";
        public string Get { get; set; } = "/api/supportAgent";
        public string Post { get; set; } = "/api/supportAgent";
    }

    public class AdminAuthenticationRoutes
    {
        public string SignIn { get; set; } = "/api/auth/SignIn";
        public string GetUserData { get; set; } = "/api/auth/GetUserData";
    }
}