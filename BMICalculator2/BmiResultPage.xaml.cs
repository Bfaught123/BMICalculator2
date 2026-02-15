namespace BMICalculator2;

public partial class BmiResultPage : ContentPage
{
    private readonly double _bmi;
    private readonly string _gender;
    private readonly string _category;

    public BmiResultPage(int height, int weight, double bmi, string gender)
    {
        InitializeComponent();

        _bmi = bmi;
        _gender = gender;
        _category = GetCategory(_gender, _bmi);

        BmiLabel.Text = $"BMI: {_bmi}";
        CategoryLabel.Text = $"Category: {_category}";
    }

    private static string GetCategory(string gender, double bmi)
    {
        bool isMale = gender.Equals("Male", StringComparison.OrdinalIgnoreCase);

        if (isMale)
        {
            if (bmi < 18.5) return "Underweight";
            if (bmi < 25) return "Normal weight";
            if (bmi < 30) return "Overweight";
            return "Obese";
        }
        else
        {
            if (bmi < 18) return "Underweight";
            if (bmi < 24) return "Normal weight";
            if (bmi < 29) return "Overweight";
            return "Obese";
        }
    }

    private async void OnGoToRecommendationsClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RecommendationsPage(_bmi, _gender, _category));
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopToRootAsync();
    }
}
