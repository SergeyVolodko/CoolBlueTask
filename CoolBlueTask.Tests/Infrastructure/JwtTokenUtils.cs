using System;
using System.Collections.Generic;
using System.Text;
using Jose;

namespace CoolBlueTask.Tests.Infrastructure
{
	public static class JwtTokenUtils
	{
		public static string GenerateToken(
			IApiConfiguration config,
			string userName)
		{
			var secretKey = Encoding.UTF8.GetBytes(config.Auth0Secret);
			var issuedDate = new DateTime(2018, 5, 14);
			var expirationDate = new DateTime(2037, 12, 31);

			var payload = new Dictionary<string, object>
			{
				{ "iss", config.Auth0Issuer },
				{ "aud", config.Auth0Audience },
				{ "sub", userName},
				{ "iat", ToEpochSeconds(issuedDate) },
				{ "exp", ToEpochSeconds(expirationDate) }
			};

			var token = JWT.Encode(payload, secretKey, JwsAlgorithm.HS256);
			return token;
		}

		private static long ToEpochSeconds(DateTime date)
		{
			if (date <= new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc))
			{
				return 0;
			}

			return (long)(date.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc))).TotalSeconds;
		}
	}
}
