namespace AssemblyLine.Models
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
            UserRoles = new string[] {};
        }

        public string[] UserRoles { get; set; }
    }
}