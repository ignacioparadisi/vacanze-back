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
			conec = new BaggageRepository();
		}

		[Test,  Order(1)]
		public void GetBaggageEspecificTest() {
			Baggage = controller.Get(5);
			response = Baggage.Value.Count();
			Assert.AreEqual(1, response);
		}

		[Test,  Order(2)]
		public void GetBaggagestatusTest()
		{
			Baggage =controller.GetStatus("EXTRAVIADO");
			response = Baggage.Value.Count();
			Assert.True(response >1);		
		}
		
		[ Test, Order(3)]
		public void GetBaggageGetDocumentTest()
		{
			Baggage =controller.GetDocument("26055828");
			response = Baggage.Value.Count();
			Assert.True(response >= 0);		
		}

		[Test,  Order(4)]
		public void PutBaggageStatusTest()
		{
			Baggage cs = new Baggage(1,"","EXTRAVIADO");
			controller.Put(5,cs);
		    ActionResult<IEnumerable<Baggage>> enumerable = controller.Get(5);			
			Baggage[] baggage = enumerable.Value.ToArray();
			Assert.AreEqual(baggage[0]._status, "EXTRAVIADO");
		}

		[Test, Order(5)]
		public void NullBaggageExceptionModifyBaggageStatusTest()
		{
			var p= new Baggage(1,"UNITARIA","EXTRAVIADO");

			Assert.Throws<NullBaggageException>(() => conec.ModifyBaggageStatus(0,p));
		}

		[TearDown]
		public void tearDown()
		{
			controller = null;
		}
	} 
	
		
	
}