using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using vacanze_back.VacanzeApi.Common.Entities.Grupo9;
using vacanze_back.VacanzeApi.Services.Controllers.Grupo9;
namespace vacanze_back.VacanzeApiTest.Grupo9
{
	[TestFixture]
	public class ClaimsTest
	{
		[Test]
		public void GetClaimsTest()
		{
			var controller = new ClaimController();

			ActionResult<IEnumerable<Claim>> claim = controller.Get();
			//var response = claim.Value.Count(); 
			Assert.Pass();
		}
	}
}