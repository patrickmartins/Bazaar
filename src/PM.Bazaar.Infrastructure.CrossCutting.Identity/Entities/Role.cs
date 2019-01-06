using Microsoft.AspNet.Identity.EntityFramework;

namespace PM.Bazaar.Infrastructure.CrossCutting.Identity.Entities
{
    public class Role : IdentityRole<int, AccountRole> { }
}
