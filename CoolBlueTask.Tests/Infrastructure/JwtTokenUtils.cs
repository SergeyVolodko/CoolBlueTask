using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Jose;

namespace CoolBlueTask.Tests.Infrastructure
{
	public static class JwtTokenUtils
	{
		//public static string GenerateToken(
		//	//IConfiguration config,
		//	string userName)
		//{
		//	var secretKey = Encoding.UTF8.GetBytes(config.Auth0Secret);
		//	var issued = new DateTime(2016, 9, 21);
		//	var expire = new DateTime(2037, 12, 31);

		//	var payload = new Dictionary<string, object>
		//	{
		//		{ "iss", config.Auth0Issuer },
		//		{ "aud", config.Auth0Audience },
		//		{ "sub", userName},
		//		{ "iat", issued.ToEpochSeconds() },
		//		{ "exp", expire.ToEpochSeconds() }
		//	};

		//	var token = JWT.Encode(payload, secretKey, JwsAlgorithm.HS256);
		//	return token;
		//}
	}
}
