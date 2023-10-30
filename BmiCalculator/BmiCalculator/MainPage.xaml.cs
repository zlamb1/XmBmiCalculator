using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BmiCalculator
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        
        public (bool, double) ParseDouble(string Text)
        {
            if (Text == null || Text.Equals(string.Empty))
                return (false, 0.0);
            try
            {
                return (true, Convert.ToDouble(Text));
            } catch (Exception)
            {
                return (false, 0.0);
            }
        }

        public double ImperialSystem_CalculateBMI(double height, double weight)
        {
            return 703 * (weight / Math.Pow(height, 2));
        }

        public double MetricSystem_CalculateBMI(double height, double weight)
        {
            return weight / Math.Pow(height, 2);
        }

        public void OnToggleImperialSystem(object sender, ToggledEventArgs e)
        {
            var Height_Placeholder = "Enter your height";
            var Weight_Placeholder = "Enter your weight";
            if (e.Value)
            {
                Height_Placeholder += " (inches)";
                Weight_Placeholder += " (lbs)";
            } else
            {
                Height_Placeholder += " (meters)";
                Weight_Placeholder += " (kg)";
            }
            Editor_Height.Placeholder = Height_Placeholder;
            Editor_Weight.Placeholder = Weight_Placeholder;
        }

        public void OnHeightChanged(object sender, TextChangedEventArgs e)
        {
            var (succeeded, _) = ParseDouble(Editor_Height.Text);
            if (succeeded)
            {
                Label_HeightHint.Text = "";
            } else
            {
                Label_HeightHint.Text = "Please enter a valid number!";
            }
        }

        public void OnWeightChanged(object sender, TextChangedEventArgs e)
        {
            var (succeeded, _) = ParseDouble(Editor_Weight.Text);
            if (succeeded)
            {
                Label_WeightHint.Text = "";
            }
            else
            {
                Label_WeightHint.Text = "Please enter a valid number!";
            }
        }

        public void OnCalculateBMI(object sender, EventArgs e)
        {
            // This is bad practice but its a hack that allows hinting on click
            OnWeightChanged(null, null);
            OnHeightChanged(null, null);

            var (succeeded0, height) = ParseDouble(Editor_Height.Text);
            var (succeeded1, weight) = ParseDouble(Editor_Weight.Text);

            if (!succeeded0 || !succeeded1)
                return;

            var bmi = Switch_ImperialSystem.IsToggled ? ImperialSystem_CalculateBMI(height, weight) 
                : MetricSystem_CalculateBMI(height, weight);
            Span_BMI.Text = $"Your BMI: {Math.Round(bmi, 2)}";
        }
    }
}
