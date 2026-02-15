namespace BMICalculator2;

public partial class RecommendationsPage : ContentPage
{
    private readonly double _bmi;
    private readonly string _gender;
    private readonly string _category;

    public RecommendationsPage(double bmi, string gender, string category)
    {
        InitializeComponent();

        _bmi = bmi;
        _gender = gender;
        _category = category;

        RecommendationsLabel.Text = GetRecommendations(_category);
    }

    private static string GetRecommendations(string category)
    {
        return category switch
        {
            "Underweight" =>
                "- Increase calories with nutrient-dense foods.\n" +
                "- Add strength training to build muscle.\n" +
                "- Consider talking with a healthcare provider.",

            "Normal weight" =>
                "- Keep a balanced diet and consistent activity.\n" +
                "- Aim for good sleep and stress management.\n" +
                "- Maintain your current routine.",

            "Overweight" =>
                "- Reduce processed foods and watch portion sizes.\n" +
                "- Do regular cardio (walking, jogging, swimming).\n" +
                "- Add strength training 2–3x per week.\n" +
                "- Drink water and track progress.",

            "Obese" =>
                "- Work with a healthcare provider on a safe plan.\n" +
                "- Increase activity gradually (goal: 150 min/week).\n" +
                "- Limit sugary drinks and high-fat foods.\n" +
                "- Set small goals and stay consistent.",

            _ => "No recommendation available."
        };
    }

    private async void OnBackToResultsClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void OnBackToInputClicked(object sender, EventArgs e)
    {
        await Navigation.PopToRootAsync();
    }
}
