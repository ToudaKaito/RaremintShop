namespace RaremintShop.Module.Identity.Models
{
    public class UsersListViewModel
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public IList<string> Roles { get; set; }
    }
}
