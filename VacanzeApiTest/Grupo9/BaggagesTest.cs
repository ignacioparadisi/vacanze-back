using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using vacanze_back.VacanzeApi.Common.Entities.Grupo9;
using vacanze_back.VacanzeApi.Services.Controllers.Grupo9;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo9;
using vacanze_back.VacanzeApi.Common.Exceptions;

namespace vacanze_back.VacanzeApiTest.Grupo9
{
	[TestFixture]
	public class BaggagesTest
	{
		ActionResult<IEnumerable<Baggage>> Baggage;
		BaggageController controller;
		
		BaggageRepository conec;
		int response;
		[SetUp]
		public void setup()
		{
			controller = new BaggageController();

		}

		[Test]
		public void GetBaggageEspecificTest() {
			Baggage = controller.Get(1);
			response = Baggage.Value.Count();
			Assert.AreEqual(1, response);
		}

		[Test]
		public void GetBaggagestatusTest()
		{
			Baggage =controller.GetStatus("EXTRAVIADO");
			response = Baggage.Value.Count();
			Assert.True(response >1);		
		}

		public void GetBaggageGetDocumentTest()
		{
			Baggage =controller.GetDocument("1");
			response = Baggage.Value.Count();
			Assert.True(response >= 0);		
		}

		[Test]
		public void PutBaggageStatusTest()
		{
			Baggage cs = new Baggage(1,"","CERADO");
			controller.Put(3,cs);
		    ActionResult<IEnumerable<Baggage>> enumerable = controller.Get(3);			
			Baggage[] baggage = enumerable.Value.ToArray();
			Assert.AreEqual(baggage[0]._status, "CERRADO");

		}

		[Test]
		public void NullBaggageExceptionModifyBaggageStatusTest()
		{
			var p= new Baggage(1,"UNITARIA","EXTRAVIADO");

			Assert.Throws<NullBaggageException>(() => conec.ModifyBaggageStatus(-1,p));
		}

		[TearDown]
		public void tearDown()
		{
			controller = null;
		}
	} 
	
		
	
}