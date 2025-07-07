namespace Test_Azienda1.Utilities.Helpers
{
    public class UserInformation
    {
        public UserInformation() { }
        public UserInformation(IHttpContextAccessor httpContextAccessor)
        {
            UserId = "Davide";
        }
        public string UserId { get; set; }
    }
}