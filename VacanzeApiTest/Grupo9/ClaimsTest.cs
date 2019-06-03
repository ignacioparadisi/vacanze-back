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
			conec = new ClaimRepository();

		}
	
		[Test, Order(1)]
		public void GetClaimsTest()
		{

			int rows = controller.Get();
			Assert.True(0 <= rows);
		}



		[Test, Order(2)]
		public void PostClaimTest()
		{
			cs.title = "Probando";
			cs.description = "Esta es mi descripcion";
			cs.status = "ABIERTO";

			int rows = controller.Get();
			controller.Post(2, cs);
			Assert.AreEqual(rows + 1, controller.Get());
		}

		[Test, Order(3)]
		public void GetClaimEspecificTest()
		{
			ActionResult<IEnumerable<Claim>> enumerable = controller.Get(0);
			Claim claimList = enumerable.Value.ToList().Find(x => (x._title.Equals("Probando") && x._description.Equals("Esta es mi descripcion") && x._status.Equals("ABIERTO")) );

			//response = claimList.Value.Count();
			Assert.AreEqual("Probando", claimList._title);
			Assert.AreEqual("Esta es mi descripcion", claimList._description);
			Assert.AreEqual("ABIERTO", claimList._status);
		}

		[Test,Order(4) ]
		public void PutClaimTitleTest()
		{	
			cs.title = "Despues del put";
			cs.description = "descripcion despues";
			ActionResult<IEnumerable<Claim>> enumerable = controller.Get(0);
			Claim claimList = enumerable.Value.ToList().Find(x =>(x._title.Equals("Probando") && x._description.Equals("Esta es mi descripcion")));

			controller.Put(Convert.ToInt32(claimList.Id), cs);
		 enumerable = controller.Get(Convert.ToInt32(claimList.Id));
			Claim[] claim = enumerable.Value.ToArray();
			Assert.AreEqual(claim[0]._title, "Despues del put");
			Assert.AreEqual(claim[0]._description,"descripcion despues");
			Assert.AreEqual(claim[0]._status,"ABIERTO");

		}

		[Test,Order(5) ]
		public void PutClaimStatusTest()
		{
			cs.status = "CERRADO";
			ActionResult<IEnumerable<Claim>> enumerable = controller.Get(0);
			Claim claimList = enumerable.Value.ToList().Find(x => x._title.Equals("Despues del put") && x._description.Equals("descripcion despues"));

			controller.Put(Convert.ToInt32(claimList.Id), cs);
			enumerable = controller.Get(Convert.ToInt32(claimList.Id));
			Claim[] claim = enumerable.Value.ToArray();
			Assert.AreEqual(claim[0]._status, "CERRADO");

		}

		[Test, Order(6)]
		public void GetClaimStatusTest()
		{
			claim = controller.GetStatus("CERRADO");
			response = claim.Value.Count();
			Assert.True(response >= 0);
		}
		[Test, Order(7)]
		public void GetClaimGetDocumentTest()
		{
			claim = controller.GetDocument("20766589");

		response = claim.Value.Count();
			Assert.AreEqual(response , 1);
		}

		[Test, Order(8)]
		public void DeleteClaimTest()
		{
			//se pone un id que exista en la bd por lo menos el 7 
			ActionResult<IEnumerable<Claim>> enumerable = controller.Get(0);
			Claim claimList = enumerable.Value.ToList().Find(x =>(x._title.Equals("Despues del put") && x._description.Equals("descripcion despues")) );
			int rows = controller.Get();
			controller.Delete(Convert.ToInt32(claimList.Id));
			int rowsresponse = controller.Get();
			Assert.AreEqual(rows-1, rowsresponse);
		}

		[Test, Order(9)]
		public void NullClaimExceptionDeleteTest()
		{
			Assert.Throws<NullClaimException>(() => conec.DeleteClaim(-1));
		}

		[Test,Order(10)]
		public void NullClaimExceptionModifyTitleTest()
		{
			var p = new Claim("PROBANDO", "UNITARIA", "CERRADO");

			Assert.Throws<NullClaimException>(() => conec.ModifyClaimTitle(-1, p));
		}

		[Test, Order(11)]
		public void NullClaimExceptionModifyStatusTest()
		{
			var p = new Claim("PROBANDO", "UNITARIA", "CERRADO");

			Assert.Throws<NullClaimException>(() => conec.ModifyClaimStatus(-1, p));
		}

		[Test, Order(12)]
		public void NullClaimExceptionGetClaimTest()
		{
			Assert.Throws<NullClaimException>(() => conec.GetClaim(-1));
		}

		[Test, Order(13)]
		public void NullClaimExceptionGetClaimDocumentTest()
		{			
			Assert.Throws<NullClaimException>(() => conec.GetClaimDocument("0"));
		}

		[Test, Order(14)]
		public void ValidateStatusClaimTest()
		{
			Claim c = new Claim("validando", "mi test", "mal");
			Assert.Throws<AttributeValueException>((() => c.Validate()));
		}

		[Test, Order(15)]
		public void ValidateLengTitleClaimTest()
		{
			Claim c = new Claim("validaaxedededdededededdedendod3dd3dd3d33", "mi test", "ABIERTO");
			Assert.Throws<AttributeSizeException>((() => c.Validate()));
		}

		[Test,Order(16)]
		public void ValidatePutClaimTest()
		{
			Claim c = new Claim("valida", "mi test", "ABIERTO");
			Assert.Throws<AttributeValueException>((() => c.ValidatePut()));
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