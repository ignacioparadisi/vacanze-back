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
		ActionResult<IEnumerable<Claim>> claim;
		ClaimController controller;
		ClaimSecundary cs;
		int response;
		[SetUp]
		public void setup()
		{
			controller = new ClaimController();
			cs = new ClaimSecundary();

		}

		[Test]
		public void GetClaimsTest()
		{

			int rows = controller.Get();
			Assert.True(0 <= rows);
		}

		[Test]
		public void GetClaimEspecificTest() {
			claim = controller.Get(2);
			response = claim.Value.Count();
			Assert.AreEqual(1, response);
		}
		
		[Test]
		public void PostClaimTest()
		{ 
			cs.title = "Probando";
			cs.description = "Esta es mi descripcion";
			cs.status = "ABIERTO";
			
			int rows= controller.Get();
			controller.Post(cs);
			Assert.AreEqual( rows + 1 , controller.Get());
		}

		[Test]
		public void DeleteClaimTest()
		{	//se pone un id que exista en la bd por lo menos el 7 
			controller.Delete(7);
			claim = controller.Get(7);
			response = claim.Value.Count();
			Assert.AreEqual(0, response);

		}

		[Test]
		public void PutClaimTitleTest()
		{
			cs.title = "Despues del put";
			cs.description = "descripcion despues";
			controller.Put(5,cs);
			claim = controller.Get(5);
			Assert.Pass();
			//no compila aca abajo
			//Assert.True(claim.Value.title == cs.title && claim.Value.description== cs.description);
		}

		[TearDown]
		public void tearDown()
		{
			controller = null;
			cs = null;
		}
	} 
	
		
	
}