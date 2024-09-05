namespace SteamC2FinderCore.Utils
{
    public class ProgressBarHelper
    {
        public static void DisplayProgressBar(int currentStep, int totalSteps)
        {
            int progressBarWidth = 50;
            double progress = (double)currentStep / totalSteps;
            int progressWidth = (int)(progress * progressBarWidth);

            Console.CursorLeft = 0;
            Console.Write("[");
            Console.Write(new string('#', progressWidth));
            Console.Write(new string('-', progressBarWidth - progressWidth));
            Console.Write("]");
            Console.Write($" {currentStep}/{totalSteps} ({progress * 100:0}%)");
        }
    }
}
