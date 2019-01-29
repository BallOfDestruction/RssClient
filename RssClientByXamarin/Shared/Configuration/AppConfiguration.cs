using System;

namespace Shared.Configuration
{
    public class AppConfiguration
    {
        public StartPage StartPage { get; set; } = StartPage.RssList;
        public MessagesViewer MessagesViewer { get; set; } = MessagesViewer.Browser;

        /// <summary>
        ///  In millisecond
        /// </summary>
        public int DefaultAnimationTime { get; set; } = 200;

        public AnimationSpeed AnimationSpeed { get; set; } = AnimationSpeed.x;
        public AnimationType AnimationType { get; set; } = AnimationType.From_left;

        public int GetCalculationAnimationTime()
        {
            var defaultTime = DefaultAnimationTime;
            switch (AnimationSpeed)
            {
                case AnimationSpeed.x0_25:
                    return defaultTime * 4;
                case AnimationSpeed.x0_5:
                    return defaultTime * 2;
                case AnimationSpeed.x:
                    return defaultTime;
                case AnimationSpeed.x2:
                    return defaultTime / 2;
                case AnimationSpeed.x4:
                    return defaultTime / 4;
                case AnimationSpeed.x8:
                    return defaultTime / 8;
            }
            
            throw new NotImplementedException(nameof(AppConfiguration) + nameof(GetCalculationAnimationTime));
        }
    }
}