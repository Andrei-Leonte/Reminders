using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace YRM.ASPIdentity.Web.Extensions
{
    public class ReminderJwtBearerEvents : JwtBearerEvents
    {
        public override Task AuthenticationFailed(AuthenticationFailedContext context)
        {
            return base.AuthenticationFailed(context);
        }

        public override Task Challenge(JwtBearerChallengeContext context)
        {
            return base.Challenge(context);
        }

        public override Task Forbidden(ForbiddenContext context)
        {
            return base.Forbidden(context);
        }

        public override Task MessageReceived(MessageReceivedContext context)
        {
            return base.MessageReceived(context);
        }

        public override Task TokenValidated(TokenValidatedContext context)
        {
            return base.TokenValidated(context);
        }
    }
}
