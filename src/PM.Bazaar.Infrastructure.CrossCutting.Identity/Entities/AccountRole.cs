using Microsoft.AspNet.Identity.EntityFramework;

namespace PM.Bazaar.Infrastructure.CrossCutting.Identity.Entities
{
    public class AccountRole : IdentityUserRole<int>
    {
        public virtual Role Role { get; set; }
        public virtual Account User { get; set; }
    }
}
