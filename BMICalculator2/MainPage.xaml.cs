using System;
using Microsoft.Maui.Controls;

namespace BMICalculator2;

public partial class MainPage : ContentPage
{
    private enum Gender
    {
        None,
        Male,
        Female
    }

    private Gender _selectedGender = Gender.None;

    public MainPage()
    {
        InitializeComponent();
        UpdateHeightLabel();
        UpdateWeightLabel();
    }

    // Gender taps
    private void OnMaleTapped(object sender, TappedEventArgs e)
    {
        _selectedGender = Gender.Male;
        HighlightGender();
    }

    private void OnFemaleTapped(object sender, TappedEventArgs e)
    {
        _selectedGender = Gender.Female;
        HighlightGender();
    }

    private void HighlightGender()
    {
        // Reset
        MaleCard.BorderColor = Microsoft.Maui.Graphics.Colors.Transparent;
        FemaleCard.BorderColor = Microsoft.Maui.Graphics.Colors.Transparent;
        MaleCard.Opacity = 0.5;
        FemaleCard.Opacity = 0.5;

        if (_selectedGender == Gender.Male)
        {
            MaleCard.BorderColor = Microsoft.Maui.Graphics.Colors.DodgerBlue;
            MaleCard.Opacity = 1.0;
        }
        else if (_selectedGender == Gender.Female)
        {
            FemaleCard.BorderColor = Microsoft.Maui.Graphics.Colors.DeepPink;
            FemaleCard.Opacity = 1.0;
        }
    }

    // Slider value updates
    private void OnHeightChanged(object sender, ValueChangedEventArgs e)
    {
        UpdateHeightLabel();
    }

    private void OnWeightChanged(object sender, ValueChangedEventArgs e)
    {
        UpdateWeightLabel();
    }

    private void UpdateHeightLabel()
    {
        int h = (int)Math.Round(HeightSlider.Value);
        HeightValueLabel.Text = h.ToString();
    }

    private void UpdateWeightLabel()
    {
        int w = (int)Math.Round(WeightSlider.Value);
        WeightValueLabel.Text = w.ToString();
    }

    // Calculate BMI and show popup
    private async void OnCalculateClicked(object sender, EventArgs e)
    {
        if (_selectedGender == Gender.None)
        {
            await DisplayAlert("Missing information",
                "Please tap Male or Female before calculating.",
                "Ok");
            return;
        }

        int heightInches = (int)Math.Round(HeightSlider.Value);
        int weightPounds = (int)Math.Round(WeightSlider.Value);

        if (heightInches <= 0 || weightPounds <= 0)
        {
            await DisplayAlert("Invalid values",
                "Please make sure height and weight are greater than zero.",
                "Ok");
            return;
        }

        // BMI formula using pounds and inches
        double bmi = 703.0 * weightPounds / (heightInches * heightInches);
        bmi = Math.Round(bmi, 1);

        string status = GetStatus(_selectedGender, bmi);
        string recommendations = GetRecommendations(status);

        string genderText = _selectedGender == Gender.Male ? "Male" : "Female";

        string message =
            $"Gender: {genderText}\n" +
            $"BMI: {bmi}\n" +
            $"Health Status: {status}\n" +
            $"Recommendations:\n{recommendations}";

        await DisplayAlert("Your calculated BMI results are:", message, "Ok");
    }

    // Gender-based BMI classification
    private string GetStatus(Gender gender, double bmi)
    {
        if (gender == Gender.Male)
        {
            if (bmi < 18.5)
                return "Underweight";
            if (bmi < 25)
                return "Normal weight";
            if (bmi < 30)
                return "Overweight";
            return "Obese";
        }
        else
        {
            if (bmi < 18)
                return "Underweight";
            if (bmi < 24)
                return "Normal weight";
            if (bmi < 29)
                return "Overweight";
            return "Obese";
        }
    }

    // Recommendations based on BMI status
    private string GetRecommendations(string status)
    {
        switch (status)
        {
            case "Underweight":
                return
                    "-Increase your calorie intake with nutrient-dense foods.\n" +
                    "-Talk with a healthcare provider or dietitian.\n" +
                    "-Add strength training to help build muscle.";

            case "Normal weight":
                return
                    "-Maintain a balanced diet rich in fruits, vegetables, and lean proteins.\n" +
                    "-Stay active most days of the week.\n" +
                    "-Keep regular sleep and stress-management habits.";

            case "Overweight":
                return
                    "-Reduce processed foods and focus on portion control.\n" +
                    "-Engage in regular aerobic exercises (jogging, swimming, walking).\n" +
                    "-Include strength training 2–3 times per week.\n" +
                    "-Drink plenty of water and track your progress.";

            case "Obese":
                return
                    "-Work with a healthcare provider to create a safe weight-loss plan.\n" +
                    "-Gradually increase physical activity to at least 150 minutes per week.\n" +
                    "-Limit sugary drinks and high-fat, high-salt foods.\n" +
                    "-Set small, realistic goals and monitor your BMI and weight.";

            default:
                return
                    "-Review your current eating and activity habits.\n" +
                    "-Consider talking with a healthcare professional for personalized advice.";
        }
    }
}
