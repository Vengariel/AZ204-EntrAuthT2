﻿using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AZ204_EntrAuth.Clients
{
    internal class BaseAppClient
    {
        protected static async Task PrintTokenClaimsAndStalkAfterSuccess(StringBuilder sb, AuthenticationResult? authenticationResult)
        {
            sb.AppendLine($"Access Token: {authenticationResult?.AccessToken}");
            Append2EmptyLines(sb);
            sb.AppendLine($"Id Token: {authenticationResult?.IdToken}");


            foreach (var claim in authenticationResult?.ClaimsPrincipal?.Identities?.FirstOrDefault()?.Claims ?? Enumerable.Empty<Claim>())
            {
                sb.AppendLine($"{claim.Type}: {claim.Value}");
            }

            Append2EmptyLines(sb);
            sb.AppendLine($"STALKING via Graph");
            sb.AppendLine($"{await FlurlIt.StalkThroughGraph(authenticationResult?.AccessToken)}");
            Console.WriteLine(sb.ToString());
        }

        #region private utility stuff
        protected static void Append2EmptyLines(StringBuilder sb)
        {
            sb.AppendLine(Environment.NewLine);
            sb.AppendLine(Environment.NewLine);
        }
        #endregion private utility stuff
    }
}