# HockeySDK-XamarinDemo
## Before App Deployment
* Android
  1. Change https://github.com/bitstadium/HockeySDK-XamarinDemo/blob/master/Droid/MainActivity.cs#L25 to HockeyApp AppId
  2. Change https://github.com/bitstadium/HockeySDK-XamarinDemo/blob/master/Droid/MainActivity.cs#L55 to Insights AppId
* iOS
  1. Change https://github.com/bitstadium/HockeySDK-XamarinDemo/blob/master/iOS/AppDelegate.cs#L22 to HockeyApp AppId
  2. Change https://github.com/bitstadium/HockeySDK-XamarinDemo/blob/master/iOS/AppDelegate.cs#L43 to Insights AppId
* Deploy against {Debug|Release}+{HockeyApp|Insights} Build Configuration
