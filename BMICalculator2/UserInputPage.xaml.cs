namespace BMICalculator2;

public partial class UserInputPage : ContentPage
{
    enum Gender { None, Male, Female }
    Gender selectedGender = Gender.None;

    public UserInputPage()
    {
        InitializeComponent();

        MaleCard.Opacity = 0.5;
        FemaleCard.Opacity = 0.5;

        UpdateHeightLabel();
        UpdateWeightLabel();
    }

    void OnMaleTapped(object sender, TappedEventArgs e)
    {
        selectedGender = Gender.Male;
        HighlightGender();
    }

    void OnFemaleTapped(object sender, TappedEventArgs e)
    {
        selectedGender = Gender.Female;
        HighlightGender();
    }

    void HighlightGender()
    {
        MaleCard.Opacity = selectedGender == Gender.Male ? 1 : 0.5;
        FemaleCard.Opacity = selectedGender == Gender.Female ? 1 : 0.5;

        MaleCard.BorderColor = selectedGender == Gender.Male ? Colors.DodgerBlue : Colors.Transparent;
        FemaleCard.BorderColor = selectedGender == Gender.Female ? Colors.DeepPink : Colors.Transparent;
    }

    void OnHeightChanged(object sender, ValueChangedEventArgs e)
        => UpdateHeightLabel();

    void OnWeightChanged(object sender, ValueChangedEventArgs e)
        => UpdateWeightLabel();

    void UpdateHeightLabel()
    => HeightValueLabel.Text = $"{(int)HeightSlider.Value} in";

    void UpdateWeightLabel()
        => WeightValueLabel.Text = $"{(int)WeightSlider.Value} lbs";


    async void OnGoToResultsClicked(object sender, EventArgs e)
    {
        if (selectedGender == Gender.None)
        {
            await DisplayAlert("Missing Information", "Please select a gender.", "OK");
            return;
        }

        int height = (int)HeightSlider.Value;
        int weight = (int)WeightSlider.Value;

        double bmi = Math.Round(703.0 * weight / (height * height), 1);

        await Navigation.PushAsync(new BmiResultPage(height, weight, bmi, selectedGender.ToString()));
    }
}
