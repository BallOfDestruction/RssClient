﻿using UIKit;

namespace iOS.App.Base.Stated
{
	public class LoadingView : UIView
	{
		public LoadingView(UIView parentView) : base(parentView.Frame)
		{
			BackgroundColor = UIColor.Red;
		}
	}
}