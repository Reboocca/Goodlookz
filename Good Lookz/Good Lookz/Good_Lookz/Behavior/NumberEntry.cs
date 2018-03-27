using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Good_Lookz.Behavior
{
	class NumberEntry : Behavior<Entry>
	{
		//Deze functie gaat na of de ingevoerde character een nummer is
		//zo niet -> haal dit teken weg
		protected override void OnAttachedTo(Entry entry)
		{
			entry.TextChanged += OnEntryTextChanged;
			base.OnAttachedTo(entry);
		}

		protected override void OnDetachingFrom(Entry entry)
		{
			entry.TextChanged -= OnEntryTextChanged;
			base.OnDetachingFrom(entry);
		}

		private static void OnEntryTextChanged(object sender, TextChangedEventArgs args)
		{

			if (!string.IsNullOrWhiteSpace(args.NewTextValue))
			{
				bool Nummer = args.NewTextValue.ToCharArray().All(IsNummer); //Check of de gegeven characters nummers zijn

				((Entry)sender).Text = Nummer ? args.NewTextValue : args.NewTextValue.Remove(args.NewTextValue.Length - 1);
			}
		}

		//Ga na of de opgegeven char een nummer is
		private static bool IsNummer(char c)
		{
			if (c >= 48)
			{
				return c <= 57;
			}

			return false;
		}
	}
}
