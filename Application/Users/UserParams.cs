using Application.Core;

namespace Application.Users
{
    public class UserParams : Params
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
