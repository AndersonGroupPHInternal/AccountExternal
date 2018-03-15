using AccountExternalEntity;
using System.Data.Entity;

namespace AccountExternalContext
{
    public class DBInitializer : CreateDatabaseIfNotExists<Context>
    {
        public DBInitializer()
        {
        }
        protected override void Seed(Context context)
        {
            var role = context.Roles.Add(
                new ERole
                {
                    Name = "ExternalAccountAdministrator"
                });

            context.Roles.Add(
                new ERole
                {
                    Name = "AndersonNotificationAdministrator"
                });

            context.Roles.Add(
                new ERole
                {
                    Name = "AndersonNotificationEmailNotification"
                });

            context.Roles.Add(
                new ERole
                {
                    Name = "SurveyAdministrator"
                });

            string password = "password";
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password + salt);

            var credential = context.Credentials.Add(
                new ECredential
                {
                    IsActive = true,

                    Password = passwordHash,
                    Salt = salt,
                    Username = "admin"
                });

            context.SaveChanges();

            if (role != null && credential != null)
            {
                context.CredentialRoles.Add(
                    new ECredentialRole
                    {
                        CredentialId = credential.CredentialId,
                        RoleId = role.RoleId
                    });
                context.SaveChanges();
            }
        }
    }
}
