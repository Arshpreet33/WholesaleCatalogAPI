using Application.Core;

namespace Application.Clients
{
    public class ClientParams : Params
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
    }
}
