/*DicdeRoller MauiProgram.cs*
 *Develop and generate code to launch MauiApp
 *Course- IST440W
 *Author: Sedrick Germain
 *Date Developed: 9/26/2022
 *Last Changed: 10/1/2022
 *Rev: 6
 */

using Plugin.Maui.Audio;

namespace DiceRoller;

public partial class MainPage : ContentPage
{
	int randomValue;
    private readonly IAudioManager audiomanager;
    int i;

    List<string> diceList;
    /*private object ShakeLabel;*/

    public MainPage(IAudioManager audiomanager)
	{
		InitializeComponent();
		diceList = new List<string>();
		diceList.Add("diceone.png");
		diceList.Add("dicetwo.png");
		diceList.Add("dicethree.png");
		diceList.Add("dicefour.png");
		diceList.Add("dicefive.png");
		diceList.Add("dicesix.png");
        ToggleShake();
		image.WidthRequest = 300;
		image.HeightRequest = 300;
		image.Source = "dicesix.png";
		this.audiomanager = audiomanager;
	}

	private async void Button_Clicked(object sender, EventArgs e)
	{
		randomValue = new Random().Next(6);
		

		image.Source = (diceList[randomValue]);
        await TextToSpeech.Default.SpeakAsync($"You rolled: {randomValue + 1}");
		VibrateStartButton_Clicked();
        var player = audiomanager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("dicerollsound.wav"));
        player.Play();
        i = 0;
        for (i = 0; i <= randomValue; i++)
        {
            await Flashlight.Default.TurnOnAsync();
            await Task.Delay(200);
            await Flashlight.Default.TurnOffAsync();
        }
    }

    private void VibrateStartButton_Clicked()
    {
        int secondsToVibrate = Random.Shared.Next(1, 7);
        TimeSpan vibrationLength = TimeSpan.FromSeconds(secondsToVibrate);

        Vibration.Default.Vibrate(vibrationLength);
    }



    private void ToggleShake()
    {
        if (Accelerometer.Default.IsSupported)
        {
            if (!Accelerometer.Default.IsMonitoring)
            {
                // Turn on compass
                Accelerometer.Default.ShakeDetected += Accelerometer_ShakeDetected;
                Accelerometer.Default.Start(SensorSpeed.Game);
            }
        }
    }

    private async void Accelerometer_ShakeDetected(object sender, EventArgs e)
    {
        randomValue = new Random().Next(6);
        image.Source = (diceList[randomValue]);
        await TextToSpeech.Default.SpeakAsync($"You rolled: {randomValue + 1}");
        VibrateStartButton_Clicked();
        var player = audiomanager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("dicerollsound.wav"));
        player.Play();
        i = 0;
        for (i = 0; i <= randomValue; i++)
        {
            await Flashlight.Default.TurnOnAsync();
            await Task.Delay(200);
            await Flashlight.Default.TurnOffAsync();
        }
        }
}
      