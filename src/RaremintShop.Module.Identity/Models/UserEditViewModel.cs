namespace RaremintShop.Module.Identity.Models
{
    public class UserEditViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public List<string> AvailableRoles { get; set; }
        public bool IsActive { get; set; }
    }
}
