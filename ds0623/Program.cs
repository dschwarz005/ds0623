using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ds0623
{
	public class Program
	{
		static void Main(string[] args)
		{
			//give user options.
			UserOptions();
			//get user selections
			string toolOption = args[0], rentalDays = args[1], discount = args[2], rentalDate = args[3];
			Dictionary<string, object> userSelections = GetUserSelections(toolOption, rentalDays, discount, rentalDate);
			//return contract text
			OutputUserContract(userSelections);
		}

		public static void OutputUserContract(Dictionary<string, object> userSelections)
		{
			string
			toolCode = userSelections["toolOption"].ToString(),
			toolType = GetToolType(toolCode),
			toolBrand = GetToolBrand(toolCode),
			rentalDays = userSelections["rentalDays"].ToString(),
			checkoutDate = Convert.ToDateTime(userSelections["rentalStartDate"].ToString()).ToString("MM/dd/yy"),
			dueDate = GetDueDate(checkoutDate, rentalDays),
			dailyRentalCharge = GetToolPrice(toolType),
			chargeDays = DateCountToCharge(userSelections),
			preDiscountCharge = PreDiscountCharge(chargeDays, dailyRentalCharge),
			discountPercent = userSelections["discount"].ToString(),
			discountAmount = DiscountAmount(discountPercent, preDiscountCharge),
			total = CalulcateTotal(preDiscountCharge, discountAmount);
			
			//output the contract
			Console.WriteLine("The rental agreement for the selection you have made is:");
			Console.WriteLine("Tool code - {0}", toolCode);
			Console.WriteLine("Tool type - {0}", toolType);
			Console.WriteLine("Tool brand - {0}", toolBrand);
			Console.WriteLine("Rental days - {0}", rentalDays);
			Console.WriteLine("Check out date - {0}", checkoutDate);
			Console.WriteLine("Due date - {0}", dueDate);
			Console.WriteLine("Daily rental charge - ${0}", dailyRentalCharge);
			Console.WriteLine("Charge days - {0}", chargeDays);
			Console.WriteLine("Pre-discount charge - ${0}",preDiscountCharge);
			Console.WriteLine("Discount percent - {0}%",discountPercent);
			Console.WriteLine("Discount amount - ${0}",discountAmount);
			Console.WriteLine("Final Charge - ${0}",total);
		}

		public static string GetDueDate(string checkoutDate, string rentalDays)
		{
			return Convert.ToDateTime(checkoutDate).AddDays(Convert.ToInt32(rentalDays)).ToString("MM/dd/yy");
		}

		public static string GetToolBrand(string toolCode)
		{
			DataTable dt = GetToolTable();
			return dt.AsEnumerable().Where(t => t["Tool Code"].Equals(toolCode)).CopyToDataTable().Rows[0]["Brand"].ToString();
		}

		public static string GetToolType(string toolCode)
		{
			DataTable dt = GetToolTable();
			return dt.AsEnumerable().Where(t => t["Tool Code"].Equals(toolCode)).CopyToDataTable().Rows[0]["Tool Type"].ToString();
		}

		public static string CalulcateTotal(string preDiscountCharge, string discountAmount)
		{
			decimal grossCharge = Convert.ToDecimal(preDiscountCharge);
			decimal discount = Convert.ToDecimal(discountAmount);
			decimal netCharge = grossCharge - discount;
			return netCharge.ToString();
		}

		public static string DiscountAmount(string discountPercent, string preDiscountCharge)
		{
			decimal discount = Convert.ToDecimal(discountPercent)/100;
			decimal charge = Convert.ToDecimal(preDiscountCharge);
			decimal discontAmount = Math.Round(discount * charge, 2);
			return discontAmount.ToString();
		}

		public static string PreDiscountCharge(string chargeDays, string dailyRentalCharge)
		{
			decimal days = Convert.ToDecimal(chargeDays);
			decimal rentalCost = Convert.ToDecimal(dailyRentalCharge);
			decimal preDiscountCharge = days * rentalCost;
			return preDiscountCharge.ToString();
		}

		public static string DateCountToCharge(Dictionary<string, object> userSelections)
		{
			// user userSelections to pull dates the rental is happening for and parse it against current dates to see how many days are being charged.
			DataTable 
			toolPriceDT = GetToolPriceTable(),
			toolTableDT = GetToolTable();
			string toolType = toolTableDT.AsEnumerable().Where(t => t["Tool Code"].ToString().Equals(userSelections["toolOption"].ToString())).CopyToDataTable().Rows[0]["Tool Type"].ToString();
			string[]
			weekdays = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" },
			weekends = { "Saturday", "Sunday" };
			int
			numberOfRentalDays = Convert.ToInt32(userSelections["rentalDays"].ToString()),
			dateCount = 0;
			DateTime 
			rentalDate = Convert.ToDateTime(userSelections["rentalStartDate"].ToString()),
			returnDate = rentalDate.AddDays(numberOfRentalDays);
			DateTime[] holiday = GetHolidayArray(userSelections);
			bool 
			allowsWeekday = Convert.ToBoolean(toolPriceDT.AsEnumerable().Where(w=> w["Tool Type"].ToString().Equals(toolType)).CopyToDataTable().Rows[0]["Weekday Charge"].ToString()),
			allowsWeekend = Convert.ToBoolean(toolPriceDT.AsEnumerable().Where(w => w["Tool Type"].ToString().Equals(toolType)).CopyToDataTable().Rows[0]["Weekend Charge"].ToString()),
			allowsHoliday = Convert.ToBoolean(toolPriceDT.AsEnumerable().Where(w => w["Tool Type"].ToString().Equals(toolType)).CopyToDataTable().Rows[0]["Holiday Charge"].ToString());
			
			
			//iterates through the rental days to check if they are valid charge days
			for (int i = 0; i < numberOfRentalDays; ++i)
			{
				string dayOfWeek = rentalDate.AddDays(i).DayOfWeek.ToString();
				// checks if the iterating day of the week is a weekday or weekend and if holidays are allowed or that it's not currently a holiday.
				if (allowsHoliday || !holiday.Contains(rentalDate.AddDays(i)))
				{
					if (allowsWeekday && weekdays.Contains(dayOfWeek))
					{
						++dateCount;
					}
					else if (allowsWeekend && weekends.Contains(dayOfWeek))
					{
						++dateCount;
					}
				}
			}
			return dateCount.ToString();
		}

		public static DateTime[] GetHolidayArray(Dictionary<string, object> userSelections)
		{
			DateTime[] holidayArray = { DateTime.Now, DateTime.Now };
			string year = Convert.ToDateTime(userSelections["rentalStartDate"].ToString()).Year.ToString();
			//set 4th of july
			holidayArray[0] = Convert.ToDateTime("07/04/"+ year);
			if (holidayArray[0].DayOfWeek.Equals("Saturday"))
			{
				holidayArray[0] = holidayArray[0].AddDays(-1);
			}else if (holidayArray[0].DayOfWeek.Equals("Sunday"))
			{
				holidayArray[0] = holidayArray[0].AddDays(1);
			}
			//set Labor Day
			holidayArray[1] = Convert.ToDateTime("09/01/" + year);
			for (int i = 1; i <= 7; ++i)
			{
				if (!holidayArray[1].DayOfWeek.Equals("Monday"))
				{
					holidayArray[1] = holidayArray[1].AddDays(1);
				}
				else
				{
					break;
				}
			}
			return holidayArray;
		}

		public static Dictionary<string, object> GetUserSelections(string tool, string rentalDays, string discount, string rentaldate)
		{
			Dictionary<string, object> userSelections = new Dictionary<string, object>();
			string userInput;
			
			//get user tool selection
			userSelections.Add("toolOption", "Invalid");
			while (userSelections["toolOption"].ToString() == "Invalid")
			{
				Console.WriteLine("Please enter 'Tool Code' for the tool you wish to rent:");
				if (string.IsNullOrWhiteSpace(tool)){
					userInput = Console.ReadLine();
				}
				else
				{
					userInput = tool;
				}
				userSelections["toolOption"] = GetToolOption(userInput);
			}

			//get user rental days
			userSelections.Add("rentalDays", -1);
			while (Convert.ToInt32(userSelections["rentalDays"]) == -1)
			{
				Console.WriteLine("Please enter how many days wish to rent it for:");
				if (string.IsNullOrWhiteSpace(rentalDays)){
					userInput = Console.ReadLine();
				}
				else
				{
					userInput = rentalDays;
				}
				userSelections["rentalDays"] = GetRentalDays(userInput);
			}

			//get user discount
			userSelections.Add("discount", -1);
			while (Convert.ToInt32(userSelections["discount"])==-1)
			{
				Console.WriteLine("Please enter any discount applicable:");
				if (string.IsNullOrWhiteSpace(discount))
				{
					userInput = Console.ReadLine();
				}
				else
				{
					userInput = discount;
				}
				userSelections["discount"] = GetDiscount(userInput);
			}
			//get user rental start date
			userSelections.Add("rentalStartDate", Convert.ToDateTime("1-1-2000"));
			while (Convert.ToDateTime(userSelections["rentalStartDate"].ToString())== Convert.ToDateTime("1-1-2000"))
			{
				Console.WriteLine("Please enter the date for the rental:");
				if (string.IsNullOrWhiteSpace(rentaldate))
				{
					userInput = Console.ReadLine();
				}
				else
				{
					userInput = rentaldate;
				}
				userSelections["rentalStartDate"] = GetRentalStartDate(userInput);
			}
			return userSelections;
		}

		public static DateTime GetRentalStartDate(string userInput)
		{
			DateTime chosenDate;
			//check if user input is valid.
			if (DateTime.TryParse(userInput, out chosenDate))
			{
				return chosenDate.AddDays(1);
			}
			else
			{
				Console.WriteLine("Please provide rental start date in MM/DD/YY format.");
				return Convert.ToDateTime("1-1-2000");
			}
		}

		public static int GetDiscount(string userInput)
		{
			DataTable dt = GetToolTable();
			int discount;
			//check if user input is valid and a number between 0-100.
			if (Int32.TryParse(userInput.ToString(), out discount) && discount >= 0 && discount <=100)
			{
				return discount;
			}
			else if (!Int32.TryParse(userInput.ToString(), out discount))
			{
				Console.WriteLine("Please provide an integer of how much discount is given. If none, please enter '0'.");
				return -1;
			}
			else
			{
				Console.WriteLine("Please provide a discount amount that is between 0-100.");
				throw new Exception("Discount percent is not in the range 0-100");
			}
		}

		public static int GetRentalDays(string userInput)
		{
			int rentalDays;
			//check if user input is valid and a number.
			if (Int32.TryParse(userInput.ToString(),out rentalDays) && rentalDays>0)
			{
				return rentalDays;
			}
			else if(!Int32.TryParse(userInput.ToString(), out rentalDays))
			{
				Console.WriteLine("Please provide an integer of how many days you would like to rent for.");
				return -1;
			}
			else
			{
				Console.WriteLine("Please provide rental days that are greater than 0.");
				throw new Exception("Rental day count is not 1 or greater.");
			}
		}

		public static string GetToolOption(string userInput)
		{
			DataTable dt = GetToolTable();
			//check if user input exists in tool code.
			if (dt.AsEnumerable().Where(code => code["Tool Code"].ToString().Equals(userInput)).Count() > 0)
			{
				return userInput;
			}else
			{
				Console.WriteLine("Please provide a valid Tool Code.");
				return "Invalid";
			}
		}

		public static void UserOptions()
		{
			DataTable toolDT = GetToolTable();
			Console.WriteLine("Hello Shopper. Here are our options for products:");
			Console.WriteLine("Tool Code   | Tool Type  | Brand      | Price      ");
			for (int i = 0; i < toolDT.Rows.Count; i++)
			{
				Console.WriteLine("{0,-12}| {1,-11}| {2,-11}| {3,-11}", toolDT.Rows[i]["Tool Code"],
					toolDT.Rows[i]["Tool Type"], toolDT.Rows[i]["Brand"],
					GetToolPrice(toolDT.Rows[i]["Tool Type"].ToString()));
			}
			Console.WriteLine("---------------------------------------------");
		}

		public static DataTable GetToolTable()
		{
			DataTable dt = new DataTable();
			dt.Columns.Add("Tool Code");
			dt.Columns.Add("Tool Type");
			dt.Columns.Add("Brand");
			dt.Rows.Add("CHNS", "Chainsaw", "Stihl");
			dt.Rows.Add("LADW", "Ladder", "Werner");
			dt.Rows.Add("JAKD", "Jackhammer", "DeWalt");
			dt.Rows.Add("JAKR", "Jackhammer", "Ridgid");
			return dt;
		}
		public static DataTable GetToolPriceTable()
		{
			DataTable dt = new DataTable();
			dt.Columns.Add("Tool Type");
			dt.Columns.Add("Daily Charge");
			dt.Columns.Add("Weekday Charge");
			dt.Columns.Add("Weekend Charge");
			dt.Columns.Add("Holiday Charge");
			dt.Rows.Add("Ladder", "1.99", true, true, false);
			dt.Rows.Add("Chainsaw", "1.49", true, false, true);
			dt.Rows.Add("Jackhammer", "2.99", true, false, false);
			return dt;
		}
		public static string GetToolPrice(string toolType)
		{
			DataTable dt = GetToolPriceTable();
			string toolPrice;
			try
			{
				// parses toolpricetable against the tool type that was given to return the tool price.
				toolPrice = dt.AsEnumerable().Where(tool => tool["Tool Type"].Equals(toolType)).CopyToDataTable().Rows[0]["Daily Charge"].ToString();
			}
			catch
			{
				toolPrice = "Invalid Tool Type given.";
			}

			return toolPrice;
		}

	}
}
