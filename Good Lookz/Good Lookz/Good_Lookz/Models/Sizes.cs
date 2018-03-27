using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Good_Lookz.Models
{
	class Sizes
	{
		public class AllSizes
		{
			public string sType { set; get; }
			public string sRegion { set; get; }
			public string sNr { set; get; }
			public string sSize { set; get; }
			public string sGender { set; get; }
		}

		static public List<AllSizes> getAllSizesList()
		{
			List<AllSizes> lList = new List<AllSizes>();

			#region Vrouwen tops
			lList.Add(new AllSizes { sRegion = "EU", sNr = "1", sSize = "34", sGender = "F", sType = "top" });
			lList.Add(new AllSizes { sRegion = "EU", sNr = "2", sSize = "36", sGender = "F", sType = "top" });
			lList.Add(new AllSizes { sRegion = "EU", sNr = "3", sSize = "38", sGender = "F", sType = "top" });
			lList.Add(new AllSizes { sRegion = "EU", sNr = "4", sSize = "40", sGender = "F", sType = "top" });
			lList.Add(new AllSizes { sRegion = "EU", sNr = "5", sSize = "42", sGender = "F", sType = "top" });
			lList.Add(new AllSizes { sRegion = "EU", sNr = "6", sSize = "44", sGender = "F", sType = "top" });
			lList.Add(new AllSizes { sRegion = "EU", sNr = "7", sSize = "46", sGender = "F", sType = "top" });

			lList.Add(new AllSizes { sRegion = "US", sNr = "1", sSize = "2", sGender = "F", sType = "top" });
			lList.Add(new AllSizes { sRegion = "US", sNr = "2", sSize = "4", sGender = "F", sType = "top" });
			lList.Add(new AllSizes { sRegion = "US", sNr = "3", sSize = "6", sGender = "F", sType = "top" });
			lList.Add(new AllSizes { sRegion = "US", sNr = "4", sSize = "8", sGender = "F", sType = "top" });
			lList.Add(new AllSizes { sRegion = "US", sNr = "5", sSize = "10", sGender = "F", sType = "top" });
			lList.Add(new AllSizes { sRegion = "US", sNr = "6", sSize = "12", sGender = "F", sType = "top" });
			lList.Add(new AllSizes { sRegion = "US", sNr = "7", sSize = "14", sGender = "F", sType = "top" });

			lList.Add(new AllSizes { sRegion = "UK", sNr = "1", sSize = "6", sGender = "F", sType = "top" });
			lList.Add(new AllSizes { sRegion = "UK", sNr = "2", sSize = "8", sGender = "F", sType = "top" });
			lList.Add(new AllSizes { sRegion = "UK", sNr = "3", sSize = "10", sGender = "F", sType = "top" });
			lList.Add(new AllSizes { sRegion = "UK", sNr = "4", sSize = "12", sGender = "F", sType = "top" });
			lList.Add(new AllSizes { sRegion = "UK", sNr = "5", sSize = "14", sGender = "F", sType = "top" });
			lList.Add(new AllSizes { sRegion = "UK", sNr = "6", sSize = "16", sGender = "F", sType = "top" });
			lList.Add(new AllSizes { sRegion = "UK", sNr = "7", sSize = "18", sGender = "F", sType = "top" });
			#endregion

			#region Mannen tops
			lList.Add(new AllSizes { sRegion = "EU", sNr = "1", sSize = "44", sGender = "M", sType = "top" });
			lList.Add(new AllSizes { sRegion = "EU", sNr = "2", sSize = "46", sGender = "M", sType = "top" });
			lList.Add(new AllSizes { sRegion = "EU", sNr = "3", sSize = "50", sGender = "M", sType = "top" });
			lList.Add(new AllSizes { sRegion = "EU", sNr = "4", sSize = "54", sGender = "M", sType = "top" });
			lList.Add(new AllSizes { sRegion = "EU", sNr = "5", sSize = "58", sGender = "M", sType = "top" });
			lList.Add(new AllSizes { sRegion = "EU", sNr = "6", sSize = "60", sGender = "M", sType = "top" });
			lList.Add(new AllSizes { sRegion = "EU", sNr = "7", sSize = "62", sGender = "M", sType = "top" });

			lList.Add(new AllSizes { sRegion = "US", sNr = "1", sSize = "13", sGender = "M", sType = "top" });
			lList.Add(new AllSizes { sRegion = "US", sNr = "2", sSize = "14", sGender = "M", sType = "top" });
			lList.Add(new AllSizes { sRegion = "US", sNr = "3", sSize = "15", sGender = "M", sType = "top" });
			lList.Add(new AllSizes { sRegion = "US", sNr = "4", sSize = "16", sGender = "M", sType = "top" });
			lList.Add(new AllSizes { sRegion = "US", sNr = "5", sSize = "17", sGender = "M", sType = "top" });
			lList.Add(new AllSizes { sRegion = "US", sNr = "6", sSize = "18", sGender = "M", sType = "top" });
			lList.Add(new AllSizes { sRegion = "US", sNr = "7", sSize = "19", sGender = "M", sType = "top" });

			lList.Add(new AllSizes { sRegion = "UK", sNr = "1", sSize = "13", sGender = "M", sType = "top" });
			lList.Add(new AllSizes { sRegion = "UK", sNr = "2", sSize = "14", sGender = "M", sType = "top" });
			lList.Add(new AllSizes { sRegion = "UK", sNr = "3", sSize = "15", sGender = "M", sType = "top" });
			lList.Add(new AllSizes { sRegion = "UK", sNr = "4", sSize = "16", sGender = "M", sType = "top" });
			lList.Add(new AllSizes { sRegion = "UK", sNr = "5", sSize = "17", sGender = "M", sType = "top" });
			lList.Add(new AllSizes { sRegion = "UK", sNr = "6", sSize = "18", sGender = "M", sType = "top" });
			lList.Add(new AllSizes { sRegion = "UK", sNr = "7", sSize = "19", sGender = "M", sType = "top" });
			#endregion

			#region Vrouwen bottoms
			lList.Add(new AllSizes { sRegion = "EU", sNr = "1", sSize = "32", sGender = "F", sType = "bottom" });
			lList.Add(new AllSizes { sRegion = "EU", sNr = "2", sSize = "34", sGender = "F", sType = "bottom" });
			lList.Add(new AllSizes { sRegion = "EU", sNr = "3", sSize = "36", sGender = "F", sType = "bottom" });
			lList.Add(new AllSizes { sRegion = "EU", sNr = "4", sSize = "38", sGender = "F", sType = "bottom" });
			lList.Add(new AllSizes { sRegion = "EU", sNr = "5", sSize = "40", sGender = "F", sType = "bottom" });
			lList.Add(new AllSizes { sRegion = "EU", sNr = "6", sSize = "42", sGender = "F", sType = "bottom" });
			lList.Add(new AllSizes { sRegion = "EU", sNr = "7", sSize = "44", sGender = "F", sType = "bottom" });
			lList.Add(new AllSizes { sRegion = "EU", sNr = "8", sSize = "46", sGender = "F", sType = "bottom" });
			lList.Add(new AllSizes { sRegion = "EU", sNr = "9", sSize = "48", sGender = "F", sType = "bottom" });

			lList.Add(new AllSizes { sRegion = "US", sNr = "1", sSize = "4", sGender = "F", sType = "bottom" });
			lList.Add(new AllSizes { sRegion = "US", sNr = "2", sSize = "6", sGender = "F", sType = "bottom" });
			lList.Add(new AllSizes { sRegion = "US", sNr = "3", sSize = "8", sGender = "F", sType = "bottom" });
			lList.Add(new AllSizes { sRegion = "US", sNr = "4", sSize = "10", sGender = "F", sType = "bottom" });
			lList.Add(new AllSizes { sRegion = "US", sNr = "5", sSize = "12", sGender = "F", sType = "bottom" });
			lList.Add(new AllSizes { sRegion = "US", sNr = "6", sSize = "14", sGender = "F", sType = "bottom" });
			lList.Add(new AllSizes { sRegion = "US", sNr = "7", sSize = "16", sGender = "F", sType = "bottom" });
			lList.Add(new AllSizes { sRegion = "US", sNr = "8", sSize = "18", sGender = "F", sType = "bottom" });
			lList.Add(new AllSizes { sRegion = "US", sNr = "9", sSize = "20", sGender = "F", sType = "bottom" });

			lList.Add(new AllSizes { sRegion = "UK", sNr = "1", sSize = "0", sGender = "F", sType = "bottom" });
			lList.Add(new AllSizes { sRegion = "UK", sNr = "2", sSize = "2", sGender = "F", sType = "bottom" });
			lList.Add(new AllSizes { sRegion = "UK", sNr = "3", sSize = "4", sGender = "F", sType = "bottom" });
			lList.Add(new AllSizes { sRegion = "UK", sNr = "4", sSize = "6", sGender = "F", sType = "bottom" });
			lList.Add(new AllSizes { sRegion = "UK", sNr = "5", sSize = "8", sGender = "F", sType = "bottom" });
			lList.Add(new AllSizes { sRegion = "UK", sNr = "6", sSize = "10", sGender = "F", sType = "bottom" });
			lList.Add(new AllSizes { sRegion = "UK", sNr = "7", sSize = "12", sGender = "F", sType = "bottom" });
			lList.Add(new AllSizes { sRegion = "UK", sNr = "8", sSize = "14", sGender = "F", sType = "bottom" });
			lList.Add(new AllSizes { sRegion = "UK", sNr = "9", sSize = "16", sGender = "F", sType = "bottom" });
			#endregion

			#region Mannen bottoms
			lList.Add(new AllSizes { sRegion = "EU", sNr = "1", sSize = "32", sGender = "M", sType = "bottom" });
			lList.Add(new AllSizes { sRegion = "EU", sNr = "2", sSize = "34", sGender = "M", sType = "bottom" });
			lList.Add(new AllSizes { sRegion = "EU", sNr = "3", sSize = "36", sGender = "M", sType = "bottom" });
			lList.Add(new AllSizes { sRegion = "EU", sNr = "4", sSize = "38", sGender = "M", sType = "bottom" });
			lList.Add(new AllSizes { sRegion = "EU", sNr = "5", sSize = "40", sGender = "M", sType = "bottom" });
			lList.Add(new AllSizes { sRegion = "EU", sNr = "6", sSize = "42", sGender = "M", sType = "bottom" });
			lList.Add(new AllSizes { sRegion = "EU", sNr = "7", sSize = "44", sGender = "M", sType = "bottom" });
			lList.Add(new AllSizes { sRegion = "EU", sNr = "8", sSize = "46", sGender = "M", sType = "bottom" });
			lList.Add(new AllSizes { sRegion = "EU", sNr = "9", sSize = "48", sGender = "M", sType = "bottom" });

			lList.Add(new AllSizes { sRegion = "US", sNr = "1", sSize = "2", sGender = "M", sType = "bottom" });
			lList.Add(new AllSizes { sRegion = "US", sNr = "2", sSize = "4", sGender = "M", sType = "bottom" });
			lList.Add(new AllSizes { sRegion = "US", sNr = "3", sSize = "6", sGender = "M", sType = "bottom" });
			lList.Add(new AllSizes { sRegion = "US", sNr = "4", sSize = "8", sGender = "M", sType = "bottom" });
			lList.Add(new AllSizes { sRegion = "US", sNr = "5", sSize = "10", sGender = "M", sType = "bottom" });
			lList.Add(new AllSizes { sRegion = "US", sNr = "6", sSize = "12", sGender = "M", sType = "bottom" });
			lList.Add(new AllSizes { sRegion = "US", sNr = "7", sSize = "14", sGender = "M", sType = "bottom" });
			lList.Add(new AllSizes { sRegion = "US", sNr = "8", sSize = "16", sGender = "M", sType = "bottom" });
			lList.Add(new AllSizes { sRegion = "US", sNr = "9", sSize = "18", sGender = "M", sType = "bottom" });

			lList.Add(new AllSizes { sRegion = "UK", sNr = "1", sSize = "4", sGender = "M", sType = "bottom" });
			lList.Add(new AllSizes { sRegion = "UK", sNr = "2", sSize = "6", sGender = "M", sType = "bottom" });
			lList.Add(new AllSizes { sRegion = "UK", sNr = "3", sSize = "8", sGender = "M", sType = "bottom" });
			lList.Add(new AllSizes { sRegion = "UK", sNr = "4", sSize = "10", sGender = "M", sType = "bottom" });
			lList.Add(new AllSizes { sRegion = "UK", sNr = "5", sSize = "12", sGender = "M", sType = "bottom" });
			lList.Add(new AllSizes { sRegion = "UK", sNr = "6", sSize = "14", sGender = "M", sType = "bottom" });
			lList.Add(new AllSizes { sRegion = "UK", sNr = "7", sSize = "16", sGender = "M", sType = "bottom" });
			lList.Add(new AllSizes { sRegion = "UK", sNr = "8", sSize = "18", sGender = "M", sType = "bottom" });
			lList.Add(new AllSizes { sRegion = "UK", sNr = "9", sSize = "20", sGender = "M", sType = "bottom" });
			#endregion

			#region Vrouwen feet
			lList.Add(new AllSizes { sRegion = "EU", sNr = "1", sSize = "33", sGender = "F", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "EU", sNr = "2", sSize = "34", sGender = "F", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "EU", sNr = "3", sSize = "35", sGender = "F", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "EU", sNr = "4", sSize = "36", sGender = "F", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "EU", sNr = "5", sSize = "37", sGender = "F", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "EU", sNr = "6", sSize = "38", sGender = "F", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "EU", sNr = "7", sSize = "39", sGender = "F", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "EU", sNr = "8", sSize = "40", sGender = "F", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "EU", sNr = "9", sSize = "41", sGender = "F", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "EU", sNr = "10", sSize = "42", sGender = "F", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "EU", sNr = "11", sSize = "43", sGender = "F", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "EU", sNr = "12", sSize = "44", sGender = "F", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "EU", sNr = "13", sSize = "45", sGender = "F", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "EU", sNr = "14", sSize = "46", sGender = "F", sType = "feet" });

			lList.Add(new AllSizes { sRegion = "US", sNr = "1", sSize = "3", sGender = "F", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "US", sNr = "2", sSize = "4", sGender = "F", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "US", sNr = "3", sSize = "5", sGender = "F", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "US", sNr = "4", sSize = "6", sGender = "F", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "US", sNr = "5", sSize = "6.5", sGender = "F", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "US", sNr = "6", sSize = "7", sGender = "F", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "US", sNr = "7", sSize = "8", sGender = "F", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "US", sNr = "8", sSize = "8.5", sGender = "F", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "US", sNr = "9", sSize = "9", sGender = "F", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "US", sNr = "10", sSize = "10", sGender = "F", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "US", sNr = "11", sSize = "11", sGender = "F", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "US", sNr = "12", sSize = "12", sGender = "F", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "US", sNr = "13", sSize = "13", sGender = "F", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "US", sNr = "14", sSize = "14", sGender = "F", sType = "feet" });

			lList.Add(new AllSizes { sRegion = "UK", sNr = "1", sSize = "1", sGender = "F", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "UK", sNr = "2", sSize = "2", sGender = "F", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "UK", sNr = "3", sSize = "2.5", sGender = "F", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "UK", sNr = "4", sSize = "3", sGender = "F", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "UK", sNr = "5", sSize = "4", sGender = "F", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "UK", sNr = "6", sSize = "4.5", sGender = "F", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "UK", sNr = "7", sSize = "5", sGender = "F", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "UK", sNr = "8", sSize = "6", sGender = "F", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "UK", sNr = "9", sSize = "6.5", sGender = "F", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "UK", sNr = "10", sSize = "7", sGender = "F", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "UK", sNr = "11", sSize = "8", sGender = "F", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "UK", sNr = "12", sSize = "9.5", sGender = "F", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "UK", sNr = "13", sSize = "10", sGender = "F", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "UK", sNr = "14", sSize = "11", sGender = "F", sType = "feet" });
			#endregion

			#region Mannen feet
			lList.Add(new AllSizes { sRegion = "EU", sNr = "1", sSize = "34", sGender = "M", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "EU", sNr = "2", sSize = "35", sGender = "M", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "EU", sNr = "3", sSize = "36", sGender = "M", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "EU", sNr = "4", sSize = "37", sGender = "M", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "EU", sNr = "5", sSize = "38", sGender = "M", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "EU", sNr = "6", sSize = "39", sGender = "M", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "EU", sNr = "7", sSize = "40", sGender = "M", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "EU", sNr = "8", sSize = "41", sGender = "M", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "EU", sNr = "9", sSize = "42", sGender = "M", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "EU", sNr = "10", sSize = "43", sGender = "M", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "EU", sNr = "11", sSize = "44", sGender = "M", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "EU", sNr = "12", sSize = "45", sGender = "M", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "EU", sNr = "13", sSize = "46", sGender = "M", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "EU", sNr = "14", sSize = "47", sGender = "M", sType = "feet" });

			lList.Add(new AllSizes { sRegion = "US", sNr = "1", sSize = "3", sGender = "M", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "US", sNr = "2", sSize = "4", sGender = "M", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "US", sNr = "3", sSize = "5", sGender = "M", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "US", sNr = "4", sSize = "6", sGender = "M", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "US", sNr = "5", sSize = "6.5", sGender = "M", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "US", sNr = "6", sSize = "7", sGender = "M", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "US", sNr = "7", sSize = "8", sGender = "M", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "US", sNr = "8", sSize = "8.5", sGender = "M", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "US", sNr = "9", sSize = "9", sGender = "M", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "US", sNr = "10", sSize = "10", sGender = "M", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "US", sNr = "11", sSize = "11", sGender = "M", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "US", sNr = "12", sSize = "12", sGender = "M", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "US", sNr = "13", sSize = "13", sGender = "M", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "US", sNr = "14", sSize = "14", sGender = "M", sType = "feet" });

			lList.Add(new AllSizes { sRegion = "UK", sNr = "1", sSize = "2", sGender = "M", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "UK", sNr = "2", sSize = "3", sGender = "M", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "UK", sNr = "3", sSize = "3.5", sGender = "M", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "UK", sNr = "4", sSize = "4", sGender = "M", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "UK", sNr = "5", sSize = "5", sGender = "M", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "UK", sNr = "6", sSize = "6", sGender = "M", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "UK", sNr = "7", sSize = "6.5", sGender = "M", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "UK", sNr = "8", sSize = "7", sGender = "M", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "UK", sNr = "9", sSize = "8", sGender = "M", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "UK", sNr = "10", sSize = "9", sGender = "M", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "UK", sNr = "11", sSize = "9.5", sGender = "M", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "UK", sNr = "12", sSize = "10", sGender = "M", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "UK", sNr = "13", sSize = "11", sGender = "M", sType = "feet" });
			lList.Add(new AllSizes { sRegion = "UK", sNr = "14", sSize = "12", sGender = "M", sType = "feet" });
			#endregion

			return lList;
		}
	}
}
