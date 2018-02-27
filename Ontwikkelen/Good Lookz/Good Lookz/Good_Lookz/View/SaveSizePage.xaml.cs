using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Good_Lookz.View
{
	public partial class SaveSizePage : ContentPage
	{
		public SaveSizePage()
		{
			InitializeComponent();

			//Zorg dat er een region en gender geselecteerd is
			cbRegion.SelectedIndex = 0;
			cbGender.SelectedIndex = 0;

			//Vul waardes in en laad gegevens in de pickers
			FillLists();
			FillPickers("F", "EU");
		}

		#region Globale variabelen
		List<TopSizes> AllItemsTop = new List<TopSizes>();
		List<BottomSizes> AllItemsBottom = new List<BottomSizes>();
		List<FeetSizes> AllItemsFeet = new List<FeetSizes>();

		string _Region = "EU";
		string _Gender = "F";
		#endregion

		//code van: https://stackoverflow.com/questions/32163471/confirmation-dialog-on-back-button-press-event-xamarin-forms voor terugknop
		protected override bool OnBackButtonPressed()
		{
			Device.BeginInvokeOnMainThread(async () =>{await this.DisplayAlert("Message", "Please save your sizes before proceeding", "OK");});

			return true;
		}

		public class TopSizes
		{
			public string sRegion { set; get; }
			public string sNr { set; get; }
			public string sSize { set; get; }
			public string sGender { set; get; }
		}

		public class BottomSizes
		{
			public string sRegion { set; get; }
			public string sNr { set; get; }
			public string sSize { set; get; }
			public string sGender { set; get; }
		}

		public class FeetSizes
		{
			public string sRegion { set; get; }
			public string sNr { set; get; }
			public string sSize { set; get; }
			public string sGender { set; get; }
		}


		private void FillLists()
		{
			#region Vrouwen tops
			AllItemsTop.Add(new TopSizes { sRegion = "EU", sNr = "1", sSize = "34", sGender = "F" });
			AllItemsTop.Add(new TopSizes { sRegion = "EU", sNr = "2", sSize = "36", sGender = "F" });
			AllItemsTop.Add(new TopSizes { sRegion = "EU", sNr = "3", sSize = "38", sGender = "F" });
			AllItemsTop.Add(new TopSizes { sRegion = "EU", sNr = "4", sSize = "40", sGender = "F" });
			AllItemsTop.Add(new TopSizes { sRegion = "EU", sNr = "5", sSize = "42", sGender = "F" });
			AllItemsTop.Add(new TopSizes { sRegion = "EU", sNr = "6", sSize = "44", sGender = "F" });
			AllItemsTop.Add(new TopSizes { sRegion = "EU", sNr = "7", sSize = "46", sGender = "F" });

			AllItemsTop.Add(new TopSizes { sRegion = "US", sNr = "1", sSize = "2", sGender = "F" });
			AllItemsTop.Add(new TopSizes { sRegion = "US", sNr = "2", sSize = "4", sGender = "F" });
			AllItemsTop.Add(new TopSizes { sRegion = "US", sNr = "3", sSize = "6", sGender = "F" });
			AllItemsTop.Add(new TopSizes { sRegion = "US", sNr = "4", sSize = "8", sGender = "F" });
			AllItemsTop.Add(new TopSizes { sRegion = "US", sNr = "5", sSize = "10", sGender = "F" });
			AllItemsTop.Add(new TopSizes { sRegion = "US", sNr = "6", sSize = "12", sGender = "F" });
			AllItemsTop.Add(new TopSizes { sRegion = "US", sNr = "7", sSize = "14", sGender = "F" });

			AllItemsTop.Add(new TopSizes { sRegion = "UK", sNr = "1", sSize = "6", sGender = "F" });
			AllItemsTop.Add(new TopSizes { sRegion = "UK", sNr = "2", sSize = "8", sGender = "F" });
			AllItemsTop.Add(new TopSizes { sRegion = "UK", sNr = "3", sSize = "10", sGender = "F" });
			AllItemsTop.Add(new TopSizes { sRegion = "UK", sNr = "4", sSize = "12", sGender = "F" });
			AllItemsTop.Add(new TopSizes { sRegion = "UK", sNr = "5", sSize = "14", sGender = "F" });
			AllItemsTop.Add(new TopSizes { sRegion = "UK", sNr = "6", sSize = "16", sGender = "F" });
			AllItemsTop.Add(new TopSizes { sRegion = "UK", sNr = "7", sSize = "18", sGender = "F" });
			#endregion

			# region Mannen tops
			AllItemsTop.Add(new TopSizes { sRegion = "EU", sNr = "1", sSize = "44", sGender = "M" });
			AllItemsTop.Add(new TopSizes { sRegion = "EU", sNr = "2", sSize = "46", sGender = "M" });
			AllItemsTop.Add(new TopSizes { sRegion = "EU", sNr = "3", sSize = "50", sGender = "M" });
			AllItemsTop.Add(new TopSizes { sRegion = "EU", sNr = "4", sSize = "54", sGender = "M" });
			AllItemsTop.Add(new TopSizes { sRegion = "EU", sNr = "5", sSize = "58", sGender = "M" });
			AllItemsTop.Add(new TopSizes { sRegion = "EU", sNr = "6", sSize = "60", sGender = "M" });
			AllItemsTop.Add(new TopSizes { sRegion = "EU", sNr = "7", sSize = "62", sGender = "M" });

			AllItemsTop.Add(new TopSizes { sRegion = "US", sNr = "1", sSize = "13", sGender = "M" });
			AllItemsTop.Add(new TopSizes { sRegion = "US", sNr = "2", sSize = "14", sGender = "M" });
			AllItemsTop.Add(new TopSizes { sRegion = "US", sNr = "3", sSize = "15", sGender = "M" });
			AllItemsTop.Add(new TopSizes { sRegion = "US", sNr = "4", sSize = "16", sGender = "M" });
			AllItemsTop.Add(new TopSizes { sRegion = "US", sNr = "5", sSize = "17", sGender = "M" });
			AllItemsTop.Add(new TopSizes { sRegion = "US", sNr = "6", sSize = "18", sGender = "M" });
			AllItemsTop.Add(new TopSizes { sRegion = "US", sNr = "7", sSize = "19", sGender = "M" });

			AllItemsTop.Add(new TopSizes { sRegion = "UK", sNr = "1", sSize = "13", sGender = "M" });
			AllItemsTop.Add(new TopSizes { sRegion = "UK", sNr = "2", sSize = "14", sGender = "M" });
			AllItemsTop.Add(new TopSizes { sRegion = "UK", sNr = "3", sSize = "15", sGender = "M" });
			AllItemsTop.Add(new TopSizes { sRegion = "UK", sNr = "4", sSize = "16", sGender = "M" });
			AllItemsTop.Add(new TopSizes { sRegion = "UK", sNr = "5", sSize = "17", sGender = "M" });
			AllItemsTop.Add(new TopSizes { sRegion = "UK", sNr = "6", sSize = "18", sGender = "M" });
			AllItemsTop.Add(new TopSizes { sRegion = "UK", sNr = "7", sSize = "19", sGender = "M" });
			#endregion

			#region Vrouwen bottoms
			AllItemsBottom.Add(new BottomSizes { sRegion = "EU", sNr = "1", sSize = "32", sGender = "F" });
			AllItemsBottom.Add(new BottomSizes { sRegion = "EU", sNr = "2", sSize = "34", sGender = "F" });
			AllItemsBottom.Add(new BottomSizes { sRegion = "EU", sNr = "3", sSize = "36", sGender = "F" });
			AllItemsBottom.Add(new BottomSizes { sRegion = "EU", sNr = "4", sSize = "38", sGender = "F" });
			AllItemsBottom.Add(new BottomSizes { sRegion = "EU", sNr = "5", sSize = "40", sGender = "F" });
			AllItemsBottom.Add(new BottomSizes { sRegion = "EU", sNr = "6", sSize = "42", sGender = "F" });
			AllItemsBottom.Add(new BottomSizes { sRegion = "EU", sNr = "7", sSize = "44", sGender = "F" });
			AllItemsBottom.Add(new BottomSizes { sRegion = "EU", sNr = "8", sSize = "46", sGender = "F" });
			AllItemsBottom.Add(new BottomSizes { sRegion = "EU", sNr = "9", sSize = "48", sGender = "F" });

			AllItemsBottom.Add(new BottomSizes { sRegion = "US", sNr = "1", sSize = "4", sGender = "F" });
			AllItemsBottom.Add(new BottomSizes { sRegion = "US", sNr = "2", sSize = "6", sGender = "F" });
			AllItemsBottom.Add(new BottomSizes { sRegion = "US", sNr = "3", sSize = "8", sGender = "F" });
			AllItemsBottom.Add(new BottomSizes { sRegion = "US", sNr = "4", sSize = "10", sGender = "F" });
			AllItemsBottom.Add(new BottomSizes { sRegion = "US", sNr = "5", sSize = "12", sGender = "F" });
			AllItemsBottom.Add(new BottomSizes { sRegion = "US", sNr = "6", sSize = "14", sGender = "F" });
			AllItemsBottom.Add(new BottomSizes { sRegion = "US", sNr = "7", sSize = "16", sGender = "F" });
			AllItemsBottom.Add(new BottomSizes { sRegion = "US", sNr = "8", sSize = "18", sGender = "F" });
			AllItemsBottom.Add(new BottomSizes { sRegion = "US", sNr = "9", sSize = "20", sGender = "F" });

			AllItemsBottom.Add(new BottomSizes { sRegion = "UK", sNr = "1", sSize = "0", sGender = "F" });
			AllItemsBottom.Add(new BottomSizes { sRegion = "UK", sNr = "2", sSize = "2", sGender = "F" });
			AllItemsBottom.Add(new BottomSizes { sRegion = "UK", sNr = "3", sSize = "4", sGender = "F" });
			AllItemsBottom.Add(new BottomSizes { sRegion = "UK", sNr = "4", sSize = "6", sGender = "F" });
			AllItemsBottom.Add(new BottomSizes { sRegion = "UK", sNr = "5", sSize = "8", sGender = "F" });
			AllItemsBottom.Add(new BottomSizes { sRegion = "UK", sNr = "6", sSize = "10", sGender = "F" });
			AllItemsBottom.Add(new BottomSizes { sRegion = "UK", sNr = "7", sSize = "12", sGender = "F" });
			AllItemsBottom.Add(new BottomSizes { sRegion = "UK", sNr = "8", sSize = "14", sGender = "F" });
			AllItemsBottom.Add(new BottomSizes { sRegion = "UK", sNr = "9", sSize = "16", sGender = "F" });
			#endregion

			#region Mannen bottoms
			AllItemsBottom.Add(new BottomSizes { sRegion = "EU", sNr = "1", sSize = "32", sGender = "M" });
			AllItemsBottom.Add(new BottomSizes { sRegion = "EU", sNr = "2", sSize = "34", sGender = "M" });
			AllItemsBottom.Add(new BottomSizes { sRegion = "EU", sNr = "3", sSize = "36", sGender = "M" });
			AllItemsBottom.Add(new BottomSizes { sRegion = "EU", sNr = "4", sSize = "38", sGender = "M" });
			AllItemsBottom.Add(new BottomSizes { sRegion = "EU", sNr = "5", sSize = "40", sGender = "M" });
			AllItemsBottom.Add(new BottomSizes { sRegion = "EU", sNr = "6", sSize = "42", sGender = "M" });
			AllItemsBottom.Add(new BottomSizes { sRegion = "EU", sNr = "7", sSize = "44", sGender = "M" });
			AllItemsBottom.Add(new BottomSizes { sRegion = "EU", sNr = "8", sSize = "46", sGender = "M" });
			AllItemsBottom.Add(new BottomSizes { sRegion = "EU", sNr = "9", sSize = "48", sGender = "M" });

			AllItemsBottom.Add(new BottomSizes { sRegion = "US", sNr = "1", sSize = "2", sGender = "M" });
			AllItemsBottom.Add(new BottomSizes { sRegion = "US", sNr = "2", sSize = "4", sGender = "M" });
			AllItemsBottom.Add(new BottomSizes { sRegion = "US", sNr = "3", sSize = "6", sGender = "M" });
			AllItemsBottom.Add(new BottomSizes { sRegion = "US", sNr = "4", sSize = "8", sGender = "M" });
			AllItemsBottom.Add(new BottomSizes { sRegion = "US", sNr = "5", sSize = "10", sGender = "M" });
			AllItemsBottom.Add(new BottomSizes { sRegion = "US", sNr = "6", sSize = "12", sGender = "M" });
			AllItemsBottom.Add(new BottomSizes { sRegion = "US", sNr = "7", sSize = "14", sGender = "M" });
			AllItemsBottom.Add(new BottomSizes { sRegion = "US", sNr = "8", sSize = "16", sGender = "M" });
			AllItemsBottom.Add(new BottomSizes { sRegion = "US", sNr = "9", sSize = "18", sGender = "M" });

			AllItemsBottom.Add(new BottomSizes { sRegion = "UK", sNr = "1", sSize = "4", sGender = "M" });
			AllItemsBottom.Add(new BottomSizes { sRegion = "UK", sNr = "2", sSize = "6", sGender = "M" });
			AllItemsBottom.Add(new BottomSizes { sRegion = "UK", sNr = "3", sSize = "8", sGender = "M" });
			AllItemsBottom.Add(new BottomSizes { sRegion = "UK", sNr = "4", sSize = "10", sGender = "M" });
			AllItemsBottom.Add(new BottomSizes { sRegion = "UK", sNr = "5", sSize = "12", sGender = "M" });
			AllItemsBottom.Add(new BottomSizes { sRegion = "UK", sNr = "6", sSize = "14", sGender = "M" });
			AllItemsBottom.Add(new BottomSizes { sRegion = "UK", sNr = "7", sSize = "16", sGender = "M" });
			AllItemsBottom.Add(new BottomSizes { sRegion = "UK", sNr = "8", sSize = "18", sGender = "M" });
			AllItemsBottom.Add(new BottomSizes { sRegion = "UK", sNr = "9", sSize = "20", sGender = "M" });
			#endregion

			#region Vrouwen feet
			AllItemsFeet.Add(new FeetSizes { sRegion = "EU", sNr = "1", sSize = "33", sGender = "F" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "EU", sNr = "2", sSize = "34", sGender = "F" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "EU", sNr = "3", sSize = "35", sGender = "F" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "EU", sNr = "4", sSize = "36", sGender = "F" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "EU", sNr = "5", sSize = "37", sGender = "F" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "EU", sNr = "6", sSize = "38", sGender = "F" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "EU", sNr = "7", sSize = "39", sGender = "F" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "EU", sNr = "8", sSize = "40", sGender = "F" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "EU", sNr = "9", sSize = "41", sGender = "F" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "EU", sNr = "10", sSize = "42", sGender = "F" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "EU", sNr = "11", sSize = "43", sGender = "F" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "EU", sNr = "12", sSize = "44", sGender = "F" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "EU", sNr = "13", sSize = "45", sGender = "F" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "EU", sNr = "14", sSize = "46", sGender = "F" });

			AllItemsFeet.Add(new FeetSizes { sRegion = "US", sNr = "1", sSize = "3", sGender = "F" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "US", sNr = "2", sSize = "4", sGender = "F" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "US", sNr = "3", sSize = "5", sGender = "F" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "US", sNr = "4", sSize = "6", sGender = "F" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "US", sNr = "5", sSize = "6.5", sGender = "F" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "US", sNr = "6", sSize = "7", sGender = "F" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "US", sNr = "7", sSize = "8", sGender = "F" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "US", sNr = "8", sSize = "8.5", sGender = "F" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "US", sNr = "9", sSize = "9", sGender = "F" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "US", sNr = "10", sSize = "10", sGender = "F" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "US", sNr = "11", sSize = "11", sGender = "F" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "US", sNr = "12", sSize = "12", sGender = "F" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "US", sNr = "13", sSize = "13", sGender = "F" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "US", sNr = "14", sSize = "14", sGender = "F" });

			AllItemsFeet.Add(new FeetSizes { sRegion = "UK", sNr = "1", sSize = "1", sGender = "F" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "UK", sNr = "2", sSize = "2", sGender = "F" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "UK", sNr = "3", sSize = "2.5", sGender = "F" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "UK", sNr = "4", sSize = "3", sGender = "F" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "UK", sNr = "5", sSize = "4", sGender = "F" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "UK", sNr = "6", sSize = "4.5", sGender = "F" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "UK", sNr = "7", sSize = "5", sGender = "F" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "UK", sNr = "8", sSize = "6", sGender = "F" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "UK", sNr = "9", sSize = "6.5", sGender = "F" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "UK", sNr = "10", sSize = "7", sGender = "F" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "UK", sNr = "11", sSize = "8", sGender = "F" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "UK", sNr = "12", sSize = "9.5", sGender = "F" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "UK", sNr = "13", sSize = "10", sGender = "F" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "UK", sNr = "14", sSize = "11", sGender = "F" });

			#endregion

			#region Mannen feet
			AllItemsFeet.Add(new FeetSizes { sRegion = "EU", sNr = "1", sSize = "34", sGender = "M" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "EU", sNr = "2", sSize = "35", sGender = "M" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "EU", sNr = "3", sSize = "36", sGender = "M" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "EU", sNr = "4", sSize = "37", sGender = "M" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "EU", sNr = "5", sSize = "38", sGender = "M" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "EU", sNr = "6", sSize = "39", sGender = "M" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "EU", sNr = "7", sSize = "40", sGender = "M" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "EU", sNr = "8", sSize = "41", sGender = "M" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "EU", sNr = "9", sSize = "42", sGender = "M" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "EU", sNr = "10", sSize = "43", sGender = "M" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "EU", sNr = "11", sSize = "44", sGender = "M" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "EU", sNr = "12", sSize = "45", sGender = "M" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "EU", sNr = "13", sSize = "46", sGender = "M" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "EU", sNr = "14", sSize = "47", sGender = "M" });

			AllItemsFeet.Add(new FeetSizes { sRegion = "US", sNr = "1", sSize = "3", sGender = "M" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "US", sNr = "2", sSize = "4", sGender = "M" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "US", sNr = "3", sSize = "5", sGender = "M" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "US", sNr = "4", sSize = "6", sGender = "M" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "US", sNr = "5", sSize = "6.5", sGender = "M" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "US", sNr = "6", sSize = "7", sGender = "M" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "US", sNr = "7", sSize = "8", sGender = "M" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "US", sNr = "8", sSize = "8.5", sGender = "M" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "US", sNr = "9", sSize = "9", sGender = "M" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "US", sNr = "10", sSize = "10", sGender = "M" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "US", sNr = "11", sSize = "11", sGender = "M" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "US", sNr = "12", sSize = "12", sGender = "M" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "US", sNr = "13", sSize = "13", sGender = "M" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "US", sNr = "14", sSize = "14", sGender = "M" });

			AllItemsFeet.Add(new FeetSizes { sRegion = "UK", sNr = "1", sSize = "2", sGender = "M" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "UK", sNr = "2", sSize = "3", sGender = "M" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "UK", sNr = "3", sSize = "3.5", sGender = "M" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "UK", sNr = "4", sSize = "4", sGender = "M" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "UK", sNr = "5", sSize = "5", sGender = "M" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "UK", sNr = "6", sSize = "6", sGender = "M" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "UK", sNr = "7", sSize = "6.5", sGender = "M" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "UK", sNr = "8", sSize = "7", sGender = "M" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "UK", sNr = "9", sSize = "8", sGender = "M" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "UK", sNr = "10", sSize = "9", sGender = "M" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "UK", sNr = "11", sSize = "9.5", sGender = "M" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "UK", sNr = "12", sSize = "10", sGender = "M" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "UK", sNr = "13", sSize = "11", sGender = "M" });
			AllItemsFeet.Add(new FeetSizes { sRegion = "UK", sNr = "14", sSize = "12", sGender = "M" });
			#endregion
		}

		private void FillPickers(string g, string r)
		{
			topSize.Items.Clear();
			bottomSize.Items.Clear();
			feetSize.Items.Clear();


			foreach( TopSizes i in AllItemsTop)
			{
				if(i.sGender == g && i.sRegion == r)
				{
					topSize.Items.Add(i.sSize);
				}
			}

			foreach (BottomSizes i in AllItemsBottom)
			{
				if (i.sGender == g && i.sRegion == r)
				{
					bottomSize.Items.Add(i.sSize);
				}
			}

			foreach (FeetSizes i in AllItemsFeet)
			{
				if (i.sGender == g && i.sRegion == r)
				{
					feetSize.Items.Add(i.sSize);
				}
			}
		}

		private void cbRegion_SelectedIndexChanged(object sender, EventArgs e)
		{
			switch (cbRegion.SelectedIndex)
			{
				case 0:
					_Region = "EU";
					break;
				case 1:
					_Region = "US";
					break;
				case 2:
					_Region = "UK";
					break;
			}

			FillPickers(_Gender, _Region);
		}

		private void cbGender_SelectedIndexChanged(object sender, EventArgs e)
		{
			switch (cbGender.SelectedIndex)
			{
				case 0:
					_Gender = "F";
					break;
				case 1:
					_Gender = "M";
					break;
			}

			FillPickers(_Gender, _Region);
		}

		private async void btnSaveSize_Clicked(object sender, EventArgs e)
		{
			if (topSize.SelectedIndex == -1 || bottomSize.SelectedIndex == -1 || feetSize.SelectedIndex == -1)
			{
				await DisplayAlert("Error", "Please tell us your size before proceeding.", "OK");
			}
			else
			{
				await DisplayAlert("Error", "niet empty.", "OK");
			}
						
			////Sla de gegevens op in de dbs
			//try
			//{
			//	int iTop = topSize.SelectedIndex;
			//	string sTop_size = null;
			//	var users_id = Models.LoginCredentials.loginId;

			//	//Verander de top size string a.d.h.v. het gekozen antwoord
			//	switch (iTop)
			//	{
			//		case -1:
			//			sTop_size = "Empty";
			//			break;

			//		case 0:
			//			sTop_size = "XS";
			//			break;

			//		case 1:
			//			sTop_size = "S";
			//			break;

			//		case 2:
			//			sTop_size = "M";
			//			break;

			//		case 3:
			//			sTop_size = "L";
			//			break;

			//		case 4:
			//			sTop_size = "XL";
			//			break;

			//		default:
			//			break;
			//	}

			//	//Check of alle invoervelden ingevuld zijn
			//	if (sTop_size == "Empty" || string.IsNullOrEmpty(bottomSize.Text) || string.IsNullOrEmpty(feetSize.Text))
			//	{
			//		await DisplayAlert("Error", "Please choose or insert all your sizes before proceeding.", "OK");
			//	}
			//	//Wanneer alles is ingevoerd -> sla gegevens op in de database
			//	else
			//	{
			//		string webadres = "http://good-lookz.com/API/account/saveSize.php?users_id=" + users_id + "& top=" + sTop_size + "&bottom=" + bottomSize.Text + "&feet=" + feetSize.Text;
			//		HttpClient connect = new HttpClient();
			//		HttpResponseMessage uploadToSale = await connect.GetAsync(webadres);
			//		uploadToSale.EnsureSuccessStatusCode();

			//		string result = await uploadToSale.Content.ReadAsStringAsync();

			//		//Is het resultaat succes
			//		if (result == "Success")
			//		{
			//			await DisplayAlert("Success", "Your sizes have been saved! You can always change your size in settings.", "OK");
			//			App.Current.MainPage = new NavigationPage(new MenuPage());
			//		}
			//		//Is het resultaat failed
			//		else if (result == "Failed")
			//		{
			//			await DisplayAlert("Error", "Something went wrong, please check your internet connection and try again.", "OK");
			//		}
			//	}
			//}
			//catch (Exception)
			//{
			//	await DisplayAlert("Error", "Something went wrong, please check your internet connection and try again.", "OK");
			//	throw;
			//}
		}
	}
}
