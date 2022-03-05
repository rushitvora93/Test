using Core.UseCases;

namespace StartUp
{
    public class LogoutGuiProxy : ILogoutGui
    {
        public ILogoutGui Real { get; set; }
        public void FinishLogout()
        {
            Real.FinishLogout();
        }
    }
}