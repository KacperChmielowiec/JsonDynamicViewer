
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MyApp1.models
{
    public class IdentityContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {

        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options) { }
    }
}
