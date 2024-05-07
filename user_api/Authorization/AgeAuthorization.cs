using Microsoft.AspNetCore.Authorization;

namespace user_api.Authorization;

public class AgeAuthorization : AuthorizationHandler<MinimalAge>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context, 
        MinimalAge requirement
    )
    {
        var birthDateClaim = context.User.FindFirst(claim => 
            claim.Type == "birthDate" 
        );
        if(birthDateClaim is null) return Task.CompletedTask;

        var birthDate = Convert.ToDateTime(birthDateClaim.Value);

        var userAge = DateTime.Today.Year - birthDate.Year;

        if(birthDate > DateTime.Today.AddYears(-userAge)) userAge--;

        if(userAge >= requirement.Age) {
            context.Succeed(requirement);
        }
        return Task.CompletedTask;
    }
}