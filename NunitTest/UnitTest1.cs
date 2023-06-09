using ds0623;
using Microsoft.VisualBasic;

namespace TestProgram
{
	public class Tests
	{
		[Test]
		public void Test1()
		{
			//ARRANGE
			string
			toolCode = "JAKR",
			checkOutDate = "9/3/15",
			rentalDays = "5",
			discount = "101";
			//ACT
			string
			testToolCode = Program.GetToolOption(toolCode),
			testToolType = Program.GetToolType(toolCode),
			testToolBrand = Program.GetToolBrand(toolCode),
			testRentalDays = Program.GetRentalDays(rentalDays).ToString(),
			testCheckOutDate = Program.GetRentalStartDate(checkOutDate).ToString("MM/dd/yy"),
			testDueDate = Program.GetDueDate(checkOutDate, rentalDays),
			testDailyRentalCharge = Program.GetToolPrice(testToolType);
			
			//ASSERT
			Console.WriteLine("TEST1");
			Console.WriteLine(testToolCode);
			Console.WriteLine(testToolType);
			Console.WriteLine(testToolBrand);
			Console.WriteLine(testRentalDays);
			Console.WriteLine(testCheckOutDate);
			Console.WriteLine(testDueDate);
			Console.WriteLine(testDailyRentalCharge);
			if (testToolCode == toolCode &&
				testToolType == "Jackhammer" &&
				testToolBrand == "Ridgid" &&
				testRentalDays == "5" &&
				testCheckOutDate == "09/04/15" &&
				testDueDate == "09/08/15" &&
				testDailyRentalCharge == "2.99")
			{
				Assert.Pass();
			}
			else
			{
				Assert.Fail();
			}
		}
		[Test]
		public void Test2()
		{
			//ARRANGE
			string
			toolCode = "LADW",
			checkOutDate = "7/2/20",
			rentalDays = "3",
			discount = "10";

			//ACT
			string
			testToolCode = Program.GetToolOption(toolCode),
			testToolType = Program.GetToolType(toolCode),
			testToolBrand = Program.GetToolBrand(toolCode),
			testRentalDays = Program.GetRentalDays(rentalDays).ToString(),
			testCheckOutDate = Program.GetRentalStartDate(checkOutDate).ToString("MM/dd/yy"),
			testDueDate = Program.GetDueDate(checkOutDate, rentalDays),
			testDailyRentalCharge = Program.GetToolPrice(testToolType),
			testChargeDays = Program.DateCountToCharge(ds0623.Program.GetUserSelections(toolCode, rentalDays, discount, checkOutDate)),
			testPreDiscountCharge = Program.PreDiscountCharge(testChargeDays, testDailyRentalCharge),
			testDiscountPercent = Program.GetDiscount(discount).ToString(),
			testDiscountAmount = Program.DiscountAmount(testDiscountPercent, testPreDiscountCharge),
			testTotalAmount = Program.CalulcateTotal(testPreDiscountCharge, testDiscountAmount);

			//ASSERT
			Console.WriteLine("TEST2");
			Console.WriteLine(testToolCode);
			Console.WriteLine(testToolType);
			Console.WriteLine(testToolBrand);
			Console.WriteLine(testRentalDays);
			Console.WriteLine(testCheckOutDate);
			Console.WriteLine(testDueDate);
			Console.WriteLine(testDailyRentalCharge);
			Console.WriteLine(testChargeDays);
			Console.WriteLine(testPreDiscountCharge);
			Console.WriteLine(testDiscountPercent);
			Console.WriteLine(testDiscountAmount);
			Console.WriteLine(testTotalAmount);
			if (testToolCode == toolCode &&
				testToolType == "Ladder" &&
				testToolBrand == "Werner" &&
				testRentalDays == "3" &&
				testCheckOutDate == "07/03/20" &&
				testDueDate == "07/05/20" &&
				testDailyRentalCharge == "1.99" &&
				testChargeDays == "2" &&
				testPreDiscountCharge == "3.98" &&
				testDiscountPercent == "10" &&
				testDiscountAmount == "0.40" &&
				testTotalAmount == "3.58")
			{
				Assert.Pass();
			}
			else
			{
				Assert.Fail();
			}
		}
		[Test]
		public void Test3()
		{
			//ARRANGE
			string
			toolCode = "CHNS",
			checkOutDate = "7/2/15",
			rentalDays = "5",
			discount = "25";

			//ACT
			string
			testToolCode = Program.GetToolOption(toolCode),
			testToolType = Program.GetToolType(toolCode),
			testToolBrand = Program.GetToolBrand(toolCode),
			testRentalDays = Program.GetRentalDays(rentalDays).ToString(),
			testCheckOutDate = Program.GetRentalStartDate(checkOutDate).ToString("MM/dd/yy"),
			testDueDate = Program.GetDueDate(checkOutDate, rentalDays),
			testDailyRentalCharge = Program.GetToolPrice(testToolType),
			testChargeDays = Program.DateCountToCharge(ds0623.Program.GetUserSelections(toolCode, rentalDays, discount, checkOutDate)),
			testPreDiscountCharge = Program.PreDiscountCharge(testChargeDays, testDailyRentalCharge),
			testDiscountPercent = Program.GetDiscount(discount).ToString(),
			testDiscountAmount = Program.DiscountAmount(testDiscountPercent, testPreDiscountCharge),
			testTotalAmount = Program.CalulcateTotal(testPreDiscountCharge, testDiscountAmount);

			//ASSERT
			Console.WriteLine("TEST3");
			Console.WriteLine(testToolCode);
			Console.WriteLine(testToolType);
			Console.WriteLine(testToolBrand);
			Console.WriteLine(testRentalDays);
			Console.WriteLine(testCheckOutDate);
			Console.WriteLine(testDueDate);
			Console.WriteLine(testDailyRentalCharge);
			Console.WriteLine(testChargeDays);
			Console.WriteLine(testPreDiscountCharge);
			Console.WriteLine(testDiscountPercent);
			Console.WriteLine(testDiscountAmount);
			Console.WriteLine(testTotalAmount);
			if (testToolCode == toolCode &&
				testToolType == "Chainsaw" &&
				testToolBrand == "Stihl" &&
				testRentalDays == "5" &&
				testCheckOutDate == "07/03/15" &&
				testDueDate == "07/07/15" &&
				testDailyRentalCharge == "1.49" &&
				testChargeDays == "3" &&
				testPreDiscountCharge == "4.47" &&
				testDiscountPercent == "25" &&
				testDiscountAmount == "1.12" &&
				testTotalAmount == "3.35")
			{
				Assert.Pass();
			}
			else
			{
				Assert.Fail();
			}
		}
		[Test]
		public void Test4()
		{
			//ARRANGE
			string
			toolCode = "JAKD",
			checkOutDate = "9/3/15",
			rentalDays = "6",
			discount = "0";

			//ACT
			string
			testToolCode = Program.GetToolOption(toolCode),
			testToolType = Program.GetToolType(toolCode),
			testToolBrand = Program.GetToolBrand(toolCode),
			testRentalDays = Program.GetRentalDays(rentalDays).ToString(),
			testCheckOutDate = Program.GetRentalStartDate(checkOutDate).ToString("MM/dd/yy"),
			testDueDate = Program.GetDueDate(checkOutDate, rentalDays),
			testDailyRentalCharge = Program.GetToolPrice(testToolType),
			testChargeDays = Program.DateCountToCharge(ds0623.Program.GetUserSelections(toolCode, rentalDays, discount, checkOutDate)),
			testPreDiscountCharge = Program.PreDiscountCharge(testChargeDays, testDailyRentalCharge),
			testDiscountPercent = Program.GetDiscount(discount).ToString(),
			testDiscountAmount = Program.DiscountAmount(testDiscountPercent, testPreDiscountCharge),
			testTotalAmount = Program.CalulcateTotal(testPreDiscountCharge, testDiscountAmount);

			//ASSERT
			Console.WriteLine("TEST4");
			Console.WriteLine(testToolCode);
			Console.WriteLine(testToolType);
			Console.WriteLine(testToolBrand);
			Console.WriteLine(testRentalDays);
			Console.WriteLine(testCheckOutDate);
			Console.WriteLine(testDueDate);
			Console.WriteLine(testDailyRentalCharge);
			Console.WriteLine(testChargeDays);
			Console.WriteLine(testPreDiscountCharge);
			Console.WriteLine(testDiscountPercent);
			Console.WriteLine(testDiscountAmount);
			Console.WriteLine(testTotalAmount);
			if (testToolCode == toolCode &&
				testToolType == "Jackhammer" &&
				testToolBrand == "DeWalt" &&
				testRentalDays == "6" &&
				testCheckOutDate == "09/04/15" &&
				testDueDate == "09/09/15" &&
				testDailyRentalCharge == "2.99" &&
				testChargeDays == "3" &&
				testPreDiscountCharge == "8.97" &&
				testDiscountPercent == "0" &&
				testDiscountAmount == "0.00" &&
				testTotalAmount == "8.97")
			{
				Assert.Pass();
			}
			else
			{
				Assert.Fail();
			}
		}
		[Test]
		public void Test5()
		{
			//ARRANGE
			string
			toolCode = "JAKR",
			checkOutDate = "7/2/15",
			rentalDays = "9",
			discount = "0";

			//ACT
			string
			testToolCode = Program.GetToolOption(toolCode),
			testToolType = Program.GetToolType(toolCode),
			testToolBrand = Program.GetToolBrand(toolCode),
			testRentalDays = Program.GetRentalDays(rentalDays).ToString(),
			testCheckOutDate = Program.GetRentalStartDate(checkOutDate).ToString("MM/dd/yy"),
			testDueDate = Program.GetDueDate(checkOutDate, rentalDays),
			testDailyRentalCharge = Program.GetToolPrice(testToolType),
			testChargeDays = Program.DateCountToCharge(ds0623.Program.GetUserSelections(toolCode, rentalDays, discount, checkOutDate)),
			testPreDiscountCharge = Program.PreDiscountCharge(testChargeDays, testDailyRentalCharge),
			testDiscountPercent = Program.GetDiscount(discount).ToString(),
			testDiscountAmount = Program.DiscountAmount(testDiscountPercent, testPreDiscountCharge),
			testTotalAmount = Program.CalulcateTotal(testPreDiscountCharge, testDiscountAmount);

			//ASSERT
			Console.WriteLine("TEST5");
			Console.WriteLine(testToolCode);
			Console.WriteLine(testToolType);
			Console.WriteLine(testToolBrand);
			Console.WriteLine(testRentalDays);
			Console.WriteLine(testCheckOutDate);
			Console.WriteLine(testDueDate);
			Console.WriteLine(testDailyRentalCharge);
			Console.WriteLine(testChargeDays);
			Console.WriteLine(testPreDiscountCharge);
			Console.WriteLine(testDiscountPercent);
			Console.WriteLine(testDiscountAmount);
			Console.WriteLine(testTotalAmount);
			if (testToolCode == toolCode &&
				testToolType == "Jackhammer" &&
				testToolBrand == "Ridgid" &&
				testRentalDays == "9" &&
				testCheckOutDate == "07/03/15" &&
				testDueDate == "07/11/15" &&
				testDailyRentalCharge == "2.99" &&
				testChargeDays == "6" &&
				testPreDiscountCharge == "17.94" &&
				testDiscountPercent == "0" &&
				testDiscountAmount == "0.00" &&
				testTotalAmount == "17.94")
			{
				Assert.Pass();
			}
			else
			{
				Assert.Fail();
			}
		}
		[Test]
		public void Test6()
		{
			//ARRANGE
			string
			toolCode = "JAKR",
			checkOutDate = "7/2/20",
			rentalDays = "4",
			discount = "50";

			//ACT
			string
			testToolCode = Program.GetToolOption(toolCode),
			testToolType = Program.GetToolType(toolCode),
			testToolBrand = Program.GetToolBrand(toolCode),
			testRentalDays = Program.GetRentalDays(rentalDays).ToString(),
			testCheckOutDate = Program.GetRentalStartDate(checkOutDate).ToString("MM/dd/yy"),
			testDueDate = Program.GetDueDate(checkOutDate, rentalDays),
			testDailyRentalCharge = Program.GetToolPrice(testToolType),
			testChargeDays = Program.DateCountToCharge(ds0623.Program.GetUserSelections(toolCode, rentalDays, discount, checkOutDate)),
			testPreDiscountCharge = Program.PreDiscountCharge(testChargeDays, testDailyRentalCharge),
			testDiscountPercent = Program.GetDiscount(discount).ToString(),
			testDiscountAmount = Program.DiscountAmount(testDiscountPercent, testPreDiscountCharge),
			testTotalAmount = Program.CalulcateTotal(testPreDiscountCharge, testDiscountAmount);

			//ASSERT
			Console.WriteLine("TEST6");
			Console.WriteLine(testToolCode);
			Console.WriteLine(testToolType);
			Console.WriteLine(testToolBrand);
			Console.WriteLine(testRentalDays);
			Console.WriteLine(testCheckOutDate);
			Console.WriteLine(testDueDate);
			Console.WriteLine(testDailyRentalCharge);
			Console.WriteLine(testChargeDays);
			Console.WriteLine(testPreDiscountCharge);
			Console.WriteLine(testDiscountPercent);
			Console.WriteLine(testDiscountAmount);
			Console.WriteLine(testTotalAmount);
			if (testToolCode == toolCode &&
				testToolType == "Jackhammer" &&
				testToolBrand == "Ridgid" &&
				testRentalDays == "4" &&
				testCheckOutDate == "07/03/20" &&
				testDueDate == "07/06/20" &&
				testDailyRentalCharge == "2.99" &&
				testChargeDays == "2" &&
				testPreDiscountCharge == "5.98" &&
				testDiscountPercent == "50" &&
				testDiscountAmount == "2.99" &&
				testTotalAmount == "2.99")
			{
				Assert.Pass();
			}
			else
			{
				Assert.Fail();
			}
		}
	}
}