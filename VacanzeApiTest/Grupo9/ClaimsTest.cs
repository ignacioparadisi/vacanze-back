using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using vacanze_back.VacanzeApi.Common.Entities.Grupo9;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo9;
using vacanze_back.VacanzeApi.Services.Controllers.Grupo9;
namespace vacanze_back.VacanzeApiTest.Grupo9
{
	[TestFixture]
	public class ClaimsTest
	{
		ActionResult<IEnumerable<Claim>> claim;
		ClaimController controller;
		ClaimSecundary cs;
		 ClaimRepository conec;
		int response;
		[SetUp]
		public void setup()
		{
			controller = new ClaimController();
			cs = new ClaimSecundary();
			conec= new ClaimRepository();

		}

		[Test]
		public void GetClaimsTest()
		{

			int rows = controller.Get();
			Assert.True(0 <= rows);
		}
		
		

		[Test]
		public void PostClaimTest()
		{ 
			cs.title = "Probando";
			cs.description = "Esta es mi descripcion";
			cs.status = "ABIERTO";
			
			int rows= controller.Get();
			controller.Post(7,cs);
			Assert.AreEqual( rows + 1 , controller.Get());
		}
		[Test]
		public void GetClaimEspecificTest() {
			ActionResult<IEnumerable<Claim>> enumerable = controller.Get(0);
			Claim claimList = enumerable.Value.ToList().Find(x => x._title.Equals("Probando"));

			//response = claimList.Value.Count();
			Assert.AreEqual("Probando", claimList._title);
		}

		[Test]
		public void PutClaimTitleTest()
		{
			cs.title = "Despues del put";
			cs.description = "descripcion despues";
			controller.Put(3,cs);
		    ActionResult<IEnumerable<Claim>> enumerable = controller.Get(3);			
			Claim[] claim = enumerable.Value.ToArray();
			Assert.AreEqual(claim[0]._title, "Despues del put");
			
		}
		[Test]
		public void PutClaimStatusTest()
		{
			cs.status = "CERRADO";
			controller.Put(3,cs);
			ActionResult<IEnumerable<Claim>> enumerable = controller.Get(3);			
			Claim[] claim = enumerable.Value.ToArray();
			Assert.AreEqual(claim[0]._status, "CERRADO");
			
		}
		[Test]
		public void GetClaimStatusTest()
		{
			claim =controller.GetStatus("ABIERTO");
			response = claim.Value.Count();
			Assert.True(response >1);		
		}

		public void GetClaimGetDocumentTest()
		{
			claim =controller.GetDocument("1");
			response = claim.Value.Count();
			Assert.True(response >= 0);		
		}
		
		[Test]
		public void DeleteClaimTest()
		{	//se pone un id que exista en la bd por lo menos el 7 
			ActionResult<IEnumerable<Claim>> enumerable = controller.Get(0);
			Claim claimList = enumerable.Value.ToList().Find(x => x._title.Equals("Probando"));
			
			controller.Delete(Convert.ToInt32(claimList.Id));
			claim = controller.Get(Convert.ToInt32(claimList.Id));
			response = claim.Value.Count();
			Assert.AreEqual(0, response);
		}

		[Test]
		public void NullClaimExceptionDeleteTest()
		{
			Assert.Throws<NullClaimException>(() => conec.DeleteClaim(-1));
		}

		[Test]
		public void NullClaimExceptionModifyTitleTest()
		{
			var p= new Claim("PROBANDO","UNITARIA","CERRADO");

			Assert.Throws<NullClaimException>(() => conec.ModifyClaimTitle(-1,p));
		}
		[Test]
		public void NullClaimExceptionModifyStatusTest()
		{
			var p= new Claim("PROBANDO","UNITARIA","CERRADO");

			Assert.Throws<NullClaimException>(() => conec.ModifyClaimStatus(-1,p));
		}

		[Test]
		public void ValidateClaimTest()
		{
			Claim c = new Claim("validando","mi test", "mal");
			Assert.Throws<AttributeValueException>((() => c.Validate()));
		}
		
		[TearDown]
		public void tearDown()
		{
			controller = null;
			cs = null;
			conec = null;
		}
	} 
	
		
	
}