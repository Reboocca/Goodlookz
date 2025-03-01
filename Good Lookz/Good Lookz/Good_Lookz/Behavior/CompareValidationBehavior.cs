﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Good_Lookz.Behavior
{
	class CompareValidationBehavior : Behavior<Entry>
	{
		//Code van: https://www.c-sharpcorner.com/article/input-validation-in-xamarin-forms-behaviors/
		public static BindableProperty TextProperty = BindableProperty.Create<CompareValidationBehavior, string>(tc => tc.Text, string.Empty, BindingMode.TwoWay);

		public string Text
		{
			get
			{
				return (string)GetValue(TextProperty);
			}
			set
			{
				SetValue(TextProperty, value);
			}
		}


		protected override void OnAttachedTo(Entry bindable)
		{
			bindable.TextChanged += HandleTextChanged;
			base.OnAttachedTo(bindable);
		}

		void HandleTextChanged(object sender, TextChangedEventArgs e)
		{
			bool IsValid = false;
			IsValid = e.NewTextValue == Text;

			((Entry)sender).TextColor = IsValid ? Color.Default : Color.Red;
		}

		protected override void OnDetachingFrom(Entry bindable)
		{
			bindable.TextChanged -= HandleTextChanged;
			base.OnDetachingFrom(bindable);
		}
	}
}