using employeeSample.API.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace employeeSample.API.Authentication
{
    public class BasicAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly AssetManagementContext _dbContext;

        public BasicAuthHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            AssetManagementContext dbContext)
            : base(options, logger, encoder, clock)
        {
            _dbContext = dbContext;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("Unauthorized");
            }

            string authorizationHeader = Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(authorizationHeader))
            {
                return AuthenticateResult.Fail("Unauthorized");
            }

            if (!authorizationHeader.StartsWith("basic ", StringComparison.OrdinalIgnoreCase))
            {
                return AuthenticateResult.Fail("Unauthorized");
            }

            var token = authorizationHeader.Substring(6);
            string credentialAsString;
            try
            {
                credentialAsString = Encoding.UTF8.GetString(Convert.FromBase64String(token));
            }
            catch (FormatException)
            {
                return AuthenticateResult.Fail("Unauthorized");
            }

            var credentials = credentialAsString?.Split(':');
            if (credentials?.Length != 2)
            {
                return AuthenticateResult.Fail("Unauthorized");
            }

            var gid = credentials[0];
            var password = credentials[1];

            // Check credentials against the database
            var employee = await _dbContext.Employeemasters
                .FirstOrDefaultAsync(e => e.Gid == gid && e.Password == password);

            if (employee == null)
            {
                return AuthenticateResult.Fail("Unauthorized");
            }

            // Fetch roles for the employee
            var roles = await _dbContext.EmployeeRoles
                .Where(er => er.EmployeeId == employee.EmployeeId)
                .Select(er => er.Role.RoleName)
                .ToListAsync();

            // Create claims and identity
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, employee.FullName),
                new Claim(ClaimTypes.Name, gid)
            };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var identity = new ClaimsIdentity(claims, "basic");
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }
}
